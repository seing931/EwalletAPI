using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace EWallet.Api.Clients
{
    public interface IEWalletApiClient
    {
        Task<T> PostAsync<T>(string url, object payload);
        Task<T> GetAsync<T>(string url, string key);
        Task<T> PutAsync<T>(string url, string key, object payload);
    }

    public class EWalletApiClient : IEWalletApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public EWalletApiClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<T> PostAsync<T>(string url, object payload)
        {
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var respString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API {(int)response.StatusCode}: {respString}");
            }

            return JsonSerializer.Deserialize<T>(respString,new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public async Task<T> GetAsync<T>(string url, string key)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("token", key);

            var response = await _httpClient.SendAsync(request);
            var respString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API {(int)response.StatusCode}: {respString}");
            }

            return JsonSerializer.Deserialize<T>(respString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public async Task<T> PutAsync<T>(string url, string key, object payload)
        {
            using var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Add("token", key);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"PUT request failed. Status: {(int)response.StatusCode}, Response: {responseContent}");
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseContent);
        }
    }
}
