using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.Domain.Services.Restaurantes;

namespace FiapReservas.WebAPI.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IRestauranteService, RestauranteService>();
            services.AddSingleton<IMesaService, MesaService>();
            services.AddSingleton<IUserService, UserService>();
            return services;
        }
    }
}
