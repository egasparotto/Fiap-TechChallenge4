using System.Linq.Expressions;

namespace FiapReservas.Domain.Interfaces.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> List(Expression<Func<T, bool>> filter);
        Task<T> Get(Guid id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(Guid id);

    }
}
