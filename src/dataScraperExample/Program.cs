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


           // var asd = new CreateEntity();
           //var data  = asd.GetDateAndCountryData();
           // MongoCrud db = new MongoCrud("WebScraper");
           // db.InsertRecord("Statistics", data);


        }

        //private static BsonDocument GetDateAndCountryData(IDocument document)
        //{

        //    var currentDateSpan = document.QuerySelector(".statistics-header-wrapper span").TextContent.Split(" ");
        //    var time = currentDateSpan[2];
        //    var date = currentDateSpan[5];
        //    var month = currentDateSpan[6];
        //    var year = currentDateSpan[7];
        //    var currDateAsString = date + " " + month + " " + year + " " + time;
        //    var currDate = DateTime.Parse(currDateAsString);

        //    var dateToAdd = currDate.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
        //    var dateScraped = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");


        //    return new BsonDocument
        //    {
        //        { "date", dateToAdd },
        //        { "date_scraped", dateScraped },
        //        { "country", "BG"},
        //        { "overall", GetOverallStats(document)}
        //    };


        //}

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
                collection.InsertOneAsync(record);
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

