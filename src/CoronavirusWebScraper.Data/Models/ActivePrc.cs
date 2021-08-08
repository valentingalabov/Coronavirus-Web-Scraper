namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    public class ActivePrc
    {
        [BsonElement("hospitalized_per_active")]
        public double HospotalizedPerActive { get; set; }

        [BsonElement("icu_per_hospitalized")]
        public double IcuPerHospitalized { get; set; }
    }
}
