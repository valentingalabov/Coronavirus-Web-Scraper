namespace CoronavirusWebScraper.Web.Models
{
    public class RegionStatisticsViewModel
    {
        public TotalAndLastViewModel Confirmed { get; set; }

        public VaccinatedViewModel Vaccinated { get; set; }
    }
}
