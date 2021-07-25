using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.ServiceModels
{
    public class VaccinatedServiceModel
    {
        [BsonElement("total")]
        public int Total { get; set; }

        [BsonElement("last")]
        public int Last { get; set; }

        [BsonElement("last_by_type")]
        public VaccineTypeServiceModel LastByType { get; set; }

        [BsonElement("total_completed")]
        public int TotalCompleted { get; set; }

    }
}