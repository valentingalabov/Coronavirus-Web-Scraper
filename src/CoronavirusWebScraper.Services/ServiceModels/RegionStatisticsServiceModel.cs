namespace CoronavirusWebScraper.Services.ServiceModels
{
    using MongoDB.Bson.Serialization.Attributes;

    public class RegionStatisticsServiceModel
    {
        [BsonElement("confirmed")]
        public TotalAndLastServiceModel Confirmed { get; set; }

        [BsonElement("vaccinated")]
        public VaccinatedServiceModel Vaccinated { get; set; }
    }
}
