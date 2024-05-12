using FiapReservas.Domain.Enums;

namespace FiapReservas.WebAPI.DTOs
{
    public class ReservarDTO
    {
        public Guid IdRestaurante { get; set; }
        public DateTimeOffset DataReserva { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int QuantidadePessoas { get; set; }
    }
}
