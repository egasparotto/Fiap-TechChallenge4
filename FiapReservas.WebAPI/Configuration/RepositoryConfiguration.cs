using FiapReservas.Data.Reservas;
using FiapReservas.Data.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Reservas;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;

namespace FiapReservas.WebAPI.Configuration
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IRestauranteRepository, RestauranteRepository>();            
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IReservaRepository, ReservaRepository>();
            return services;
        }
    }
}
