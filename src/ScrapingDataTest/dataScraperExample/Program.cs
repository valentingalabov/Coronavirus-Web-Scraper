using AngleSharp;
using AngleSharp.Dom;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace dataScraperExample
{
    class Program
    {

        
        static void Main(string[] args)
        {


            var asd = new CreateEntity();
           var data  = asd.GetDateAndCountryData();
            MongoCrud db = new MongoCrud("WebScraper");
            db.InsertRecord("Statistics", data);


            //var config = Configuration.Default.WithDefaultLoader();
            //var context = BrowsingContext.New(config);

            //var covidUrl = "https://coronavirus.bg/";


            //var document = context.OpenAsync(covidUrl)
            //    .GetAwaiter()
            //    .GetResult();

            //var covidStatisticUrl = covidUrl + document.QuerySelector(".statistics-sub-header.nsi").GetAttribute("href");

            //var statisticDocument = context.OpenAsync(covidStatisticUrl)
            //    .GetAwaiter()
            //    .GetResult();

            ////if (document.StatusCode == HttpStatusCode.NotFound || statisticDocument.StatusCode == HttpStatusCode.NotFound)
            ////{
            ////    throw new InvalidOperationException();
            ////}

            //var entity = GetDateAndCountryData(document);

            ////var currentDateSpan = document.QuerySelector(".statistics-header-wrapper span").TextContent.Split(" ");
            ////var time = currentDateSpan[2];
            ////var date = currentDateSpan[5];
            ////var month = currentDateSpan[6];
            ////var year = currentDateSpan[7];
            ////var currDateAsString = date + " " + month + " " + year + " " + time;
            ////var currDate = DateTime.Parse(currDateAsString);

            ////var dateToAdd = currDate.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            ////var dateScraped = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            ////var statistics = document.QuerySelectorAll(".statistics-container > div > p").Select(x => x.TextContent).ToArray();
            ////var totalTests = IntParser(statistics[0]);
            ////var totalTests24 = IntParser(statistics[2]);
            ////var totalConfirmed = IntParser(statistics[4]);
            ////var active = IntParser(statistics[6]);
            ////var totalCured = IntParser(statistics[8]);
            ////var totalCured24 = IntParser(statistics[10]);
            ////var hospitalized = IntParser(statistics[12]);
            ////var intensiveCare = IntParser(statistics[14]);
            ////var deceased = IntParser(statistics[16].Trim());
            ////var deceased24 = IntParser(statistics[18].Trim());
            ////var vaccinated = IntParser(statistics[20]);
            ////var vaccinated24 = IntParser(statistics[22]);



            //var allTebles = statisticDocument.QuerySelectorAll(".table").ToArray();

            ////var totalTestsByTypeTableRecords = allTebles[1].QuerySelectorAll("td").Select(x => x.TextContent).ToArray();
            ////var totalPcr = IntParser(totalTestsByTypeTableRecords[1]);
            ////var totalAntigen = IntParser(totalTestsByTypeTableRecords[4]);
            ////var totalPcr24 = IntParser(totalTestsByTypeTableRecords[2]);
            ////var totalAntigen24 = IntParser(totalTestsByTypeTableRecords[5]);

            ////var confirmedTestsByTypeTableRecords = allTebles[2].QuerySelectorAll("td").Select(x => x.TextContent).ToArray();
            ////var confirmedPcr = IntParser(confirmedTestsByTypeTableRecords[1]);
            ////var confirmedAntigen = IntParser(confirmedTestsByTypeTableRecords[4]);
            ////var confirmedPcr24 = IntParser(confirmedTestsByTypeTableRecords[2]);
            ////var confirmedAntigen24 = IntParser(confirmedTestsByTypeTableRecords[5]);


            ////var medicalTableRecords = allTebles[4].QuerySelectorAll("td").Select(x => x.TextContent).ToArray();
            ////var totalDoctors = IntParser(medicalTableRecords[1]);
            ////var totalNurces = IntParser(medicalTableRecords[3]);
            ////var totalParamedics1 = IntParser(medicalTableRecords[5]);
            ////var totalParamedics2 = IntParser(medicalTableRecords[7]);
            ////var others = IntParser(medicalTableRecords[9]);
            ////var totalMedical = IntParser(medicalTableRecords[11]);


            //var vaccinatedTableRecords = allTebles[5].QuerySelectorAll("tr").Last().QuerySelectorAll("td").Select(x => x.TextContent).ToArray();
            //var comirnaty = IntParser(vaccinatedTableRecords[2]);
            //var moderna = IntParser(vaccinatedTableRecords[3]);
            //var astraZeneca = IntParser(vaccinatedTableRecords[4]);
            //var janssen = IntParser(vaccinatedTableRecords[5]);
            //var totalVaccinatedComplate = IntParser(vaccinatedTableRecords[6]);


            //var confirmedByRegionTableRecords = allTebles[3].QuerySelectorAll("td").SkipLast(3).Select(x => x.TextContent).ToArray();
            //var vaccinatedByRegions = allTebles[5].QuerySelectorAll("td").SkipLast(7).Select(x => x.TextContent).ToArray();

            //var listOfTowns = new List<string>();

            //for (int i = 0; i < confirmedByRegionTableRecords.Length; i += 3)
            //{
            //    var regionCode = GetRegionCode(confirmedByRegionTableRecords[i]);
            //    var confirmed = IntParser(confirmedByRegionTableRecords[i + 1]);
            //    var confirmed24 = IntParser(confirmedByRegionTableRecords[i + 2]);


            //}

            //var asdasad = 0;

            ////var tested = new BsonDocument
            ////{
            ////    {"total" , totalTests},
            ////    {"total_by_type", new BsonDocument { { "pcr", 215641}, { "antigen", 564848} } }
            ////};
            ////var stats = new BsonDocument
            ////{
            ////    {"date", dateToAdd },
            ////    { "date_scraped", dateScraped},
            ////    { "country", "BG"},
            ////    { "overall", tested}
            ////};





            ////var tested = new Tested();
            ////tested.Total = IntParser(statistics[0]);


            ////var overall = new Overall();

            ////overall.Tested = tested;

            ////var statsss = new Statistic();
            ////statsss.Date = dateToAdd;
            ////statsss.DateScraped = dateScraped;

            ////statsss.Overall = overall;

            //MongoCrud db = new MongoCrud("WebScraper");
            ////var elToADd = new Statistic()
            ////{
            ////    Date = dateToAdd,
            ////    DateScraped = dateScraped,
            ////    Overall = new Overall
            ////    {
            ////        Tested = new Tested
            ////        {
            ////            Total = 52841,
            ////            TotalByType = new ByType
            ////            {
            ////                Antigen = 4541,
            ////                PCR = 444
            ////            }
            ////        }
            ////    }

            ////};




            ////db.InsertRecord("Statistics", stats);




            ////db.InsertRecord("Statistics", statsss);



            ////Console.WriteLine(stats.TotalTests);
            ////Console.WriteLine(stats.TotalTests24);
            ////Console.WriteLine(stats.TotalConfirmed);
            ////Console.WriteLine(stats.Active);
            ////Console.WriteLine(stats.TotalCured);
            ////Console.WriteLine(stats.TotalCured24);
            ////Console.WriteLine(stats.Hospitalized);
            ////Console.WriteLine(stats.IntensiveCare);
            ////Console.WriteLine(stats.Died);
            ////Console.WriteLine(stats.Died24);
            ////Console.WriteLine(stats.Vaccinated);
            ////Console.WriteLine(stats.Vaccinated24);

            ////Console.WriteLine(stats.Country);


        }

        private static BsonDocument GetDateAndCountryData(IDocument document)
        {

            var currentDateSpan = document.QuerySelector(".statistics-header-wrapper span").TextContent.Split(" ");
            var time = currentDateSpan[2];
            var date = currentDateSpan[5];
            var month = currentDateSpan[6];
            var year = currentDateSpan[7];
            var currDateAsString = date + " " + month + " " + year + " " + time;
            var currDate = DateTime.Parse(currDateAsString);

            var dateToAdd = currDate.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            var dateScraped = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");


            return new BsonDocument
            {
                { "date", dateToAdd },
                { "date_scraped", dateScraped },
                { "country", "BG"},
                { "overall", GetOverallStats(document)}
            };


        }

        private static BsonDocument GetOverallStats(IDocument document)
        {
            var statistics = document.QuerySelectorAll(".statistics-container > div > p").Select(x => x.TextContent).ToArray();

            var totalTests = IntParser(statistics[0]);

            var totalTests24 = IntParser(statistics[2]);
            var totalConfirmed = IntParser(statistics[4]);
            var active = IntParser(statistics[6]);
            var totalCured = IntParser(statistics[8]);
            var totalCured24 = IntParser(statistics[10]);
            var hospitalized = IntParser(statistics[12]);
            var intensiveCare = IntParser(statistics[14]);
            var deceased = IntParser(statistics[16].Trim());
            var deceased24 = IntParser(statistics[18].Trim());
            var vaccinated = IntParser(statistics[20]);
            var vaccinated24 = IntParser(statistics[22]);


            return new BsonDocument
            {
                { "tested", GetTestedStatistics(document)}
            };

        }

        private static BsonDocument GetTestedStatistics(IDocument document)
        {
            var statistics = document.QuerySelectorAll(".statistics-container > div > p").Select(x => x.TextContent).ToArray();
            var totalTests = IntParser(statistics[0]);

            return new BsonDocument()
            {
                { "total", totalTests},
                { "total_by_type", "" },
            };
        }

        private static string GetRegionCode(string region)
        {
            var regionsWithCodes = new Dictionary<string, string>()
            {
                { "Благоевград", "BLG" },
                { "Бургас", "BGS" },
                { "Варна", "VAR" },
                { "Велико Търново", "VTR" },
                { "Видин", "VID" },
                { "Враца", "VRC" },
                { "Габрово", "GAB" },
                { "Добрич", "DOB" },
                { "Кърджали", "KRZ" },
                { "Кюстендил", "KNL" },
                { "Ловеч", "LOV" },
                { "Монтана", "MON" },
                { "Пазарджик", "PAZ" },
                { "Перник", "PER" },
                { "Плевен", "PVN" },
                { "Пловдив", "PDV" },
                { "Разград", "RAZ" },
                { "Русе", "RSE" },
                { "Силистра", "SLS" },
                { "Сливен", "SLV" },
                { "Смолян", "SML" },
                { "София", "SOF" },
                { "София (столица)", "SOF" },
                { "Стара Загора", "SZR" },
                { "Търговище", "TGV" },
                { "Хасково", "HKV" },
                { "Шумен", "SHU" },
                { "Ямбол", "JAM" },


            };



            return regionsWithCodes[region];

            //            var listBulgarianRegins = new List<string>();
            //            listBulgarianRegins.AddRange();
            //            var bgRegions =["Благоевград", "Добрич", "Плевен", "София",
            //  "Бургас", "Кърджали", "Пловдив", "София(столица)", "Варна", "Кюстендил", "Разград", "Стара Загора",
            //"Велико", "Търново", "Ловеч", "Русе", "Търговище",
            //  "Видин", "Монтана", "Силистра", "Хасково",
            //  "Враца", "Пазарджик", "Сливен", "Шумен",
            //  "Габрово", "Перник", "Смолян", "Ямбол"];



        }

        private static int IntParser(string num)
        {
            if (num == "-")
            {
                return 0;
            }

            return int.Parse(num.Trim().Replace(" ", string.Empty));
        }

        public class MongoCrud
        {
            private IMongoDatabase db;

            public MongoCrud(string database)
            {
                var client = new MongoClient();
                db = client.GetDatabase(database);
            }


            public void InsertRecord<T>(string table, T record)
            {
                var collection = db.GetCollection<T>(table);
                collection.InsertOne(record);
            }

            public List<T> LoadRecords<T>(string table)
            {
                var collection = db.GetCollection<T>(table);

                return collection.Find(new BsonDocument()).ToList();
            }

            public T LoadRecordById<T>(string table, Guid id)
            {
                var collection = db.GetCollection<T>(table);

                var filter = Builders<T>.Filter.Eq("Id", id);

                return collection.Find(filter).First();
            }
            public void UpsertRecord<T>(string table, Guid id, T record)
            {
                var collection = db.GetCollection<T>(table);

                var result = collection.ReplaceOne(new BsonDocument("_id", id),
                    record, new UpdateOptions { IsUpsert = true });


            }

            public void DeleteRecord<T>(string table, Guid id)
            {
                var collection = db.GetCollection<T>(table);
                var filter = Builders<T>.Filter.Eq("Id", id);
                collection.DeleteOne(filter);

            }
        }
    }
}

