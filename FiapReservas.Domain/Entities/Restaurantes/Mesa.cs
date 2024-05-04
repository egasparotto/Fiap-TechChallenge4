using FiapReservas.Domain.Entities.Base;
using FiapReservas.Domain.Entities.Reservas;

namespace FiapReservas.Domain.Entities.Restaurantes
{
    public class Mesa : EntityBase
    {
        public int QuantidadePessoas { get; set; }
        public int Numero { get; set; }
        public IEnumerable<Reserva> Reservas { get; set; }
    }
}
