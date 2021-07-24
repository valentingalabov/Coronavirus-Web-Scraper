using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Web.Models
{
    public class RegionsViewModel
    {
        [BsonElement("confirmed")]
        public ConfirmedViewModel Confirmed { get; set; }
    }
}
