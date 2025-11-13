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
            services.AddScoped<IBlogService, BlogService>();            
            services.AddScoped<IDiamondSearchService, DiamondSearchSevice>();
            return services;
        }
    }
}
