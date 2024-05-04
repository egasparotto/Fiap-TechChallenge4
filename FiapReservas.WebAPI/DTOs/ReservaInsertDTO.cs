using FiapReservas.Domain.Enums;

namespace FiapReservas.WebAPI.DTOs
{
    public class ReservaInsertDTO
    {
        public Guid RestauranteId { get; set; }
        public int MesaNumero { get; set; }
        public DateTime DataReserva { get; set; }
        public StatusReserva Status { get; set; }
    }
}
