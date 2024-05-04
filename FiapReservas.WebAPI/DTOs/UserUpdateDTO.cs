namespace FiapReservas.WebAPI.DTOs
{
    public class UserUpdateDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
