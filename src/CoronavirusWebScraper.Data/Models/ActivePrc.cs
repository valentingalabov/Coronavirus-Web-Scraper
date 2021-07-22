using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Data.Models
{
    public class ActivePrc
    {
        [BsonElement("hospitalized_per_active")]
        public double HospotalizedPerActive { get; set; }

        [BsonElement("icu_per_hospitalized")]
        public double IcuPerHospitalized { get; set; }
    }
}