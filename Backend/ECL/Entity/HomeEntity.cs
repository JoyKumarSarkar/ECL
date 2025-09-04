using ECL.Models;

namespace ECL.Entity
{
    public class MenuDetails
    {
        public int MenuId { get; set; }
        public string? MenuItem { get; set; }
        public string? Url { get; set; }
        public string? Logo { get; set; }
        public double SequenceNo { get; set; }
        public MenuDetails[] Children { get; set; } = [];
    }

    public class Menu : CommonResponse
    {
        public MenuDetails[] MenuDetails { get; set; } = [];
    }
}
