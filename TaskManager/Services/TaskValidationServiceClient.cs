using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaskManager.Services.Comunication.TaskValidationService;

namespace TaskManager.Services
{
    public class TaskValidationServiceClient : ITaskValidationServiceClient
    {
        private readonly string apiEndpoint;
        private static readonly HttpClient httpClient;
        static TaskValidationServiceClient()
        {
            httpClient = new HttpClient();
        }
        public TaskValidationServiceClient(string apiEndpoint)
        {
            this.apiEndpoint = apiEndpoint;
        }

        public async Task<ValidationResponse> IsTaskNameUniqueAsync(string taskName)
        {
            var baseResponse = new ValidationResponse();
            using (var response = await httpClient.GetAsync($"{apiEndpoint}/IsTaskNameUnique/{taskName}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                baseResponse = JsonConvert.DeserializeObject<ValidationResponse>(apiResponse);
            }
            return baseResponse;
        }

        public ValidationResponse IsTaskNameUnique(string taskName)
        {
            var task = IsTaskNameUniqueAsync(taskName);
            task.Wait();
            return task.Result;
        }
    }
}
