using FiapReservas.Data.Base;
using FiapReservas.Domain.Entities.Reservas;
using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Reservas;
using MongoDB.Driver;

namespace FiapReservas.Data.Reservas
{
    public class ReservaRepository : BaseRepository<Reserva>, IReservaRepository
    {
        protected override string CollectionName => "Reserva";
    }
}
