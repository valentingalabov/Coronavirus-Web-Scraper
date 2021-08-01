using CoronavirusWebScraper.Data;
using CoronavirusWebScraper.Data.Configuration;
using CoronavirusWebScraper.Services;
using CoronavirusWebScraper.Services.Impl;
using CoronavirusWebScraper.Web.BackgroundServices;
using CoronavirusWebScraper.Web.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.ServiceProcess;

namespace CoronavirusWebScraper.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //MongoDbSettings
            services.Configure<MongoDbSettings>(Configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            //MongoDbRepository
            services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            //Services
            services.AddSingleton<ICovidDataScraperService, CovidDataScraperService>();
            services.AddTransient<IStatisticsDataService, StatisticsDataService>();

            //BackgroundService implementation
            services.AddHostedService<Worker>();
            services.AddControllersWithViews();

            //Health checks
            services.AddHealthChecks()
             .AddMongoDb(mongodbConnectionString: Configuration["MongoDbSettings:ConnectionString"],
                  name: "MongoDb connection")
             .AddCheck<DBResponseTimeHealthCheck>(name: "Database response time")
             .AddUrlGroup(new Uri("https://coronavirus.bg/"), "Check https://coronavirus.bg/ page is up")
             .AddUrlGroup(new Uri("https://coronavirus.bg/bg/statistika"), "Check https://coronavirus.bg/bg/statistika page is up")
             .AddCheck<CoronaviursPagePingHelthCheck>(name: "coronavirus.bg ping check")
             .AddDiskStorageHealthCheck(s => s.AddDrive("C:\\", 1024))
             .AddProcessAllocatedMemoryHealthCheck(512);
            
   
            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10);  
                opt.MaximumHistoryEntriesPerEndpoint(60);    
                opt.SetApiMaxActiveRequests(1);   
            })
            .AddInMemoryStorage();

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
