using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronavirusWebScraper.Data.Repositories
{
    public interface IMongoRepository<T> where T : class
    {
        Task<IList<T>> All();

        Task AddAsync(T entity);

     
    }
}
