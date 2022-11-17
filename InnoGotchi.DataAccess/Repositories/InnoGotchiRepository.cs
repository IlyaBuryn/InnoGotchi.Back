﻿using InnoGotchi.DataAccess.Components;
using InnoGotchi.DataAccess.Data;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

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

        public async Task<T> GetOneAsync(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate == null)
                return _dbSet.FirstOrDefault();

            return _dbSet.FirstOrDefault(predicate);
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate == null)
                return _dbSet.OrderByDescending(e => e.Id);

            return _dbSet.OrderByDescending(e => e.Id).Where(predicate);
        }

        public async Task<Page<T>> GetAllAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null, params string[] includeValues)
        {
            IQueryable<T> set = _dbSet.OrderByDescending(e => e.Id);
            for (int i = 0; i < includeValues.Length; i++)
                set = set.Include(includeValues[i]);

            if (predicate == null)
                return await Page<T>.CreateFromQueryAsync(
                    set, pageNumber, pageSize);

            set = set.Where(predicate);

            return await Page<T>.CreateFromQueryAsync(set, pageNumber, pageSize);
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
    }
}
