using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.ServiceModels
{
    public class RegionStatisticsServiceModel
    {
        [BsonElement("confirmed")]
        public TotalAndLastServiceModel Confirmed { get; set; }

        [BsonElement("vaccinated")]
        public VaccinatedServiceModel Vaccinated { get; set; }
    }
}