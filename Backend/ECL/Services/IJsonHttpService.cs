using System.Threading.Tasks;

public interface IJsonHttpService
{
    Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request);
}
