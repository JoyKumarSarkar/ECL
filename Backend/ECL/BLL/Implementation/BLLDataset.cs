using ECL.BLL.Signature;
using ECL.Controllers;
using ECL.Data.ECLContext;
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
        public DatasetResponse GetDatasetList(string searchText)
        {
            var datasetResponse = new DatasetResponse();

            try
            {
                var datasetList = (from ds in DB.TDataset
                                   join df in DB.TDatafile.Where(df => df.IsActive == (int)Status.Active)
                                   on ds.DatasetID equals df.DatasetId
                                   into lfjn
                                   where ds.IsActive == (int)Status.Active
                                   select new DatasetListResponse
                                   {
                                       DatasetID = ds.DatasetID,
                                       DatasetName = ds.DatasetName,
                                       Source = Common.GetEnumDescription<Source>(ds.Source),
                                       //Cutoff = ds.CutOff.ToShortDateString(),
                                       Cutoff = ds.CutOff.ToString("yyyy-MM-dd"),
                                       FileCount = lfjn.Count(),
                                       IsRelated = ds.IsRelated == (int)TrueFalse.True,
                                       CreatedBy = Common.GetEnumDescription<ModifiedBy>(ds.CreatedBy),
                                       //CreatedOn = ds.CreatedOn.ToShortDateString(),
                                       CreatedOn = ds.CreatedOn.ToString("yyyy-MM-dd"),
                                   }).ToArray();


                datasetResponse.IsSuccess = true;
                datasetResponse.Message = datasetList.Any() ? "Dataset List Found" : "No dataset matched the criteria";
                datasetResponse.DatasetList = datasetList;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "SQL Error in GetStudentList");
                throw;
            }
            return datasetResponse;
        }

        public DatasetDetailsResponse GetDatasetDetails(int datasetID)
        {
            var datasetResponse = new DatasetDetailsResponse();
            try
            {
                var datasetDetail = (from ds in DB.TDataset
                                     where ds.DatasetID == datasetID
                                           && ds.IsActive == (int)Status.Active
                                     select new DatasetDetailListResponse
                                     {
                                         DatasetName = ds.DatasetName,
                                         Cutoff = ds.CutOff.ToShortDateString(),
                                         IsRelated = ds.IsRelated == (int)TrueFalse.True
                                     }).FirstOrDefault();

                if (datasetDetail != null)
                {
                    datasetResponse.IsSuccess = true;
                    datasetResponse.Message = "Data fetched successfully.";
                    datasetResponse.DatasetDetil = datasetDetail;
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error in fetchingf dataset detail");
                datasetResponse.IsSuccess = false;
                datasetResponse.Message = "An error occurred while fetching data.";
            }

            return datasetResponse;
        }



        public CommonResponse UpdateDataset(DatasettUpdateRequest datasetRequest)
        {
            var response = new CommonResponse();

            try
            {
                if (datasetRequest.datasetID == 0)
                {
                    var dataset = new TDataset
                    {
                        DatasetName = datasetRequest.DatasetName,
                        CutOff = datasetRequest.CutOff,
                        IsRelated = datasetRequest.IsRelated,
                        Source = (int)Source.Api,
                        CreatedOn = DateOnly.FromDateTime(DateTime.Now),
                        CreatedBy = (int)CreatedBy.Joy,
                        ModifiedOn = null,
                        ModifiedBy = null,
                        IsActive = 1
                    };

                    _ = DB.TDataset.Add(dataset);
                    _ = DB.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = $"{dataset.DatasetName} added successfully.";
                }
                else
                {
                    var dataset = DB.TDataset.FirstOrDefault(x => x.DatasetID == datasetRequest.datasetID);
                    if (dataset != null)
                    {
                        dataset.DatasetName = datasetRequest.DatasetName;
                        dataset.CutOff = datasetRequest.CutOff;
                        dataset.IsRelated = datasetRequest.IsRelated;
                        dataset.Source = (int)Source.Manual;
                        dataset.ModifiedOn = DateOnly.FromDateTime(DateTime.Now);
                        dataset.ModifiedBy = (int)ModifiedBy.Joy;
                        DB.TDataset.Update(dataset);
                        DB.SaveChanges();

                        response.IsSuccess = true;
                        response.Message = $"{dataset.DatasetName} updated successfully.";
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Dataset not found.";
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error in UpdateStudent");
                response.IsSuccess = false;
                response.Message = "An error occurred: " + ex.Message;
            }

            return response;
        }


        public CommonResponse DeleteDataset(int datasetID)
        {
            var response = new CommonResponse();

            try
            {
                var dataset = DB.TDataset.FirstOrDefault(x => x.DatasetID == datasetID);

                if (dataset != null)
                {
                    dataset.IsActive = 0;
                    dataset.ModifiedOn = DateOnly.FromDateTime(DateTime.Now);
                    dataset.ModifiedBy = (int)ModifiedBy.Joy;

                    DB.TDataset.Update(dataset);
                    DB.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = $"{dataset.DatasetName} deleted successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Dataset not found.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error in DeleteDataset");
                response.IsSuccess = false;
                response.Message = "An error occurred: " + ex.Message;
            }

            return response;
        }
    }
}