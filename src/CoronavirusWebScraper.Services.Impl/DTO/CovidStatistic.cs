using CoronavirusWebScraper.Data.Attributes;
using CoronavirusWebScraper.Data.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace CoronavirusWebScraper.Services.Impl.DTO
{
    [BsonCollection("CovidStatistics")]
    public class CovidStatistic : Document
    {
        
        [BsonElement("date")]
        public string Date { get; set; }

        [BsonElement("date_scraped")]
        public string ScrapedDate { get; set; }

        [BsonElement("country")]
        public string Country { get; set; }

        [BsonElement("overall")]
        public Overall Overall { get; set; }

        [BsonElement("regions")]
        public BsonDocument Regions { get; set; }

        [BsonElement("stats")]
        public Stats Stats { get; set; }
       
    }
}
