using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Kajo.Configuration.Startup.HostingStartup
{
    public static class MvcHostingStartup
    {
        public static void MvcConfigureServices(this IServiceCollection services)
        {
            //ADD MVC With Custom routing option
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                RegisterFilters.RegisterCustomFilters(options.Filters);
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        public static void MvcConfigure(this IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
