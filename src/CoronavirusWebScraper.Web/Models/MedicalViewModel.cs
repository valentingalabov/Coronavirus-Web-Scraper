namespace CoronavirusWebScraper.Web.Models
{
    public class MedicalViewModel
    {
        public int Total { get; set; }

        public MedicalTypesViewModel TotalByType { get; set; }

        public int Last24 { get; set; }

        public MedicalTypesViewModel LastByType24 { get; set; }
    }
}