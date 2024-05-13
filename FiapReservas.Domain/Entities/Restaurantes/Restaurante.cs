using FiapReservas.Domain.Entities.Base;

namespace FiapReservas.Domain.Entities.Restaurantes
{
    public class Restaurante : EntityBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Telefone { get; set; }
        public IEnumerable<Mesa> Mesas { get; set; }
    }
}
