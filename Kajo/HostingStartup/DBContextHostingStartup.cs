using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Kajo.DataContext;

namespace Kajo.Configuration.Startup.HostingStartup
{
    public static class DBContextHostingStartup
    {
        public static void DBContextConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            //Add Database Context
            //.UseLazyLoadingProxies() added for navigation properties to be loaded
            //Also package Microsoft.EntityFrameworkCore.Proxies Added
            //check out: https://www.koskila.net/ef-core-returns-null-for-a-navigation-property/
            services.AddDbContext<KajoContext>(options =>
                    options.UseLazyLoadingProxies().UseSqlServer(
                        configuration.GetConnectionString("KajoContext")));
        }

    }
}
