using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kajo.Configuration.Startup.HostingStartup
{
    public static class Log4NetHostingStartup
    {
        public static void Log4NetConfigureServices(this IServiceCollection services)
        {

        }

        public static void Log4NetConfigure(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();
        }
    }
}
