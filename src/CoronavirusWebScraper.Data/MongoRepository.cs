namespace CoronavirusWebScraper.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using CoronavirusWebScraper.Data.Attributes;
    using CoronavirusWebScraper.Data.Configuration;
    using CoronavirusWebScraper.Data.Models;
    using MongoDB.Driver;

    public class MongoRepository<TDocument> : IMongoRepository<TDocument>
    where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> collection;

        public MongoRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            this.collection = database.GetCollection<TDocument>(this.GetCollectionName(typeof(TDocument)));
        }

        public async Task InsertOneAsync(TDocument document)
        {
            await this.collection.InsertOneAsync(document);
        }

        public virtual IEnumerable<TDocument> FilterBy(
      Expression<Func<TDocument, bool>> filterExpression)
        {
            return this.collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
       Expression<Func<TDocument, bool>> filterExpression,
       Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return this.collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => this.collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return this.collection.AsQueryable();
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
    }
}
