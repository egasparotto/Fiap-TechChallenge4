using FiapReservas.Domain.Entities.Reservas;
using FiapReservas.Domain.Interfaces.Services.Base;

namespace FiapReservas.Domain.Interfaces.Services.Reservas
{
    public interface IReservaService : IBaseService<Reserva>
    {
        Task<Reserva> Reservar(Reserva reserva);
        Task Confirmar(Reserva reserva);
    }
}
