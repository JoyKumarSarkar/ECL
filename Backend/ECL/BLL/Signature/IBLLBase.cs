using ECL.Entity;
using ECL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECL.BLL.Signature
{
    public interface IBLLBase
    {
        #region Home

        Menu GetMenu();

        #endregion Home

        #region Dataset

        CommonResponse UpdateDataset(DatasettUpdateRequest datasetRequest);
        DatasetResponse GetDatasetList(string searchText = "");
        DatasetDetailsResponse GetDatasetDetails(int datasetId);
        CommonResponse DeleteDataset(int datasetID);

        #endregion Dataset

        #region RoleMenuMapping

        Admin FetchRoleMenuMappings(int RoleId);
        Admin SaveRoleMenuMappingByRoleId(SaveRoleMenuMappingByRoleId SaveRoleMenuMappingByRoleId);

        #endregion RoleMenuMapping
    }
}
