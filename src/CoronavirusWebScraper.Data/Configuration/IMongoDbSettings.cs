namespace CoronavirusWebScraper.Data.Configuration
{
    /// <summary>
    /// Hold MongoDB settings data.
    /// </summary>
    public interface IMongoDbSettings
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
