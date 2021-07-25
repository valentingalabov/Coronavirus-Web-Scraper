using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Web.Models
{
    public class VaccinatedViewModel
    {
        public int Total { get; set; }

        public int Last { get; set; }

        public VaccineTypeViewModel LastByType { get; set; }

        public int TotalCompleted { get; set; }

    }
}