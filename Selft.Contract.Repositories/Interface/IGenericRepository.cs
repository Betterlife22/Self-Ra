
using System.Linq.Expressions;


namespace Selft.Contract.Repositories.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Entities { get; }
        Task<T?> GetByIdAsync(object id);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(object id);
    }
}
