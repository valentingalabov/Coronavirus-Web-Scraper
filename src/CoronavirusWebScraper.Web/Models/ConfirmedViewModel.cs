namespace CoronavirusWebScraper.Web.Models
{
    public class ConfirmedViewModel
    {
        public int Total { get; set; }

        public TestedByTypeViewModel TotalByType { get; set; }

        public int Last24 { get; set; }

        public TestedByTypeViewModel TotalByType24 { get; set; }

        public MedicalViewModel Medical { get; set; }
    }
}
