using FiapReservas.Data.Base;
using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;

namespace FiapReservas.Data.Restaurantes
{
    public class RestauranteRepository : BaseRepository<Restaurante>, IRestauranteRepository
    {
        protected override string CollectionName => "Restaurante";
    }
}
