using InnoGotchi.DataAccess.Models;
using System.Linq.Expressions;

namespace InnoGotchi.DataAccess.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<int?> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> RemoveAsync(int id);
        Task<int> GetCountAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetOneAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);
        Task<T?> GetOneByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
        Task<IQueryable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<IQueryable<T>> GetManyByIdAsync(int id,
            params Expression<Func<T, object>>[] includeProperties);
        Task<IQueryable<T>> GetManyAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includeProperties);
        Task<IQueryable<T>> GetManyAsync(Expression<Func<T, bool>> expression,
            Func<T, object> orderBy,
            params Expression<Func<T, object>>[] includeProperties);
        Task<IQueryable<T>> GetPageAsync(IQueryable<T> set, int pageNumber, int pageSize);
    }
}
