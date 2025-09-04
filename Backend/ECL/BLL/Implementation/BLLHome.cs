using ECL.BLL.Signature;
using ECL.Controllers;
using ECL.Data.ECLContext;
using ECL.Entity;
using ECL.Models;
using ECL.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static ECL.Utility.Enums;

namespace ECL.BLL.Implementation
{
    public partial class BLLCommon: IBLLBase
    {
        private readonly ILogger<BLLCommon> _Logger;
        private readonly ECLDBContext DB;
        public BLLCommon(ILogger<BLLCommon> Logger, ECLDBContext eclDBContext)
        {
            _Logger = Logger;
            DB = eclDBContext;
        }

        public Menu GetMenu()
        {
            var response = new Menu();
            try
            {
                var menu = (from mm in DB.TMenuMaster
                            where
                                mm.IsActive == (int)Status.Active
                                && mm.IsVisible == (int)TrueFalse.True
                                && mm.ParentId == null
                            select new MenuDetails
                            {
                                MenuId = mm.MenuId,
                                MenuItem = mm.MenuName,
                                Url = mm.Route,
                                Logo = mm.Logo,
                                SequenceNo = mm.SequenceNo,
                                Children = (from mc in DB.TMenuMaster
                                            where
                                                mc.IsActive == (int)Status.Active
                                                && mc.IsVisible == (int)TrueFalse.True
                                                && mc.ParentId == mm.MenuId
                                            select new MenuDetails
                                            {
                                                MenuId = mc.MenuId,
                                                MenuItem = mc.MenuName,
                                                Url = mc.Route,
                                                Logo = mc.Logo,
                                                SequenceNo = mc.SequenceNo,
                                            })
                                            .OrderBy(mc => mc.SequenceNo)
                                            .ToArray()
                            })
                            .OrderBy(mc => mc.SequenceNo)
                            .ToArray();

                response.MenuDetails = menu;
                return response;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
            }
            return new Menu { MenuDetails = [], IsSuccess = false, Message = "Internal server error" };

        }
    }
}