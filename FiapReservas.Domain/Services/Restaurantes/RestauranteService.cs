using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.Domain.Services.Base;

namespace FiapReservas.Domain.Services.Restaurantes
{
    public class RestauranteService : BaseService<Restaurante, IRestauranteRepository>, IRestauranteService
    {
        public RestauranteService(IRestauranteRepository repository) : base(repository)
        {
        }
    }
}
