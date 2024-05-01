using FiapReservas.Data.Base;
using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;

namespace FiapReservas.Data.Restaurantes
{
    public class MesaRepository : BaseRepository<Mesa>, IMesaRepository
    {
        protected override string CollectionName => "Mesa";
    }
}
