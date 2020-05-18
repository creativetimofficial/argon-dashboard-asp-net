using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CreativeTim.Argon.DotNetCore.Free.Data.Seeders
{
    public static class IdentityDataSeeder<TIdentityUser, TIdentityRole>
        where TIdentityUser : IdentityUser, new()
        where TIdentityRole : IdentityRole, new()
    {
        private const string DefaultAdminRoleName = "Administrator";
        private const string DefaultAdminUserEmail = "admin@argon.com";
        private const string DefaultAdminUserPassword = "Secret1+";

        private static async Task CreateDefaultAdminRole(RoleManager<TIdentityRole> roleManager)
        {
            // Make sure we have an Administrator role
            if (!await roleManager.RoleExistsAsync(DefaultAdminRoleName))
            {
                var role = new TIdentityRole
                {
                    Name = DefaultAdminRoleName
                };

                var roleResult = await roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    throw new ApplicationException($"Could not create '{DefaultAdminRoleName}' role");
                }
            }
        }

        private static async Task<TIdentityUser> CreateDefaultAdminUser(UserManager<TIdentityUser> userManager)
        {
            var user = await userManager.FindByEmailAsync("admin@argon.com");
            if (user == null)
            {
                user = new TIdentityUser
                {
                    UserName = DefaultAdminUserEmail,
                    Email = DefaultAdminUserEmail,
                    EmailConfirmed = true,
                };
                var userResult = await userManager.CreateAsync(user, DefaultAdminUserPassword);

                if (!userResult.Succeeded)
                {
                    throw new ApplicationException($"Could not create '{DefaultAdminUserEmail}' user");
                }
            }

            return user;
        }

        private static async Task AddDefaultAdminRoleToDefaultAdminUser(
            UserManager<TIdentityUser> userManager,
            TIdentityUser user)
        {
            // Add user to Administrator role if it's not already associated
            if (!(await userManager.GetRolesAsync(user)).Contains(DefaultAdminRoleName))
            {
                var addToRoleResult = await userManager.AddToRoleAsync(user, DefaultAdminRoleName);
                if (!addToRoleResult.Succeeded)
                {
                    throw new ApplicationException(
                        $"Could not add user '{DefaultAdminUserEmail}' to '{DefaultAdminRoleName}' role");
                }
            }
        }

        public static async Task SeedDataAsync(IServiceProvider services, ILogger logger) 
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<TIdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<TIdentityRole>>();

            var retries = 3;
            ensureCreated:
            try
            {
                await context.Database.EnsureCreatedAsync();
            }
            catch (Exception ex)
            {
                if (retries == 0)
                {
                    throw;
                }

                logger.LogWarning(ex, $"An error occurred while seeding the database ; maybe the server isn't currently running, will retry {retries} more times after 5 seconds");
                await Task.Delay(5000);

                retries -= 1;
                goto ensureCreated;
            }

            await CreateDefaultAdminRole(roleManager);
            var defaultAdminUser = await CreateDefaultAdminUser(userManager);
            await AddDefaultAdminRoleToDefaultAdminUser(userManager, defaultAdminUser);
        }
    }
}
