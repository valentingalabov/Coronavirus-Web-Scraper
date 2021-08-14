namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold percentage information about confirmed cases.
    /// </summary>
    public class ConfirmedPrc
    {
        /// <summary>
        /// Gets or sets percentage of total confirmed per total tests.
        /// </summary>
        [BsonElement("total_per_tested_prc")]
        public double TotalPerTestedPrc { get; set; }

        /// <summary>
        /// Gets or sets percentage of confirmed per tested for last 24h.
        /// </summary>
        [BsonElement("last_per_tested_prc")]
        public double LastPerTestedPrc { get; set; }

        /// <summary>
        /// Gets or sets confirmed percantage by type of test. 
        /// </summary>
        [BsonElement("total_by_type_prc")]
        public PcrAntigenPrc TotalByTypePrc { get; set; }

        /// <summary>
        /// Gets or sets confirmed percantage by type of test for last 24 hours.
        /// </summary>
        [BsonElement("last_by_type_prc")]
        public PcrAntigenPrc LastByTypePrc { get; set; }

        /// <summary>
        /// Gets or sets percantage about medical staff per confirmed for last 24 hours.
        /// </summary>
        [BsonElement("medical_prc")]
        public double MedicalPcr { get; set; }
    }
}
