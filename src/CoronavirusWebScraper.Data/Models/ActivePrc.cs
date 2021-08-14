namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold percentage information abaut people hospitalized and intensive care unit.
    /// </summary>
    public class ActivePrc
    {
        /// <summary>
        /// Gets or sets percentage of people for current hospitalized per infected.
        /// </summary>
        [BsonElement("hospitalized_per_active")]
        public double HospotalizedPerActive { get; set; }

        /// <summary>
        /// Gets or sets percentage for current people in intensive care unit per infected.
        /// </summary>
        [BsonElement("icu_per_hospitalized")]
        public double IcuPerHospitalized { get; set; }
    }
}
