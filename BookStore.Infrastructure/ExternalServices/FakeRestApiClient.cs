using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BookStore.Infrastructure.ExternalServices
{
    public class FakeRestApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;

        public FakeRestApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net/api/v1/");

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>(_serializerOptions);
        }

        public async Task<IEnumerable<T>?> GetAllAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<T>>(_serializerOptions);
        }

        public async Task<T?> PostAsync<T>(string endpoint, T item)
        {
            var content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>(_serializerOptions);
        }

        public async Task<T?> PutAsync<T>(string endpoint, T item)
        {
            var content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>(_serializerOptions);
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }
    }
}