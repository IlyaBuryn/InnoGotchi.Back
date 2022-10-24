using InnoGotchi.DataAccess.Data;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.DataAccess.Repositories
{
    public class InnoGotchiRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly InnoGotchiDbContext _context;
        private readonly DbSet<T> _dbSet;

        public InnoGotchiRepository(InnoGotchiDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<int?> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public IQueryable<T> GetAll() => _dbSet;
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<bool> RemoveAsync(int id)
        {
            T? entity = await _context.FindAsync<T>(id);
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
