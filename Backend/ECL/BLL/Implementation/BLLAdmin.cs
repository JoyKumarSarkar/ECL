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
    public partial class BLLCommon : IBLLBase
    {
        public Admin FetchRoleMenuMappings(int RoleId)
        {
            var response = new Admin();
            try
            {
                var roleMenuMappingDetail = (from rmm in DB.TRoleMenuMapping
                                             join rm in DB.TRoleMaster on rmm.RoleId equals rm.RoleId
                                             join mm in DB.TMenuMaster on rmm.MenuId equals mm.MenuId
                                             where rmm.RoleId == RoleId && rm.IsActive == (int)Status.Active && rmm.IsActive == (int)Status.Active
                                                && mm.IsActive == (int)Status.Active && mm.ParentId == null
                                             select new RoleMenuMapping
                                             {
                                                 RoleMenuMappingId = rmm.MappingId,
                                                 MenuId = rmm.MenuId,
                                                 MenuItem = mm.MenuName,
                                                 IsChecked = mm.IsVisible,
                                                 MenuTag = mm.MenuTag,
                                                 Children = (from rmmc in DB.TRoleMenuMapping
                                                             join rmc in DB.TRoleMaster on rmmc.RoleId equals rmc.RoleId
                                                             join mmc in DB.TMenuMaster on rmmc.MenuId equals mmc.MenuId
                                                             where rmmc.RoleId == RoleId && rmc.IsActive == (int)Status.Active && rmmc.IsActive == (int)Status.Active
                                                                && mmc.IsActive == (int)Status.Active && mmc.ParentId != null
                                                             select new RoleMenuMapping
                                                             {
                                                                 RoleMenuMappingId = rmmc.MappingId,
                                                                 MenuId = rmmc.MenuId,
                                                                 MenuItem = mmc.MenuName,
                                                                 IsChecked = mmc.IsVisible,
                                                                 MenuTag = mmc.MenuTag,
                                                             })
                                                             .ToArray()
                                             })
                                             .ToArray();

                foreach (var item in roleMenuMappingDetail)
                {
                    if (item.Children.Length != 0 && item.Children.Any(c => c.IsChecked == (int)TrueFalse.True))
                    {
                        item.IsChecked = (int)TrueFalse.True;
                    }
                }

                response.RoleMenuMappingDetails = roleMenuMappingDetail;
                return response;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
            }

            return new Admin { RoleMenuMappingDetails = [], IsSuccess = false, Message = "Internal server error" };

        }

        public Admin SaveRoleMenuMappingByRoleId(SaveRoleMenuMappingByRoleId SaveRoleMenuMappingByRoleId)
        {
            var response = new Admin();

            try
            {
                var existingRole = DB.TRoleMaster.Where(rm => rm.RoleId == SaveRoleMenuMappingByRoleId.RoleId && rm.IsActive == (int)Status.Active).FirstOrDefault();

                if (existingRole == null)
                    return new Admin { RoleMenuMappingDetails = [], IsSuccess = false, Message = "Role does not exist" };

                if (SaveRoleMenuMappingByRoleId.RoleMenuMappings.Length == 0)
                    return new Admin { RoleMenuMappingDetails = [], IsSuccess = false, Message = "No menu items to save" };

                var existingRoleMenuMappings = DB.TRoleMenuMapping.Where(rmm => rmm.RoleId == SaveRoleMenuMappingByRoleId.RoleId && rmm.IsActive == (int)Status.Active).ToArray();
                if (existingRoleMenuMappings.Length == 0)
                {
                    var newRoleMenuMappings = new List<TRoleMenuMapping>();
                    foreach (var menuMapping in SaveRoleMenuMappingByRoleId.RoleMenuMappings)
                    {
                        newRoleMenuMappings.Add(new TRoleMenuMapping
                        {
                            RoleId = SaveRoleMenuMappingByRoleId.RoleId,
                            MenuId = menuMapping.MenuId,
                            IsActive = menuMapping.IsChecked,
                            CreatedBy = (int)CreatedBy.Joy,
                            CreatedOn = DateTime.Now,
                        });

                        if (menuMapping.Children.Length > 0)
                        {
                            foreach (var child in menuMapping.Children)
                            {
                                newRoleMenuMappings.Add(new TRoleMenuMapping
                                {
                                    RoleId = SaveRoleMenuMappingByRoleId.RoleId,
                                    MenuId = child.MenuId,
                                    IsActive = child.IsChecked,
                                    CreatedBy = (int)CreatedBy.Joy,
                                    CreatedOn = DateTime.Now,
                                });
                            }
                        }
                    }

                    if (newRoleMenuMappings.Count > 0)
                        DB.TRoleMenuMapping.AddRange(newRoleMenuMappings);
                }
                else
                {
                    foreach (var menuMapping in SaveRoleMenuMappingByRoleId.RoleMenuMappings)
                    {
                       var existingMapping = existingRoleMenuMappings.FirstOrDefault(rmm => rmm.MenuId == menuMapping.MenuId);
                        if (existingMapping != null)
                        {
                            existingMapping.IsActive = menuMapping.IsChecked;
                            existingMapping.ModifiedBy = (int)CreatedBy.Kuntal;
                            existingMapping.ModifiedOn = DateTime.Now;
                        }

                        if (menuMapping.Children.Length > 0)
                        {
                            foreach (var child in menuMapping.Children)
                            {
                                var existingChildMapping = existingRoleMenuMappings.FirstOrDefault(rmm => rmm.MenuId == child.MenuId);
                                if (existingChildMapping != null)
                                {
                                    existingChildMapping.IsActive = child.IsChecked;
                                    existingChildMapping.ModifiedBy = (int)CreatedBy.Kuntal;
                                    existingChildMapping.ModifiedOn = DateTime.Now;
                                }
                            }
                        }
                    }
                }
                DB.SaveChanges();

                var roleMenuMappingDetail = (from rmm in DB.TRoleMenuMapping
                                             join rm in DB.TRoleMaster on rmm.RoleId equals rm.RoleId
                                             join mm in DB.TMenuMaster on rmm.MenuId equals mm.MenuId
                                             where rmm.RoleId == SaveRoleMenuMappingByRoleId.RoleId && rm.IsActive == (int)Status.Active && rmm.IsActive == (int)Status.Active
                                                && mm.IsActive == (int)Status.Active && mm.ParentId == null
                                             select new RoleMenuMapping
                                             {
                                                 RoleMenuMappingId = rmm.MappingId,
                                                 MenuId = rmm.MenuId,
                                                 MenuItem = mm.MenuName,
                                                 IsChecked = mm.IsVisible,
                                                 MenuTag = mm.MenuTag,
                                                 Children = (from rmmc in DB.TRoleMenuMapping
                                                             join rmc in DB.TRoleMaster on rmmc.RoleId equals rmc.RoleId
                                                             join mmc in DB.TMenuMaster on rmmc.MenuId equals mmc.MenuId
                                                             where rmmc.RoleId == SaveRoleMenuMappingByRoleId.RoleId && rmc.IsActive == (int)Status.Active && rmmc.IsActive == (int)Status.Active
                                                                && mmc.IsActive == (int)Status.Active && mmc.ParentId != null
                                                             select new RoleMenuMapping
                                                             {
                                                                 RoleMenuMappingId = rmmc.MappingId,
                                                                 MenuId = rmmc.MenuId,
                                                                 MenuItem = mmc.MenuName,
                                                                 IsChecked = mmc.IsVisible,
                                                                 MenuTag = mmc.MenuTag,
                                                             })
                                                             .ToArray()
                                             })
                                            .ToArray();

                foreach (var item in roleMenuMappingDetail)
                {
                    if (item.Children.Length != 0 && item.Children.Any(c => c.IsChecked == (int)TrueFalse.True))
                    {
                        item.IsChecked = (int)TrueFalse.True;
                    }
                }

                response.RoleMenuMappingDetails = roleMenuMappingDetail;
                return response;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
            }

            return new Admin { RoleMenuMappingDetails = [], IsSuccess = false, Message = "Internal server error" };
        }

    }
}