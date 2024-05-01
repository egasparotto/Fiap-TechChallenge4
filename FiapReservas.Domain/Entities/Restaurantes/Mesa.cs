using FiapReservas.Domain.Entities.Base;

namespace FiapReservas.Domain.Entities.Restaurantes
{
    public class Mesa : EntityBase
    {
        public int QuantidadePessoas { get; set; }
        public IEnumerable<Reserva> Reservas { get; set; }
    }
}
