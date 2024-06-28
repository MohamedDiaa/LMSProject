using LMS.api.Model;
using System.Net.Http.Json;

namespace RoboUnicornsLMS.Services
{
    public class ActivityRequestService : GenericRequestService<Activity, int>, IActivityRequestService
    {
        public ActivityRequestService(HttpClient httpClient) : base(httpClient) { }
    }
    public class CourseRequestService : GenericRequestService<Course, int>, ICourseRequestService
    {
        public CourseRequestService(HttpClient httpClient) : base(httpClient) { }
    }
    public class DocumentRequestService : GenericRequestService<Document, int>, IDocumentRequestService
    {
        public DocumentRequestService(HttpClient httpClient) : base(httpClient) { }
    }
    public class ModuleRequestService : GenericRequestService<Module, int>, IModuleRequestService
    {
        public ModuleRequestService(HttpClient httpClient) : base(httpClient) { }
    }
    public class GenericRequestService<TEntity, TKey> : IGenericRequestService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : notnull
    {
        private readonly HttpClient _httpClient;
        private readonly string _entityName;

        public GenericRequestService(HttpClient httpClient, string? entityName = null)
        {
            _httpClient = httpClient;
            _entityName = (entityName ?? typeof(TEntity).Name).ToLower();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"/api/{_entityName}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<TEntity>>();
            return result ?? throw new InvalidOperationException("The response was unexpectedly null.");
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            var response = await _httpClient.GetAsync($"/api/{_entityName}/{id}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TEntity>();
            return result ?? throw new InvalidOperationException("The response was unexpectedly null.");
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/{_entityName}", entity);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TEntity>();
            return result ?? throw new InvalidOperationException("The response was unexpectedly null.");
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/{_entityName}/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TEntity>();
            return result ?? throw new InvalidOperationException("The response was unexpectedly null.");
        }

        public async Task<bool> DeleteAsync(TKey id)
        {
            var response = await _httpClient.DeleteAsync($"/api/{_entityName}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
