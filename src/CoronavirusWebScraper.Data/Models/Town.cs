using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Data.Models
{
    public class Town
    {
        [BsonElement("confirmed")]
        public ConfirmedByTown Confirmed { get; set; }

        [BsonElement("vaccinated")]
        public Vaccinated Vaccinated { get; set; }
    }
}