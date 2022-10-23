using Kajo.Services;
using Kajo.Services.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Kajo.Configuration.Startup.HostingStartup
{
    public static class RegisterServicesHostingStartup
    {
        public static void RegisterServicesConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticateService>();
            services.AddScoped<RolesService>();
            services.AddScoped<UsersService>();

            services.AddScoped<TopicsRestService>();
            services.AddScoped<PostsRestService>();
            services.AddScoped<PublicPostsService>();
        }
    }
}
