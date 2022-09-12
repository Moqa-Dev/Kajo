using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System.Linq;
using System.Reflection;
using System;
using Kajo.Infrastucture.SwaggerConfigurations;

namespace Kajo.Configuration.Startup.HostingStartup
{
    public static class SwaggerHostingStartup
    {
        public static void SwaggerConfigureServices(this IServiceCollection services)
        {
            // Workaround: https://github.com/OData/WebApi/issues/1177
            //For Swashbuckle swagger generator to work
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });

            //ADD Swashbuckle Swager Generator
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // Define the BearerAuth scheme that's in use
                    options.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                    {
                        Description = "JWT Authorization header using the Bearer scheme. \r\nExample: \"Value: Bearer {token}\"\r\n Get Token From Login Endpoint.",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey",
                    });
                    // Assign scope requirements to operations based on AuthorizeAttribute
                    options.OperationFilter<SecurityOperationFilter>();

                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    options.OperationFilter<FileUploadOperationFilter>();

                    // integrate xml comments
                    options.IncludeXmlComments(XmlCommentsFilePath);
                });

        }

        public static void SwaggerConfigure(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            //ADD Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }
        //Include XML Comments path for Swashbuckle 
        //to be able to generate Documentation using XML Comments
        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = AppContext.BaseDirectory;
                var fileName = "Doc.xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}
