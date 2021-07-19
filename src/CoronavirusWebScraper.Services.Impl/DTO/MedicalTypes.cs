using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.Impl.DTO
{
    public class MedicalTypes
    {
        [BsonElement("doctor")]
        public int Doctror { get; set; }

        [BsonElement("nurces")]
        public int Nurces { get; set; }

        [BsonElement("paramedics_1")]
        public int Paramedics_1 { get; set; }

        [BsonElement("paramedics_2")]
        public int Paramedics_2 { get; set; }

        [BsonElement("others")]
        public int Others { get; set; }

    }
}