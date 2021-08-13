namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold percentage information abaut hospitalized and intensive care people.
    /// </summary>
    public class ActivePrc
    {
        [BsonElement("hospitalized_per_active")]
        public double HospotalizedPerActive { get; set; }

        [BsonElement("icu_per_hospitalized")]
        public double IcuPerHospitalized { get; set; }
    }
}
