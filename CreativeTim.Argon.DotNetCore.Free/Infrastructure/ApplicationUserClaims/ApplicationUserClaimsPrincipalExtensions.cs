using System.Linq;
using System.Security.Claims;

namespace CreativeTim.Argon.DotNetCore.Free.Infrastructure.ApplicationUserClaims
{
    public static class ApplicationUserClaimsPrincipalExtensions
    {
        public static string GetFullNameOrEmail(this ClaimsPrincipal principal)
        {
            var fullName = principal.Claims.FirstOrDefault(c => c.Type == "FullName");
            return fullName?.Value ?? principal.Identity?.Name;
        }

        public static string GetFullName(this ClaimsPrincipal principal)
        {
            var fullName = principal.Claims.FirstOrDefault(c => c.Type == "FullName");
            return fullName?.Value;
        }

        // You can add other extension methods here to access user properties exposed
        // via the ApplicationUserClaimsPrincipalFactory class
    }
}
