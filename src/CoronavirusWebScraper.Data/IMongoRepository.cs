using CoronavirusWebScraper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Data
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task InsertOneAsync(TDocument document);

        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        IQueryable<TDocument> AsQueryable();

        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

    }
}