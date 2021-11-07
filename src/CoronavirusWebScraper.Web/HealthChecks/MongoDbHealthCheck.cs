namespace CoronavirusWebScraper.Web.HealthChecks
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class MongoDbHealthCheck : IHealthCheck
    {
        private readonly IOptions<MongoDbHCSettings> mongoDbSettings;

        public MongoDbHealthCheck(IOptions<MongoDbHCSettings> mongoDbSettings)
        {
            this.mongoDbSettings = mongoDbSettings;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var mongoClient = new MongoClient(this.mongoDbSettings.Value.ConnectionString);

                using var collectionCursor = await mongoClient
                    .GetDatabase(this.mongoDbSettings.Value.DatabaseName)
                    .ListCollectionNamesAsync(cancellationToken: cancellationToken);
                var collections = await collectionCursor.ToListAsync(cancellationToken);

                if (collections.Any())
                {
                    return HealthCheckResult.Healthy();
                }

                using var dbCursor = await mongoClient.ListDatabaseNamesAsync(cancellationToken);
                await dbCursor.FirstOrDefaultAsync(cancellationToken);

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
            }
        }
    }
}
