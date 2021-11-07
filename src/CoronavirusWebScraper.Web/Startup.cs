namespace CoronavirusWebScraper.Web
{
    using CoronavirusWebScraper.Data;
    using CoronavirusWebScraper.Data.Configuration;
    using CoronavirusWebScraper.Services;
    using CoronavirusWebScraper.Services.Impl;
    using CoronavirusWebScraper.Web.BackgroundServices;
    using global::HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // MongoDbSettings
            services.Configure<MongoDbSettings>(this.Configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            // MongoDbRepository
            services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            // Services
            services.AddSingleton<ICovidDataScraperService, CovidDataScraperService>();
            services.AddTransient<IStatisticsDataService, StatisticsDataService>();

            // BackgroundService implementation
            services.Configure<HostedServiceOptions>(this.Configuration.GetSection(HostedServiceOptions.HostedService));
            services.AddHostedService<WebScraperHostedService>();

            services.AddControllersWithViews();

            // Health checks
            services.AddHealthChecks()
             .AddMongoDb(
                 mongodbConnectionString: this.Configuration["MongoDbSettings:ConnectionString"],
                 name: "MongoDb connection");

            services.AddHealthChecksUI().AddInMemoryStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHealthChecksUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                });
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
