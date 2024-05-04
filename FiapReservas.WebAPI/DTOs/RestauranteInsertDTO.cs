namespace FiapReservas.WebAPI.DTOs
{
    public class RestauranteInsertDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<MesaDTO> Mesas { get; set; }
    }
}
