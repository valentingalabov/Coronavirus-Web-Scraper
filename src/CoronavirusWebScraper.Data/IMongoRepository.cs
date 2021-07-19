using CoronavirusWebScraper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Data
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task InsertOneAsync(TDocument document);

        IEnumerable<TProjected> FilterBy<TProjected>(
    Expression<Func<TDocument, bool>> filterExpression,
    Expression<Func<TDocument, TProjected>> projectionExpression);
    }
}