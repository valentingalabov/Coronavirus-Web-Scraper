namespace CoronavirusWebScraper.Services.ServiceModels
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold information about vaccinated count.
    /// </summary>
    public class VaccinatedServiceModel
    {
        /// <summary>
        /// Gets or sets count of total vaccinated.
        /// </summary>
        [BsonElement("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets count of vaccinated for last 24 hours.
        /// </summary>
        [BsonElement("last")]
        public int Last { get; set; }

        /// <summary>
        /// Gets or sets information of vaccinated for last 24 hours by vaccine type.
        /// </summary>
        [BsonElement("last_by_type")]
        public TotalVaccineByType24ServiceModel LastByType { get; set; }

        /// <summary>
        /// Gets or sets count fully complate vaccination.
        /// </summary>
        [BsonElement("total_completed")]
        public int TotalCompleted { get; set; }
    }
}
