﻿
using Selfra_Core.Base;
using System.Linq.Expressions;


namespace Selft.Contract.Repositories.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Entities { get; }
        IQueryable<T> GetQueryableByProperty(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T?> GetByIdAsync(object id);
        Task<PaginatedList<T>> GetPagingAsync(IQueryable<T> query, int pageIndex, int pageSize);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(object id);
        Task<List<T>> GetAllByPropertyAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T> GetByPropertyAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null);
    }
}
