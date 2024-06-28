using LMS.api.Model;
using RoboUnicornsLMS.Services;
using RoboUnicornsLMS;
using System.Configuration;

namespace RoboUnicornsLMS.Services
{
    internal interface IActivityRequestService : IGenericRequestService<Activity, int>
    {
    }
    public interface ICourseRequestService : IGenericRequestService<Course, int>
    {
    }
    public interface IDocumentRequestService : IGenericRequestService<Document, int>
    {
    }
    public interface IModuleRequestService : IGenericRequestService<Module, int>
    {
    }
    public interface IGenericRequestService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : notnull
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TKey id);
    }
}
