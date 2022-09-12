using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Kajo.Configuration.Startup.HostingStartup;
using System;
using System.Linq;
using static Microsoft.AspNetCore.Mvc.CompatibilityVersion;

namespace Kajo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// Configures services for the application.
        /// </summary>
        /// <param name="services">The collection of services to configure the application with.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.MvcConfigureServices();
            services.ODataConfigureServices();
            services.SwaggerConfigureServices();
            services.DBContextConfigureServices(Configuration);
            services.IdentityConfigureServices(Configuration);
            services.RegisterServicesConfigureServices();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// Configures the application using the provided builder, hosting environment.
        /// </summary>
        /// <param name="app">The current application builder.</param>
        /// <param name="hostingEnvironment">The current hosting environment.</param>
        /// <param name="modelBuilder">The <see cref="VersionedODataModelBuilder">model builder</see> used to create OData entity data models (EDMs).</param>
        /// <param name="apiVersionProvider">The API version descriptor provider used to enumerate defined API versions.</param>
        /// <param name="loggerFactory">Logger</param>
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment hostingEnvironment, 
            VersionedODataModelBuilder modelBuilder, 
            IApiVersionDescriptionProvider apiVersionProvider, 
            ILoggerFactory loggerFactory)
        {
            app.Log4NetConfigure(loggerFactory);
            app.ExceptionHandlingConfigure();
            app.HttpsConfigure();
            app.UseStaticFiles();
            app.CorsConfigure(Configuration);
            app.IdentityConfigure();
            app.ODataConfigure(modelBuilder);
            app.MvcConfigure();
            app.SwaggerConfigure(apiVersionProvider);
        }
    }
}
