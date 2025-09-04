using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class PythonService : IPythonService
{
    private readonly IJsonHttpService _JsonHttpService;
    private readonly string _PythonApiBaseUrl;

    public PythonService(IJsonHttpService JsonHttpService, IConfiguration Configuration)
    {
        _JsonHttpService = JsonHttpService;
        _PythonApiBaseUrl = Configuration["PythonApi:BaseUrl"]!;
    }

    public async Task<string> CallPythonAsync(object Payload)
    {
        var response = await _JsonHttpService.PostAsync<object, string>(_PythonApiBaseUrl + "/process", Payload);
        return response;
    }
}
