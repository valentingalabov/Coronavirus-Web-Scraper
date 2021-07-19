using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.Impl.DTO
{
    public class Town
    {
        [BsonElement("confirmed")]
        public ConfirmedByTown Confirmed { get; set; }

        [BsonElement("vaccinated")]
        public Vaccinated Vaccinated { get; set; }
    }
}