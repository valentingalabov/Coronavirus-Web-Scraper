namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    public class Overall
    {
        [BsonElement("tested")]
        public Tested Tested { get; set; }

        [BsonElement("confirmed")]
        public Confirmed Confirmed { get; set; }

        [BsonElement("active")]
        public Active Active { get; set; }

        [BsonElement("recovered")]
        public TotalAndLast Recovered { get; set; }

        [BsonElement("deceased")]
        public TotalAndLast Deceased { get; set; }

        [BsonElement("vaccinated")]
        public Vaccinated Vaccinated { get; set; }
    }
}
