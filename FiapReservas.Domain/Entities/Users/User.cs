using FiapReservas.Domain.Entities.Base;
using FiapReservas.Domain.Utils.Cryptography;
using System.Text.Json.Serialization;

namespace FiapReservas.Domain.Entities.Restaurantes
{
    public class User : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        
        [JsonIgnore]
        public string Password { get; set; }

        public bool ValidatePassword(string password)
        {
            return PasswordCryptography.Validate(password, Password);
        }
    }
}
