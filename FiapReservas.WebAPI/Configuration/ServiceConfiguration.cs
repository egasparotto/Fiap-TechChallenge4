using FiapReservas.Domain.Interfaces.Services.Reservas;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.SMS;
using FiapReservas.Domain.Services.Reservas;
using FiapReservas.Domain.Services.Restaurantes;
using FiapReservas.Domain.Services.SMS;

namespace FiapReservas.WebAPI.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IRestauranteService, RestauranteService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IReservaService, ReservaService>();
            services.AddSingleton<ISmsService, SmsService>();
            return services;
        }
    }
}
