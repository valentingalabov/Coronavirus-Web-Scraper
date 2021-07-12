using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoronavirusWebScraper.Data
{
    public interface IMongoDbContext
    {
        IMongoDatabase MongoDB { get; }
    }
}
