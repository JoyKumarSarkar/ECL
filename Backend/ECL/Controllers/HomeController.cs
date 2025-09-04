using ECL.BLL;
using ECL.BLL.Signature;
using ECL.Entity;
using ECL.Models;
using ECL.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ECL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _Logger;
        private readonly IBLLBase _BLL;

        public HomeController(ILogger<HomeController> Logger, IBLLBase IBLLBase)
        {
            _Logger = Logger;
            _BLL = IBLLBase;
        }


        
        [HttpGet("GetMenu")]
        public Return<MenuDetails[]> GetMenu()
        {
            try
            {
                var result = _BLL.GetMenu();
                if (result.IsSuccess)
                {
                    return new Return<MenuDetails[]> { Data = result.MenuDetails, Success = true, Message = "Success" };
                }
                else
                {
                    return new Return<MenuDetails[]> { Data = null!, Success = false, Message = result.Message };
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
            }

            return new Return<MenuDetails[]> { Data = null!, Success = false, Message = "Internal server error" };
        }
    }
}
