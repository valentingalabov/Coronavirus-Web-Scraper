namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold overall statistical information abaut covid19.
    /// </summary>
    public class Overall
    {
        /// <summary>
        /// Gets or sets information abaut tested people.
        /// </summary>
        [BsonElement("tested")]
        public Tested Tested { get; set; }

        /// <summary>
        /// Gets or sets information abaut confimed cases.
        /// </summary>
        [BsonElement("confirmed")]
        public Confirmed Confirmed { get; set; }

        /// <summary>
        /// Gets or sets information abaut active cases.
        /// </summary>
        [BsonElement("active")]
        public Active Active { get; set; }

        /// <summary>
        /// Gets or sets information abaut recovered cases.
        /// </summary>
        [BsonElement("recovered")]
        public TotalAndLast Recovered { get; set; }

        /// <summary>
        /// Gets or sets information abaut deceased cases.
        /// </summary>
        [BsonElement("deceased")]
        public TotalAndLast Deceased { get; set; }

        /// <summary>
        /// Gets or sets information abaut vaccinated cases.
        /// </summary>
        [BsonElement("vaccinated")]
        public Vaccinated Vaccinated { get; set; }
    }
}
