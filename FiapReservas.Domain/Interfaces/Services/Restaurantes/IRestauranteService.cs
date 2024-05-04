using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Base;

namespace FiapReservas.Domain.Interfaces.Services.Restaurantes
{
    public interface IRestauranteService : IBaseService<Restaurante>
    {
        Task<Mesa> GetMesaByNumero(Guid restauranteId, int mesaNumero);
    }
}
