namespace ECL.Models
{
    public class DatasetResponse : CommonResponse
    {
        public DatasetListResponse [] DatasetList { get; set; } = Array.Empty<DatasetListResponse>();
    }
}
