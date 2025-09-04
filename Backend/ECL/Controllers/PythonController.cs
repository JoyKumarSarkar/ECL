using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ECL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PythonController : ControllerBase
    {
        private readonly ILogger<PythonService> _Logger;
        private readonly IPythonService _PythonService;
        public PythonController(IPythonService PythonService, ILogger<PythonService> Logger)
        {
            _PythonService = PythonService;
            _Logger = Logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendToPython([FromBody] object data)
        {
            var result = await _PythonService.CallPythonAsync(data);
            return Content(result, "application/json");
        }
    }
}
