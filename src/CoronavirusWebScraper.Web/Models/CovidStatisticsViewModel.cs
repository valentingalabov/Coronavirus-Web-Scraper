
using MongoDB.Bson;
using System;

namespace CoronavirusWebScraper.Web.Models
{
    public class CovidStatisticsViewModel
    {
        public string Date { get; set; }

        public string FormatedDate => DateTime.Parse(Date).ToShortDateString();

        public string ScrapedDate { get; set; }

        public string Country { get; set; }

        public OverallViewModel Overall { get; set; }

        //public BsonDocument Regions { get; set; }

        //public Stats Stats { get; set; }
    }
}
