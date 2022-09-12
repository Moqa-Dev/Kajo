using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Kajo.Configuration.Startup.HostingStartup
{
    public static class HttpsHostingStartup
    {
        public static void HttpsConfigure(this IApplicationBuilder app)
        {
            //app.UseHttpsRedirection();
            //app.UseHsts();
        }
    }
}
