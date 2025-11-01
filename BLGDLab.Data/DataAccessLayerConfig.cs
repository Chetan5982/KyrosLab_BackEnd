using BLGDLab.Data.IRepository;
using BLGDLab.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLGDLab.Data
{
    public static class DataAccessLayerConfig
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,IConfiguration configuration,string blgdConnString)
        {

            services.AddSingleton<SqlConnectionRepository>(_ => new SqlConnectionRepository(blgdConnString));
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

            return services; 
        }

       
    }
}
