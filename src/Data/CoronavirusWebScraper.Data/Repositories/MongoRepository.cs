using CoronavirusWebScraper.Data.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Data.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T : class
    {
        private readonly IMongoCollection<T> collection;
        public MongoRepository(IOptions<MongoDbSettings> options, IMongoDbContext myWorldContext)
        {
            this.collection = myWorldContext.MongoDB.GetCollection<T>(options.Value.CollectionName);
        }

        public async Task<IList<T>> All()
        {
            return await this.collection.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await this.collection.InsertOneAsync(entity);
        }
    }
}
