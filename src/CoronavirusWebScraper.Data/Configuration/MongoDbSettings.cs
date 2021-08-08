﻿namespace CoronavirusWebScraper.Data.Configuration
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string CollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
