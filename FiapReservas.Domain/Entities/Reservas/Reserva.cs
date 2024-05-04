using FiapReservas.Domain.Entities.Base;
using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Enums;

namespace FiapReservas.Domain.Entities.Reservas
{
    public class Reserva : EntityBase
    {
        public Mesa Mesa { get; set; }
        public Restaurante restaurante { get; set; }
        public DateTime DataReserva { get; set; }
        public StatusReserva Status { get; set; }
    }
}
