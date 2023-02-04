using InnoGotchi.DataAccess.Data;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

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

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null
                ? await _dbSet.FirstOrDefaultAsync()
                : await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null
                ? _dbSet.OrderByDescending(e => e.Id)
                : _dbSet.OrderByDescending(e => e.Id).Where(predicate);
        }

        public async Task<Page<T>> GetAllAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null, params string[] includeValues)
        {
            IQueryable<T> set = _dbSet.OrderByDescending(e => e.Id);
            for (int i = 0; i < includeValues.Length; i++)
                set = set.Include(includeValues[i]);

            if (predicate == null)
                return await CreatePage(set, pageNumber, pageSize);

            set = set.Where(predicate);

            return await CreatePage(set, pageNumber, pageSize);
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(new object[] { id });

        public async Task<bool> RemoveAsync(int id)
        {
            T? entity = await _context.FindAsync<T>(new object[] { id });
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Update(entity);
            if (entity != null)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Page<T>> CreatePage(IQueryable<T> set, int pageNumber, int pageSize)
        {
            return await Page<T>.CreateFromQueryAsync(set, pageNumber, pageSize);
        }
    }
}
