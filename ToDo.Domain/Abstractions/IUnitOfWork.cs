using ToDo.Domain.Models;

namespace ToDo.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel;
        Task<int> Complete();
    }
}
