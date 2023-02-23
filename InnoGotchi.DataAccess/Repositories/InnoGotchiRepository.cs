using InnoGotchi.DataAccess.Data;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<T?> GetOneAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T?> GetOneByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);

            return await query.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> expression)
        {
            int resultCount;

            if (expression != null)
            {
                resultCount = _dbSet.Where(expression).Count();
            }
            else
            {
                resultCount = _dbSet.Count();
            }

            return await Task.FromResult(resultCount);
        }

        public async Task<IQueryable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<T>> GetManyByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);

            query = query.Where(x => x.Id == id);

            return await Task.FromResult(query);
        }
        
        public async Task<IQueryable<T>> GetManyAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<T>> GetManyAsync(Expression<Func<T, bool>> expression, Func<T, object> orderBy, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy).AsQueryable();
            }

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<T>> GetPageAsync(IQueryable<T> set, int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            set = set.Skip(skip).Take(pageSize);

            return await Task.FromResult(set);
        }

        private IQueryable<T> GetIncludeProperties(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query;
        }
    }
}
