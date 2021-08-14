namespace CoronavirusWebScraper.Data.Configuration
{
    /// <remarks> Hold MongoDB settings data.
    public class MongoDbSettings : IMongoDbSettings
    {
        /// <inheritdoc />
        public string ConnectionString { get; set; }

        /// <inheritdoc />
        public string DatabaseName { get; set; }
    }
}
