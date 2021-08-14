namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold information abaut confirmed cases total and for last 24 hours for Medical staff.
    /// </summary>
    public class Medical
    {
        /// <summary>
        /// Gets or sets the value of total confirmed cases for Medical staff.
        /// </summary>
        [BsonElement("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the value of total confirmed cases for types of Medical staff.
        /// </summary>
        [BsonElement("total_by_type")]
        public MedicalTypes TotalByType { get; set; }

        /// <summary>
        /// Gets or sets the value of confirmed cases for Medical staff for last 24 hours.
        /// </summary>
        [BsonElement("last")]
        public int Last24 { get; set; }

        /// <summary>
        /// Gets or sets the value of confirmed cases for types of Medical staff for last 24 hours.
        /// </summary>
        [BsonElement("last_by_type")]
        public MedicalTypes LastByType24 { get; set; }
    }
}
