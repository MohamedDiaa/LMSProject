using LMS.api.Model;

namespace LMS.api.Services
{
    public interface IGenericResponseService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : notnull
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
    }
}
