using ECL.Models;

namespace ECL.Models
{
    public class DatasetDetailsResponse : CommonResponse
    {
        public DatasetDetailListResponse? DatasetDetil { get; set; }
    }
}