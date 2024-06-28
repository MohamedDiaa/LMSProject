using LMS.api.Model;
using RoboUnicornsLMS.Services;
using RoboUnicornsLMS;
using System.Configuration;

namespace RoboUnicornsLMS.Services
{
    public interface IGenericService<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
