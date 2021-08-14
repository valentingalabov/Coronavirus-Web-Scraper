namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold information for vaccinated by vaccine type.
    /// </summary>
    public class VaccineType
    {
        /// <summary>
        /// Gets or sets count of vaccinated with comirnaty.
        /// </summary>
        [BsonElement("comirnaty")]
        public int Comirnaty { get; set; }

        /// <summary>
        /// Gets or sets count of vaccinated with moderna.
        /// </summary>
        [BsonElement("moderna")]
        public int Moderna { get; set; }

        /// <summary>
        /// Gets or sets count of vaccinated with astrazeneca.
        /// </summary>
        [BsonElement("astrazeneca")]
        public int AstraZeneca { get; set; }

        /// <summary>
        /// Gets or sets count of vaccinated with janssen.
        /// </summary>
        [BsonElement("janssen")]
        public int Janssen { get; set; }
    }
}
