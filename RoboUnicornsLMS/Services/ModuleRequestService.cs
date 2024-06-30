using LMS.api.DTO;
using LMS.api.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RoboUnicornsLMS.Services
{
    public class ModuleRequestService : GenericRequestService<Module>, IModuleRequestService
    {
        public ModuleRequestService(HttpClient httpClient, IConfiguration configuration, string serviceName)
            : base(httpClient, configuration, serviceName)
        { }

        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId, CancellationToken cancellation = default)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Module>>($"{_endpointPath}/Course/{courseId}", cancellation) ??
                Enumerable.Empty<Module>();
        }
    }
}
