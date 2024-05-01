using FiapReservas.Domain.Entities.Base;
using FiapReservas.Domain.Enums;

namespace FiapReservas.Domain.Entities.Restaurantes
{
    public class Reserva : EntityBase
    {
        public Mesa Mesa { get; set; }
        public DateTime DataReserva { get; set; }
        public StatusReserva Status { get; set; }
    }
}
