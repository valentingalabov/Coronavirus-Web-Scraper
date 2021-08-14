namespace CoronavirusWebScraper.Services.ServiceModels
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold statistical information about region.
    /// </summary>
    public class RegionStatisticsServiceModel
    {
        /// <summary>
        /// Gets or sets information about confirmed cases for region.
        /// </summary>
        [BsonElement("confirmed")]
        public TotalAndLastServiceModel Confirmed { get; set; }

        /// <summary>
        /// Gets or sets information about vaccinated for region.
        /// </summary>
        [BsonElement("vaccinated")]
        public VaccinatedServiceModel Vaccinated { get; set; }
    }
}
