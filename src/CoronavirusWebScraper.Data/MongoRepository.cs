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

    /// <summary>
    /// This repository implementation lets you store
    /// your documents in a MongoDB database.
    /// </summary>
    /// <typeparam name="TDocument">Entity to store as document.</typeparam>
    public class MongoRepository<TDocument> : IMongoRepository<TDocument>
    where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> collection;

        /// <summary>
        /// Connects to our database server by MongoClient and gets the proper database by GetDatabase method.
        /// Get collection data and store it in private field called collection.
        /// </summary>
        /// <param name="settings">Contain connection string and database name.</param>
        public MongoRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            this.collection = database.GetCollection<TDocument>(this.GetCollectionName(typeof(TDocument)));
        }

        /// <inheritdoc />
        public async Task InsertOneAsync(TDocument document)
        {
            await this.collection.InsertOneAsync(document);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TDocument> FilterBy(
      Expression<Func<TDocument, bool>> filterExpression)
        {
            return this.collection.Find(filterExpression).ToEnumerable();
        }

        /// <inheritdoc />
        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
       Expression<Func<TDocument, bool>> filterExpression,
       Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return this.collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        /// <inheritdoc />
        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => this.collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        /// <inheritdoc />
        public virtual IQueryable<TDocument> AsQueryable()
        {
            return this.collection.AsQueryable();
        }

        /// <summary>
        /// Gets documents collection by type of document provided in parameter.
        /// </summary>
        /// <param name="documentType">Current document type.</param>
        /// <returns>Collection name</returns>
        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
    }
}
