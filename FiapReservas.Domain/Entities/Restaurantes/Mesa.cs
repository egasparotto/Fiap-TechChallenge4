using FiapReservas.Domain.Entities.Base;

namespace FiapReservas.Domain.Entities.Restaurantes
{
    public class Mesa : EntityBase
    {
        public Restaurante Restaurante { get; set; }
        public int QuantidadePessoas { get; set; }
        public IEnumerable<Reserva> Reservas { get; set; }
    }
}
