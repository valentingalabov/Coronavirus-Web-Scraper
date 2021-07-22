using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Data.Models
{
    public class ConfirmedPrc
    {
        [BsonElement("total_per_tested_prc")]
        public double TotalPerTestedPrc { get; set; }

        [BsonElement("last_per_tested_prc")]
        public double LastPerTestedPrc { get; set; }

        [BsonElement("total_by_type_prc")]
        public PcrAntigenPrc TotalByTypePrc { get; set; }

        [BsonElement("last_by_type_prc")]
        public PcrAntigenPrc LastByTypePrc { get; set; }

        [BsonElement("medical_prc")]
        public double MedicalPcr { get; set; }
    }
}