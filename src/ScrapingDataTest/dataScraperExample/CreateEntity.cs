using AngleSharp;
using AngleSharp.Dom;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataScraperExample
{
    class CreateEntity
    {
        private IConfiguration config;
        private IBrowsingContext context;
        private string covidUrl;
        private AngleSharp.Dom.IDocument document;
        private string covidStatisticUrl;
        private IDocument statisticDocument;
        private string[] statistics;
        private IElement[] allTebles;

        public CreateEntity()
        {
            this.config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(config);
            this.covidUrl = "https://coronavirus.bg/";
            this.document = context.OpenAsync(this.covidUrl).GetAwaiter().GetResult();
            this.covidStatisticUrl = this.covidUrl + document.QuerySelector(".statistics-sub-header.nsi").GetAttribute("href");
            this.statisticDocument = context.OpenAsync(this.covidStatisticUrl)
                .GetAwaiter()
                .GetResult();

            this.statistics = document.QuerySelectorAll(".statistics-container > div > p").Select(x => x.TextContent).ToArray();
            this.allTebles = this.statisticDocument.QuerySelectorAll(".table").ToArray();
        }



        public BsonDocument GetDateAndCountryData()
        {

            var currentDateSpan = this.document.QuerySelector(".statistics-header-wrapper span").TextContent.Split(" ");
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
                { "overall", GetOverallStats()},
                {"regions", GetAllRegionsStatistics() }
            };


        }

        private BsonDocument GetAllRegionsStatistics()
        {
            var regions = GetRegionsData();

            return new BsonDocument
            {
                { "regions", 151 }
            };
        }

        private List<BsonDocument> GetRegionsData()
        {
            var regions = new List<BsonDocument>();


            var confirmedByRegionTableRecords = allTebles[3].QuerySelectorAll("td").SkipLast(3).Select(x => x.TextContent).ToArray();
            var vaccinatedByRegions = allTebles[5].QuerySelectorAll("td").SkipLast(7).Select(x => x.TextContent).ToArray();

            var currentRegionDocument = new BsonDocument();

            for (int i = 0; i < confirmedByRegionTableRecords.Length; i += 3)
            {
                var regionCode = GetRegionЕКАТТЕCode(confirmedByRegionTableRecords[i]).ToLower();
                var confirmed = IntParser(confirmedByRegionTableRecords[i + 1]);
                var confirmed24 = IntParser(confirmedByRegionTableRecords[i + 2]);
                var bson = new BsonDocument
                {
                    { regionCode,  new BsonDocument { { "confirmed", new BsonDocument { { "total", confirmed }, { "last", confirmed24 } } } } }
                };
                regions.Add(bson);
            }
            var counter = 0;
            for (int i = 0; i < vaccinatedByRegions.Length; i += 7)
            {
                var totalVaccinated = IntParser(vaccinatedByRegions[i + 1]);
                var comirnaty = IntParser(vaccinatedByRegions[i + 2]);
                var moderna = IntParser(vaccinatedByRegions[i + 3]);
                var astrazeneca = IntParser(vaccinatedByRegions[i + 4]);
                var janssen = IntParser(vaccinatedByRegions[i + 5]);
                var totalVaccinedComplate = IntParser(vaccinatedByRegions[i + 6]);
                var totalVaccinated24 = comirnaty + moderna + astrazeneca + janssen;

                regions[counter].Add("vaccinated", new BsonDocument { { "total", totalVaccinated } });
                regions[counter].Add("last", totalVaccinated24);
                regions[counter].Add("last_by_type", new BsonDocument { { "comirnaty", comirnaty }, { "moderna", moderna }, { "astrazeneca", astrazeneca }, { "janssen", janssen } });
                regions[counter].Add("total_completed", totalVaccinedComplate);
                counter++;
            }


            return regions;
        }


        private string GetRegionЕКАТТЕCode(string region)
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
                { "София", "SFO" },
                { "София (столица)", "SOF" },
                { "Стара Загора", "SZR" },
                { "Търговище", "TGV" },
                { "Хасково", "HKV" },
                { "Шумен", "SHU" },
                { "Ямбол", "JAM" },
            };

            return regionsWithCodes[region];
        }


        private BsonDocument GetOverallStats()
        {


            var totalTests = IntParser(statistics[0]);


            return new BsonDocument
            {
                { "tested", GetTestedStatistics()},
                {"confirmed",  GetConfirmedStatistics()},
                { "active", GetActiveStatistic()},
                {"recovered", GetRecoveredStatistic()},
                { "deceased", GetDeceasedStatistic()},
                { "vaccinated", GetVaccinatedStatistic()}
            };

        }

        private BsonDocument GetVaccinatedStatistic()
        {
            var vaccinated = IntParser(statistics[20]);
            var vaccinated24 = IntParser(statistics[22]);

            var vaccinatedTableRecords = allTebles[5].QuerySelectorAll("tr").Last().QuerySelectorAll("td").Select(x => x.TextContent).ToArray();
            var comirnaty = IntParser(vaccinatedTableRecords[2]);
            var moderna = IntParser(vaccinatedTableRecords[3]);
            var astraZeneca = IntParser(vaccinatedTableRecords[4]);
            var janssen = IntParser(vaccinatedTableRecords[5]);
            var totalVaccinatedComplate = IntParser(vaccinatedTableRecords[6]);

            return new BsonDocument()
            {
                { "total", 590495 },
                // Ваксинирани (24 ч)
                { "last", 10503 },
                { "last_by_type", new BsonDocument { { "Comirnaty", comirnaty }, { "Moderna", moderna }, { "AstraZeneca", astraZeneca }, { "Janssen", janssen } } },
                { "total_completed", totalVaccinatedComplate}
            };
        }

        private BsonDocument GetDeceasedStatistic()
        {

            var deceased = IntParser(statistics[16]);
            var deceased24 = IntParser(statistics[18]);

            return new BsonDocument
            {
                // Починали (общо)
                { "total", deceased },
                // Починали (24 ч)
                { "last", deceased24 }
            };
        }


        private BsonDocument GetRecoveredStatistic()
        {
            var totalRecovered = IntParser(statistics[8]);
            var totalRecovered24 = IntParser(statistics[10]);

            return new BsonDocument
            {
                { "total", totalRecovered },
                // Излекувани (24 ч)
                { "last", totalRecovered24 }
            };
        }

        private BsonDocument GetActiveStatistic()
        {

            var active = IntParser(statistics[6]);

            var hospitalized = IntParser(statistics[12]);
            var intensiveCare = IntParser(statistics[14]);



            return new BsonDocument
            {
                {"current", active },
                { "current_by_type", new BsonDocument { { "hospitalized", hospitalized }, { "icu", intensiveCare } }}
            };
        }

        private BsonDocument GetConfirmedStatistics()
        {
            var totalConfirmed = IntParser(this.statistics[4]);

            var confirmedTestsByTypeTableRecords = allTebles[2].QuerySelectorAll("td").Select(x => x.TextContent).ToArray();

            var confirmedPcr = IntParser(confirmedTestsByTypeTableRecords[1]);
            var confirmedAntigen = IntParser(confirmedTestsByTypeTableRecords[4]);
            var confirmedPcr24 = IntParser(confirmedTestsByTypeTableRecords[2]);
            var confirmedAntigen24 = IntParser(confirmedTestsByTypeTableRecords[5]);
            var totalConfirmed24 = IntParser(confirmedTestsByTypeTableRecords[8]);



            return new BsonDocument
            {
                {"total", totalConfirmed },
                { "total_by_type", new BsonDocument
                    { { "pcr", confirmedPcr }, {"antigen", confirmedAntigen } } },
                {"last",  totalConfirmed24},
                {"last_by_type", new BsonDocument { { "pcr", confirmedPcr24 }, { "antigen", confirmedAntigen24 } } },
                { "medical", GetMedicalStatistics() },


            };
        }

        private BsonDocument GetMedicalStatistics()
        {
            var medicalTableRecords = allTebles[4].QuerySelectorAll("td").Select(x => x.TextContent).ToArray();
            var totalDoctors = IntParser(medicalTableRecords[1]);
            var totalNurces = IntParser(medicalTableRecords[3]);
            var totalParamedics1 = IntParser(medicalTableRecords[5]);
            var totalParamedics2 = IntParser(medicalTableRecords[7]);
            var others = IntParser(medicalTableRecords[9]);
            var totalMedical = IntParser(medicalTableRecords[11]);

            //TODO:  data for medical for 24h!

            return new BsonDocument
            {
                { "total", totalMedical },
                { "total_by_type", new BsonDocument{{ "doctor", totalDoctors },
                        { "nurces", totalNurces }, { "paramedics_1", totalParamedics1 },
                        { "paramedics_2", totalParamedics2 }, { "other", others } }},
                { "last", 1111111 },
                { "last_by_type", new BsonDocument{{ "doctor", 11111 },
                        { "nurces", 1111 }, { "paramedics_1", 1111 },
                        { "paramedics_2", 1111 }, { "other", 111 } }}
            };


        }
        private BsonDocument GetTestedStatistics()
        {
            var totalTests = IntParser(statistics[0]);

            var totalTestsByTypeTableRecords = allTebles[1].QuerySelectorAll("td").Select(x => x.TextContent).ToArray();

            var totalPcr = IntParser(totalTestsByTypeTableRecords[1]);
            var totalAntigen = IntParser(totalTestsByTypeTableRecords[4]);

            var totalTests24 = IntParser(statistics[2]);

            var totalPcr24 = IntParser(totalTestsByTypeTableRecords[2]);
            var totalAntigen24 = IntParser(totalTestsByTypeTableRecords[5]);




            return new BsonDocument()
            {
                { "total", totalTests},
                { "total_by_type", new BsonDocument { { "pcr", totalPcr }, { "antigen", totalAntigen} } },
                { "last",totalTests24 },
                { "last_by_type", new BsonDocument { { "pcr", totalPcr24 }, { "antigen", totalAntigen24 } } }
            };
        }


        private static int IntParser(string num)
        {
            if (num == "-")
            {
                return 0;
            }

            return int.Parse(num.Trim().Replace(" ", string.Empty));
        }

    }
}
