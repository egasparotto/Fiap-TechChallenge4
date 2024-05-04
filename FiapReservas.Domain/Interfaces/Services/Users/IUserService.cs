using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Base;

namespace FiapReservas.Domain.Interfaces.Services.Restaurantes
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> GetByEmail(string email);
    }
}
