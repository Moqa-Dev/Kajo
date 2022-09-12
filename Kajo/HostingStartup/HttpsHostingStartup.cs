using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Kajo.Configuration.Startup.HostingStartup
{
    public static class CorsHostingStartup
    {
        public static void CorsConfigure(this IApplicationBuilder app, IConfiguration Configuration)
        {
            var origins = Configuration.GetValue(typeof(string), "CORS").ToString()
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .ToArray();

            app.UseCors(builder => builder
                .WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(_ => true)
                .AllowCredentials()
            );
        }
    }
}
