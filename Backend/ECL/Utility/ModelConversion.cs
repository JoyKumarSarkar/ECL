using ECL.Data.ECLContext;
using ECL.Models;
using static ECL.Utility.Enums;

namespace ECL.Utility
{
    public class ModelConversion
    {
        public static DatasetListResponse ConvertDatasetDBModelToDatasetGetResponse (TDataset Dataset)
        { 
            var DatasetGetResponse = new DatasetListResponse()
            {
                DatasetName = Dataset.DatasetName,
                Source = Common.GetEnumDescription<Source>(Dataset.Source),
                Cutoff = Dataset.CutOff.ToShortDateString(),
                //FileCount = Datafiles.Length,
                IsRelated = Dataset.IsRelated == (int)TrueFalse.True,
                CreatedBy = Common.GetEnumDescription<ModifiedBy>(Dataset.CreatedBy),
                CreatedOn = Dataset.CreatedOn.ToShortDateString(),
            };

            return DatasetGetResponse;
        }
    }
}
