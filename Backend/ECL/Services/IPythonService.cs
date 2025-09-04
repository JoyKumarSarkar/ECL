public interface IPythonService
{
    Task<string> CallPythonAsync(object Payload);
}