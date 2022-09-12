using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Kajo.Configuration.Startup.HostingStartup
{
    public static class ODataHostingStartup
    {
        public static void ODataConfigureServices(this IServiceCollection services)
        {

            services.APIVersioningConfigureServices();
            //Add OData and integrate API Versioning with OData
            services.AddOData().EnableApiVersioning();

            //Add OData API Explorer: for odata to be able to discover api endpoints
            services.AddODataApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

        }

        public static void ODataConfigure(this IApplicationBuilder app, VersionedODataModelBuilder modelBuilder)
        {
            app.UseMvc(routeBuilder =>
            {
                //Add OData Query options
                routeBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();

                //OData URLs Convention
                routeBuilder.MapVersionedODataRoutes("odata", "api", modelBuilder.GetEdmModels());

                routeBuilder.EnableDependencyInjection();
            });
        }
    }
}
