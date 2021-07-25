using MongoDB.Bson.Serialization.Attributes;

namespace CoronavirusWebScraper.Web.Models
{
    public class TotalAndLastViewModel
    {
        public int Total { get; set; }

        public int Last { get; set; }
    }
}