namespace ECL.Models
{
    public class DatasettUpdateRequest
    {
        public int datasetID { get; set; }
        public string? DatasetName { get; set; }
        public DateOnly CutOff { get; set; }
        public int IsRelated { get; set; }
        //public DateTime? CreatedOn{ get; set; }
        //public string? CreatedBy { get; set; }
        //public DateTime? ModifiedOn { get; set; }
        //public string? ModifiedBy { get; set; }
    }
}
