using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class JsonHttpService : IJsonHttpService
{
    private readonly HttpClient _httpClient;

    public JsonHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(result)!;
    }
}
