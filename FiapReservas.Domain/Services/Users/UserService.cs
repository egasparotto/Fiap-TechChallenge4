using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.Domain.Services.Base;

namespace FiapReservas.Domain.Services.Restaurantes
{
    public class UserService : BaseService<User, IUserRepository>, IUserService
    {
        IUserRepository _repository;
        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Task<User> GetByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }
    }
}
