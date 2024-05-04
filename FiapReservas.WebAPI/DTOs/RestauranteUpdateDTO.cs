namespace FiapReservas.WebAPI.DTOs
{
    public class RestauranteUpdateDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<MesaDTO> Mesas { get; set; }
    }
}
