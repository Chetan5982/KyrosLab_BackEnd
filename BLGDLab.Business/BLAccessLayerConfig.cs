using BLGDLab.Business.IServices;
using BLGDLab.Business.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLGDLab.Data
{
    public static class BLAccessLayerConfig
    {
        public static IServiceCollection AddBLAccessLayer(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
