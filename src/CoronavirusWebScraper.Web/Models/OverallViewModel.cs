namespace CoronavirusWebScraper.Web.Models
{
    public class OverallViewModel
    {
        public TestedViewModel Tested { get; set; }

        public ConfirmedViewModel Confirmed { get; set; }

        public ActiveViewModel Active { get; set; }

        public TotalAndLastViewModel Recovered { get; set; }

        public TotalAndLastViewModel Deceased { get; set; }

        public VaccinatedViewModel Vaccinated { get; set; }

    }
}