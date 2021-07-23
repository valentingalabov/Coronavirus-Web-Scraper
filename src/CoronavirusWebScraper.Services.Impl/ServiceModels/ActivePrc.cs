using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.Impl.ServiceModels
{
    public class ActivePrc
    {
        [BsonElement("hospitalized_per_active")]
        public double HospotalizedPerActive { get; set; }

        [BsonElement("icu_per_hospitalized")]
        public double IcuPerHospitalized { get; set; }
    }
}