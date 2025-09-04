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
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _Logger;
        private readonly IBLLBase _BLL;

        public AdminController(ILogger<AdminController> Logger, IBLLBase IBLLBase)
        {
            _Logger = Logger;
            _BLL = IBLLBase;
        }


        
        [HttpPost("FetchRoleMenuMappings")]
        public Return<RoleMenuMapping[]> FetchRoleMenuMappings([FromBody] int RoleId)
        {
            try
            {
                var result = _BLL.FetchRoleMenuMappings(RoleId);
                if (result.IsSuccess)
                {
                    return new Return<RoleMenuMapping[]> { Data = result.RoleMenuMappingDetails, Success = true, Message = "Success" };
                }
                else
                {
                    return new Return<RoleMenuMapping[]> { Data = null!, Success = false, Message = result.Message };
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
            }

            return new Return<RoleMenuMapping[]> { Data = null!, Success = false, Message = "Internal server error" };
        }


        [HttpPost("SaveRoleMenuMappingByRoleId")]
        public Return<RoleMenuMapping[]> SaveRoleMenuMappingByRoleId([FromBody] SaveRoleMenuMappingByRoleId SaveRoleMenuMappingByRoleId)
        {
            try
            {
                var result = _BLL.SaveRoleMenuMappingByRoleId(SaveRoleMenuMappingByRoleId);
                if (result.IsSuccess)
                {
                    return new Return<RoleMenuMapping[]> { Data = result.RoleMenuMappingDetails, Success = true, Message = "Success" };
                }
                else
                {
                    return new Return<RoleMenuMapping[]> { Data = null!, Success = false, Message = result.Message };
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
            }

            return new Return<RoleMenuMapping[]> { Data = null!, Success = false, Message = "Internal server error" };
        }
    }
}
