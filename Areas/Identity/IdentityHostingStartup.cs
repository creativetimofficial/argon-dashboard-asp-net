using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CreativeTim.Argon.DotNetCore.Free.Areas.Identity.IdentityHostingStartup))]
namespace CreativeTim.Argon.DotNetCore.Free.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}