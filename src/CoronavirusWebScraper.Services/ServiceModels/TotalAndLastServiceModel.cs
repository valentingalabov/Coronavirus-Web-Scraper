using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Services.ServiceModels
{
    public class TotalAndLastServiceModel
    {
        [BsonElement("total")]
        public int Total { get; set; }

        [BsonElement("last")]
        public int Last { get; set; }
    }
}