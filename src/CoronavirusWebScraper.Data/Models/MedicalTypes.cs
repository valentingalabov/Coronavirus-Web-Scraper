namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold information abaut confirmed cases for types of Medical staff.
    /// </summary>
    public class MedicalTypes
    {
        /// <summary>
        /// Gets or sets the value of confirmed doctors.
        /// </summary>
        [BsonElement("doctor")]
        public int Doctror { get; set; }

        /// <summary>
        /// Gets or sets the value of confirmed nurces.
        /// </summary>
        [BsonElement("nurces")]
        public int Nurces { get; set; }

        /// <summary>
        /// Gets or sets the value of confirmed paramedics1.
        /// </summary>
        [BsonElement("paramedics_1")]
        public int Paramedics_1 { get; set; }

        /// <summary>
        /// Gets or sets the value of confirmed paramedics2.
        /// </summary>
        [BsonElement("paramedics_2")]
        public int Paramedics_2 { get; set; }

        /// <summary>
        /// Gets or sets the value of others medical staff.
        /// </summary>
        [BsonElement("others")]
        public int Others { get; set; }
    }
}
