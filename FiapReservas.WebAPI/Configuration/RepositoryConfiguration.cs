using FiapReservas.Data.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;

namespace FiapReservas.WebAPI.Configuration
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IRestauranteRepository, RestauranteRepository>();
            services.AddSingleton<IMesaRepository, MesaRepository>();
            return services;
        }
    }
}
