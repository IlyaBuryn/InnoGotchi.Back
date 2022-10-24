using InnoGotchi.DataAccess.Models;

namespace InnoGotchi.DataAccess.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<int?> AddAsync(T entity);
        public Task<bool> RemoveAsync(int id);
        public Task UpdateAsync(T entity);
        public IQueryable<T> GetAll();
        public Task<T?> GetByIdAsync(int id);
    }
}
