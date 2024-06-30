using LMS.api.DTO;
using LMS.api.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
    }
}
