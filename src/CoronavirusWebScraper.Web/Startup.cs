namespace CoronavirusWebScraper.Web
{
    using System;

    using CoronavirusWebScraper.Data;
    using CoronavirusWebScraper.Data.Configuration;
    using CoronavirusWebScraper.Services;
    using CoronavirusWebScraper.Services.Impl;
    using CoronavirusWebScraper.Web.BackgroundServices;
    using CoronavirusWebScraper.Web.HealthChecks;
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
            services.Configure<BackgroundServiceConfiguration>(this.Configuration.GetSection(nameof(BackgroundServiceConfiguration)));
            services.AddSingleton<IBackgroundServiceConfiguration>(serviceProvider => serviceProvider.GetRequiredService<IOptions<BackgroundServiceConfiguration>>().Value);
            services.AddHostedService<Worker>();

            services.AddControllersWithViews();

            // Health checks
            services.AddHealthChecks()
             .AddMongoDb(
                 mongodbConnectionString: this.Configuration["MongoDbSettings:ConnectionString"],
                 name: "MongoDb connection")
             .AddUrlGroup(new Uri("https://coronavirus.bg/"), "Check coronavirus.bg page is up")
             .AddUrlGroup(new Uri("https://coronavirus.bg/bg/statistika"), "Check coronavirus.bg/bg/statistika page is up")
             .AddCheck<CoronaviursPagePingHelthCheck>(name: "coronavirus.bg ping check")
             .AddDiskStorageHealthCheck(s => s.AddDrive("C:\\", 1024))
             .AddProcessAllocatedMemoryHealthCheck(512);

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
