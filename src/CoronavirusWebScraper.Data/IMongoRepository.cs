namespace CoronavirusWebScraper.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using CoronavirusWebScraper.Data.Models.BaseModels;

    /// <summary>
    /// MongoDB repository interface.
    /// </summary>
    /// <typeparam name="TDocument">Entity to store as document.</typeparam>
    public interface IMongoRepository<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Add object to the repository.
        /// </summary>
        /// <param name="document">Object as document you want to add.</param>
        Task InsertOneAsync(TDocument document);

        /// <summary>
        /// Return single objects from database.
        /// </summary>
        /// <param name="filterExpression">Expression parameter which filter data.</param>
        /// <returns>The maching document.</returns>
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Return whole non-materialized collection.
        /// </summary>
        /// <returns>Whole non-materialized collection.</returns>
        IQueryable<TDocument> AsQueryable();

        /// <summary>
        /// Return filtred objects.
        /// </summary>
        /// <param name="filterExpression">/Expression parameter which filter data.</param>
        /// <returns>Maching collection.</returns>
        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Filter data and take only some fields, rather than full objects.
        /// </summary>
        /// <typeparam name="TProjected">Filtered object.</typeparam>
        /// <param name="filterExpression">Expression parameter which filter data.</param>
        /// <param name="projectionExpression"> Project field you want to take.</param>
        /// <returns>Maching collection projected objects.</returns>
        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);
    }
}
