using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.Domain.Services.Base;

namespace FiapReservas.Domain.Services.Restaurantes
{
    public class RestauranteService : BaseService<Restaurante, IRestauranteRepository>, IRestauranteService
    {
        private readonly IRestauranteRepository _repository;

        public RestauranteService(IRestauranteRepository repository) : base(repository)
        {
            this._repository = repository;
        }

        public Task<Mesa> GetMesaByNumero(Guid restauranteId, int mesaNumero)
        {
            return _repository.GetMesaByNumero(restauranteId, mesaNumero);
        }
    }
}
