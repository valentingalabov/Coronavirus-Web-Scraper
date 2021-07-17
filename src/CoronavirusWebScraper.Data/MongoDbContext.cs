using CoronavirusWebScraper.Data.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CoronavirusWebScraper.Data
{
    public class MongoDbContext
    {
        public IMongoDatabase MongoDB { get; set; }

        public MongoDbContext(IOptions<MongoDbSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            MongoDB = mongoClient.GetDatabase(options.Value.DatabaseName);
        }
    }
}
