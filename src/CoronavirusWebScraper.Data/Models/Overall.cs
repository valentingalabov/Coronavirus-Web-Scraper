namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold overall statistical information about covid19.
    /// </summary>
    public class Overall
    {
        /// <summary>
        /// Gets or sets values about tested people.
        /// </summary>
        [BsonElement("tested")]
        public Tested Tested { get; set; }

        /// <summary>
        /// Gets or sets values about confirmed cases.
        /// </summary>
        [BsonElement("confirmed")]
        public Confirmed Confirmed { get; set; }

        /// <summary>
        /// Gets or sets values about active cases.
        /// </summary>
        [BsonElement("active")]
        public Active Active { get; set; }

        /// <summary>
        /// Gets or sets values about recovered cases.
        /// </summary>
        [BsonElement("recovered")]
        public TotalAndLast Recovered { get; set; }

        /// <summary>
        /// Gets or sets values about deceased cases.
        /// </summary>
        [BsonElement("deceased")]
        public TotalAndLast Deceased { get; set; }

        /// <summary>
        /// Gets or sets values about vaccinated cases.
        /// </summary>
        [BsonElement("vaccinated")]
        public Vaccinated Vaccinated { get; set; }
    }
}
