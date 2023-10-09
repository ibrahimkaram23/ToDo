using ToDo.Domain.Specifications;
using System.Linq.Expressions;

namespace ToDo.Domain.Abstractions
{
    public interface IGenericRepository<T> where T : Models.BaseModel
    {
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        IReadOnlyList<T> Get();
        (IQueryable<T> data, int count) GetWithSpec(BaseSpecifications<T> specifications);
        Task<T?> GetById(params object[] idValues);
        (T? data, int count) GetEntityWithSpec(BaseSpecifications<T> specifications);
        Task<T?> GetObj(Expression<Func<T, bool>> filter);
        Task<bool> IsExist(Expression<Func<T, bool>> filter);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task<bool> Save();
    }
}
