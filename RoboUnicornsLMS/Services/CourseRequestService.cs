using LMS.api.Model;

namespace RoboUnicornsLMS.Services
{
    public class CourseRequestService : GenericRequestService<Course>, ICourseRequestService
    {
        public CourseRequestService(HttpClient httpClient, IConfiguration configuration, string serviceName)
            : base(httpClient, configuration, serviceName)
        { }

        public async Task<Course?> GetCourseForUserAsync(string userId, CancellationToken cancellation = default)
        {
            return await _httpClient.GetFromJsonAsync<Course>($"{_endpointPath}/User/{userId}");
        }

        public Task<HttpResponseMessage> UpdateAsync(int id, CourseDTO entity, CancellationToken cancellation = default)
        {
            return _httpClient.PutAsJsonAsync($"{_endpointPath}/{id}", entity);
        }
    }
}
