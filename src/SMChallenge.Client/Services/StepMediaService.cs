using SMChallenge.Shared;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SMChallenge.Client.Services
{
    public interface IStepMediaService
    {
        Task<StepMediaModelResult> Arrrange(StepMediaModel model);
    }
    public class StepMediaService : IStepMediaService
    {
        private readonly HttpClient _httpClient;

        public StepMediaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<StepMediaModelResult> Arrrange(StepMediaModel model)
        {
            var result = await _httpClient.PostJsonAsync<StepMediaModelResult>("api/stepmedia", model);
            return result;
        }
    }
}
