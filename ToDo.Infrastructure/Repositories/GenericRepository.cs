﻿using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Abstractions;
using ToDo.Domain.Models;
using ToDo.Domain.Specifications;
using ToDo.Infrastructure.Context;
using ToDo.Infrastructure.Specifications;
using System.Linq.Expressions;

namespace ToDo.Infrastructure.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ApplicationDBContext _context;
        internal DbSet<T> _entity;
        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public async System.Threading.Tasks.Task Add(T entity) => await _entity.AddAsync(entity);
        public async System.Threading.Tasks.Task AddRange(IEnumerable<T> entities) => await _entity.AddRangeAsync(entities);
        public void Update(T entity) => _entity.Update(entity);
        public void UpdateRange(IEnumerable<T> entity) => _entity.UpdateRange(entity);
        public void Delete(T entity) => _entity.Remove(entity);
        public void DeleteRange(IEnumerable<T> entity) => _entity.RemoveRange(entity);
        public IReadOnlyList<T> Get() => _entity.AsNoTracking().ToList();
        public (IQueryable<T> data, int count) GetWithSpec(BaseSpecifications<T> specifications) => SpecificationEvaluator<T>.GetQuery(_entity, specifications);
        public async Task<T?> GetById(params object[] idValues) => await _entity.FindAsync(idValues);
        public (T? data, int count) GetEntityWithSpec(BaseSpecifications<T> specifications)
        {
            var result = SpecificationEvaluator<T>.GetQuery(_entity, specifications);

            var data = result.data.FirstOrDefault();

            var count = data == null ? 0 : 1;

            return (data, count);
        }
        public async Task<T?> GetByGuid(Guid id) => await _entity.FindAsync(id);
        public async Task<T?> GetObj(Expression<Func<T, bool>> filter) => await _entity.AsQueryable<T>().FirstOrDefaultAsync(filter);
        public async Task<bool> IsExist(Expression<Func<T, bool>> filter) => await _entity.AnyAsync(filter);
        public async Task<bool> Save() => await _context.SaveChangesAsync() > 0;

    }
}
