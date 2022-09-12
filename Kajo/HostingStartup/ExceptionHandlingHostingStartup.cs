using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Kajo.Configuration.Startup.HostingStartup
{
    public static class ExceptionHandlingHostingStartup
    {

        public static void ExceptionHandlingConfigure(this IApplicationBuilder app)
        {
            app.UseExceptionHandler("/error");
        }
    }
}
