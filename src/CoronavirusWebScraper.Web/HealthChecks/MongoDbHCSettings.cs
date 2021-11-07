namespace CoronavirusWebScraper.Web.HealthChecks
{
    public class MongoDbHCSettings
    {
        public const string MongoDbSettings = "MongoDbSettings";

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
