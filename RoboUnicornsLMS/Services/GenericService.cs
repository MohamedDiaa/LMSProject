using LMS.api.Model;

namespace RoboUnicornsLMS.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class, IEntity
    {
        private readonly HttpClient _httpClient;
        private readonly string _entityName;

        public GenericService(HttpClient httpClient, string entityName)
        {
            _httpClient = httpClient;
            _entityName = entityName;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"/api/{_entityName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<TEntity>>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/{_entityName}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/{_entityName}", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/{_entityName}/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TEntity>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/{_entityName}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
