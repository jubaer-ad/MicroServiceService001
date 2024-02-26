using PlatformService.Data.Interfaces;

namespace PlatformService
{
    public static class ServiceRegistrations
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPlatformRepo, PlatformRepo>();
        }
    }
}