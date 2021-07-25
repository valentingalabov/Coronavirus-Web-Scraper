namespace CoronavirusWebScraper.Web.Models
{
    public class TestedViewModel
    {
        public int Total { get; set; }

        public TestedByTypeViewModel TotalByType { get; set; }

        public int Last24 { get; set; }

        public TestedByTypeViewModel TotalByType24 { get; set; }
    }
}