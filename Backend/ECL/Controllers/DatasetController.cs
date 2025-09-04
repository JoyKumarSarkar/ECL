using ECL.BLL;
using ECL.BLL.Signature;
using ECL.Models;
using ECL.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ECL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatasetController : ControllerBase
    {
        private readonly ILogger<DatasetController> _Logger;
        private readonly IBLLBase _BLL;

        public DatasetController(ILogger<DatasetController> Logger, IBLLBase BLLBase)
        {
            _Logger = Logger;
            _BLL = BLLBase;
        }


        ///<summary>
        /// Get filtered Dataset list besed on search text and activity
        ///</summary>
        /// <param name="searchText">Search keyword (optional)</param>
        ///<returns>Filtered list of Datasets <seealso cref="DatasetResponse"/> DatasetResponse.</returns>
        [HttpGet("GetDatasetList")]
        [ProducesResponseType(typeof(DatasetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetDatasetList(string searchText = "")
        {
            ObjectResult objectResult;

            try
            {
                var result = _BLL.GetDatasetList(searchText = "");
                objectResult = new ObjectResult(result)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }

            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return objectResult;
        }

        ///<summary>
        /// Get a particuler dataset details based on dataset id
        ///</summary>
        ///<param name="datasetId">Dataset Id</param>
        ///<returns>Dataset details <seealso cref="DatasetDetailsResponse"/> DatasetDetailsResponse.</returns>
        [HttpGet("GetDatasetDetails/{datasetId}")]
        [ProducesResponseType(typeof(DatasetDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetDatasetDetails(int datasetId)
        {
            ObjectResult objectResult;
            try
            {
                var result = _BLL.GetDatasetDetails(datasetId);
                objectResult = new ObjectResult(result)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }

            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return objectResult;
        }


        ///<summary>
        /// Update a Dataset
        ///</summary>
        ///<param name="datasettRequest">Dataset Data</param>
        ///<returns>Updated Dataset details <seealso cref="CommonResponse"/> CommonResponse.</returns>
        [HttpPost("UpdateDataset")]
        [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult UpdateDataset(DatasettUpdateRequest datasetRequest)
        {
            ObjectResult objectResult;
            var response = new CommonResponse();
            try
            {
                if (datasetRequest == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Request body is null";
                    objectResult = new ObjectResult(response)
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                    return objectResult;
                }

                if (!Common.IsEmptyDatasetName(datasetRequest.DatasetName))
                {
                    response.IsSuccess = false;
                    response.Message = "Dataset Name can not be empty";
                    objectResult = new ObjectResult(response)
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                    return objectResult;
                }

                var result = _BLL.UpdateDataset(datasetRequest);
                objectResult = new ObjectResult(result)
                {
                    StatusCode = StatusCodes.Status200OK
                };
                return objectResult;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete a Dataset
        /// </summary>
        /// <param name="DatasetID">Dataset Id</param>
        /// <returns>Delete Dataset <seealso cref="CommonResponse"/> CommonResponse.</returns>
        [HttpPost("DeleteDataset")]
        [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult DeleteDataset([FromBody] int DatasetID)
        {
            ObjectResult objectResult;
            var response = new CommonResponse();

            try
            {
                if (DatasetID <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid dataset id";
                    objectResult = new ObjectResult(response)
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                    return objectResult;
                }

                var result = _BLL.DeleteDataset(DatasetID);

                objectResult = new ObjectResult(result)
                {
                    StatusCode = StatusCodes.Status200OK
                };
                return objectResult;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
