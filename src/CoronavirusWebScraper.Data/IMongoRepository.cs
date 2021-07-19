using CoronavirusWebScraper.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Data
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task InsertOneAsync(TDocument document);
    }
}