namespace CoronavirusWebScraper.Data.Configuration
{
    /// <remarks> Hold MongoDB settings data.
    public class MongoDbSettings : IMongoDbSettings
    {
        /// <summary>
        /// Gets or sets connection string for MongoDB.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets database name in MongoDB.
        /// </summary>
        public string DatabaseName { get; set; }
    }
}
