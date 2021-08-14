namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold information about confirmed cases total and for last 24 hours for Medical staff.
    /// </summary>
    public class Medical
    {
        /// <summary>
        /// Gets or sets count of total confirmed cases for Medical staff.
        /// </summary>
        [BsonElement("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the values of total confirmed cases by types of Medical staff.
        /// </summary>
        [BsonElement("total_by_type")]
        public MedicalTypes TotalByType { get; set; }

        /// <summary>
        /// Gets or sets count of confirmed cases for Medical staff for last 24 hours.
        /// </summary>
        [BsonElement("last")]
        public int Last24 { get; set; }

        /// <summary>
        /// Gets or sets the values of confirmed cases by types of Medical staff for last 24 hours.
        /// </summary>
        [BsonElement("last_by_type")]
        public MedicalTypes LastByType24 { get; set; }
    }
}
