namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    public class PcrAntigenPrc
    {
        [BsonElement("pcr")]
        public double PCR { get; set; }

        [BsonElement("antigen")]
        public double Antigen { get; set; }
    }
}
