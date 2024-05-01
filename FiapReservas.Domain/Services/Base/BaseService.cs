using FiapReservas.Domain.Entities.Base;
using FiapReservas.Domain.Interfaces.Repositories.Base;
using FiapReservas.Domain.Interfaces.Services.Base;

using System.Linq.Expressions;

namespace FiapReservas.Domain.Services.Base
{
    public abstract class BaseService<T, TRepository> : IBaseService<T>
        where T : EntityBase
        where TRepository : IBaseRepository<T>
    {

        protected TRepository Repository { get; }

        protected BaseService(TRepository repository)
        {
            Repository = repository;
        }

        public async Task Delete(Guid id)
        {
            await Repository.Delete(id);
        }

        public async Task<T> Get(Guid id)
        {
            return await Repository.Get(id);
        }

        public async Task Insert(T entity)
        {
            await Repository.Insert(entity);
        }

        public async Task<IEnumerable<T>> List(Expression<Func<T, bool>> filter)
        {
            return await Repository.List(filter);
        }

        public async Task Update(T entity)
        {
            await Repository.Update(entity);
        }
    }
}
