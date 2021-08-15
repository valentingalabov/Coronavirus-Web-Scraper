namespace CoronavirusWebScraper.Services.ServiceModels
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold infromation of vaccinated by type of vaccine for last 24 hours.
    /// </summary>
    public class TotalVaccineByType24ServiceModel
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
