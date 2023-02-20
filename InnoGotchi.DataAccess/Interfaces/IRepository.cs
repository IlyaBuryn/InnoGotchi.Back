﻿using InnoGotchi.DataAccess.Models;
using System.Linq.Expressions;

namespace InnoGotchi.DataAccess.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<int?> AddAsync(T entity);
        public Task<bool> RemoveAsync(int id);
        public Task<bool> UpdateAsync(T entity);
        public Task<T?> GetOneAsync(Expression<Func<T, bool>>? predicate = null);
        public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null);
        public Task<Page<T>> GetAllAsync(int pageNumber, int pageSize,
            Expression<Func<T, bool>>? predicate = null,
            params string[] includeValues);
        public Task<T?> GetByIdAsync(int id);
        public Task<Page<T>> CreatePageAsync(IQueryable<T> set, int pageNumber, int pageSize);
    }
}
