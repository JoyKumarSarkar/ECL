namespace ECL.Models
{
    public class DatasetListResponse
    {
        public int DatasetID { get; set; }
        public string? DatasetName { get; set; }
        public string? Source { get; set; }
        public string? Cutoff { get; set; }
        public bool IsRelated { get; set; }
        public int FileCount { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
    }
}
