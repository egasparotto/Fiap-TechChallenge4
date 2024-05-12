using FiapReservas.Domain.Entities.Base;
using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Enums;

namespace FiapReservas.Domain.Entities.Reservas
{
    public class Reserva : EntityBase
    {
        public Restaurante Restaurante { get; set; }
        public DateTimeOffset DataReserva { get; set; }
        public StatusReserva Status { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int QuantidadePessoas { get; set; }
    }
}
