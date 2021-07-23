using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.Impl.ServiceModels
{
    public class VaccineType
    {
        [BsonElement("comirnaty")]
        public int Comirnaty { get; set; }

        [BsonElement("moderna")]
        public int Moderna { get; set; }

        [BsonElement("astraZeneca")]
        public int AstraZeneca { get; set; }

        [BsonElement("janssen")]
        public int Janssen { get; set; }
    }
}