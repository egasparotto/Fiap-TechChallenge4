using FiapReservas.Data.Base;
using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;
using MongoDB.Driver;

namespace FiapReservas.Data.Restaurantes
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        protected override string CollectionName => "User";

        public async Task<User> GetByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await GetCollection().Find(filter).FirstOrDefaultAsync();
        }
    }
}
