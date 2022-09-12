using Kajo.Models;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace Kajo.Configuration.Startup.HostingStartup
{
    public static class APIVersioningHostingStartup
    {
        public static void APIVersioningConfigureServices(this IServiceCollection services)
        {
            //Add API Versioning
            services.AddApiVersioning(options =>
            {
                // send the api-supported-versions and api-deprecated-versions HTTP header in responses.
                options.ReportApiVersions = true;

                //Add Default API Version so requests can be made without specifying API Version
                options.DefaultApiVersion = ApiVersions.V1;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.Conventions.Add(new VersionByNamespaceConvention());
            });

        }
    }
}
