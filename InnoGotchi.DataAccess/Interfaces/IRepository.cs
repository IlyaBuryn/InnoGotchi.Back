using InnoGotchi.DataAccess.Models;
using System.Linq.Expressions;

namespace InnoGotchi.DataAccess.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<int?> AddAsync(T entity);
        Task<bool> RemoveAsync(int id);
        Task<bool> UpdateAsync(T entity);
        Task<T?> GetOneAsync(Expression<Func<T, bool>>? predicate = null);
        IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null);
        Task<Page<T>> GetAllAsync(int pageNumber, int pageSize,
            Expression<Func<T, bool>>? predicate = null,
            params string[] includeValues);
        Task<T?> GetByIdAsync(int id);
        Task<Page<T>> CreatePageAsync(IQueryable<T> set, int pageNumber, int pageSize);
    }
}
