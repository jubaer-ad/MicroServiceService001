using PlatformService.Data.Interfaces;
using PlatformService.SyncDataServices.http;

namespace PlatformService
{
    public static class ServiceRegistrations
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPlatformRepo, PlatformRepo>();
            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
        }
    }
}