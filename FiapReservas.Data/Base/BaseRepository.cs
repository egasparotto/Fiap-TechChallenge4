using FiapReservas.Domain.Entities.Base;
using FiapReservas.Domain.Interfaces.Repositories.Base;

using MongoDB.Driver;

using System.Linq.Expressions;

namespace FiapReservas.Data.Base
{
    public abstract class BaseRepository<T> : MongoDbConnection, IBaseRepository<T>
        where T : EntityBase
    {
        protected abstract string CollectionName { get; }

        protected virtual IMongoCollection<T> GetCollection()
        {

            var database = GetDatabase();
            return database.GetCollection<T>(CollectionName);
        }

        public virtual async Task Delete(Guid id)
        {
            await GetCollection().DeleteOneAsync<T>(x => x.Id == id);
        }

        public virtual async Task<T> Get(Guid id)
        {
            return await GetCollection().Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task Insert(T entity)
        {
            await GetCollection().InsertOneAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> List(Expression<Func<T, bool>> filter)
        {
            return await GetCollection().Find(filter).ToListAsync();
        }

        public virtual async Task Update(T entity)
        {
            await GetCollection().ReplaceOneAsync<T>(x => x.Id == entity.Id, entity);
        }
    }
}
