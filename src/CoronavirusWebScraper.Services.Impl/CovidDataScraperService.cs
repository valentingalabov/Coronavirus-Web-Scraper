using AngleSharp;
using AngleSharp.Dom;
using CoronavirusWebScraper.Data;
using CoronavirusWebScraper.Data.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CoronavirusWebScraper.Services.Impl
{
    public class CovidDataScraperService : ICovidDataScraperService
    {
        const string covidUrl = "https://coronavirus.bg/";

        private readonly IMongoRepository<CovidStatistic> _repository;

        public CovidDataScraperService(IMongoRepository<CovidStatistic> repository)
        {
            _repository = repository;
        }

        public async Task ScrapeData()
        {
            var document = await FetchDocument();

            await _repository.InsertOneAsync(document);
        }

        private async Task<CovidStatistic> FetchDocument()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            var document = await context.OpenAsync(covidUrl);

            var covidStatisticUrl = covidUrl + document.QuerySelector(".statistics-sub-header.nsi").GetAttribute("href");
            var statisticDocument = await context.OpenAsync(covidStatisticUrl);

            var statistics = document.QuerySelectorAll(".statistics-container > div > p").Select(x => x.TextContent).ToArray();
            var allTebles = statisticDocument.QuerySelectorAll(".table").ToArray();

            //Date
            var currentDateSpan = document.QuerySelector(".statistics-header-wrapper span").TextContent.Split(" ");
            var time = currentDateSpan[2];
            var date = currentDateSpan[5];
            var month = currentDateSpan[6];
            var year = currentDateSpan[7];
            var currDateAsString = date + " " + month + " " + year + " " + time;
            var currDate = DateTime.Parse(currDateAsString);

            var dataDate = currDate.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");

            var dateScraped = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            //Tests
            var totalTestsByTypeTableRecords = allTebles[1].QuerySelectorAll("td").Select(x => x.TextContent).ToArray();

            var totalTests = IntParser(statistics[0]);
            var totalPcr = IntParser(totalTestsByTypeTableRecords[1]);
            var totalAntigen = IntParser(totalTestsByTypeTableRecords[4]);

            var totalTests24 = IntParser(statistics[2]);
            var totalPcr24 = IntParser(totalTestsByTypeTableRecords[2]);
            var totalAntigen24 = IntParser(totalTestsByTypeTableRecords[5]);

            //confirmed
            var confirmedTestsByTypeTableRecords = allTebles[2].QuerySelectorAll("td").Select(x => x.TextContent).ToArray();
            var confirmedByRegionTableRecords = allTebles[3].QuerySelectorAll("td").SkipLast(3).Select(x => x.TextContent).ToArray();

            var totalConfirmed = IntParser(statistics[4]);
            var confirmedPcr = IntParser(confirmedTestsByTypeTableRecords[1]);
            var confirmedAntigen = IntParser(confirmedTestsByTypeTableRecords[4]);

            var confirmedPcr24 = IntParser(confirmedTestsByTypeTableRecords[2]);
            var confirmedAntigen24 = IntParser(confirmedTestsByTypeTableRecords[5]);
            var totalConfirmed24 = IntParser(confirmedTestsByTypeTableRecords[8]);

            //active 
            var active = IntParser(statistics[6]);

            var hospitalized = IntParser(statistics[12]);
            var intensiveCare = IntParser(statistics[14]);

            //recovered
            var totalRecovered = IntParser(statistics[8]);
            var totalRecovered24 = IntParser(statistics[10]);

            //deceased
            var deceased = IntParser(statistics[16]);
            var deceased24 = IntParser(statistics[18]);

            //vaccinated
            var vaccinated = IntParser(statistics[20]);
            var vaccinated24 = IntParser(statistics[22]);

            var vaccinatedTableRecords = allTebles[5].QuerySelectorAll("tr").Last().QuerySelectorAll("td").Select(x => x.TextContent).ToArray();

            var comirnaty = IntParser(vaccinatedTableRecords[2]);
            var moderna = IntParser(vaccinatedTableRecords[3]);
            var astraZeneca = IntParser(vaccinatedTableRecords[4]);
            var janssen = IntParser(vaccinatedTableRecords[5]);
            var totalVaccinatedComplate = IntParser(vaccinatedTableRecords[6]);

            //MedicalTable
            var medicalTableRecords = allTebles[4].QuerySelectorAll("td").Select(x => x.TextContent).ToArray();

            var status = "approved";


            var covidStatistics = new CovidStatistic
            {
                Date = dataDate,
                ScrapedDate = dateScraped,
                Country = "BG",
                Overall = new Overall
                {
                    Tested = new Tested
                    {
                        Total = totalTests,
                        TotalByType = new TestedByType { PCR = totalPcr, Antigen = totalAntigen },
                        Last24 = totalTests24,
                        TotalByType24 = new TestedByType { PCR = totalPcr24, Antigen = totalAntigen24 },
                    },
                    Confirmed = new Confirmed
                    {
                        Total = totalConfirmed,
                        TotalByType = new TestedByType { PCR = confirmedPcr, Antigen = confirmedAntigen },
                        Last24 = totalConfirmed24,
                        TotalByType24 = new TestedByType { PCR = confirmedPcr24, Antigen = confirmedAntigen24 },
                        Medical = GetMedicalStatistics(medicalTableRecords),
                    },
                    Active = new Active
                    {
                        Curent = active,
                        CurrentByType = new ActiveTypes { Hospitalized = hospitalized, Icu = intensiveCare }
                    },
                    Recovered = new TotalAndLast
                    {
                        Total = totalRecovered,
                        Last = totalRecovered24
                    },
                    Deceased = new TotalAndLast
                    {
                        Total = deceased,
                        Last = deceased24
                    },
                    Vaccinated = new Vaccinated
                    {
                        Total = vaccinated,
                        Last = vaccinated24,
                        LastByType = new VaccineType
                        {
                            Comirnaty = comirnaty,
                            Moderna = moderna,
                            AstraZeneca = astraZeneca,
                            Janssen = janssen
                        },
                        TotalCompleted = totalVaccinatedComplate
                    }
                },
                Regions = GetAllRegionsData(allTebles),
                Stats = new Stats
                {
                    TestedPrc = new TestedPrc
                    {
                        TotalByTyprPrc = new PcrAntigenPrc { PCR = DevideTwoIntiger(totalPcr, totalTests), Antigen = DevideTwoIntiger(totalAntigen, totalTests) },
                        LastByTypePrc = new PcrAntigenPrc { PCR = DevideTwoIntiger(totalPcr24, totalTests24), Antigen = DevideTwoIntiger(totalAntigen24, totalTests24) },
                    },
                    ConfirmedPrc = new ConfirmedPrc
                    {
                        TotalPerTestedPrc = DevideTwoIntiger(totalConfirmed, totalTests),
                        LastPerTestedPrc = DevideTwoIntiger(totalConfirmed24, totalTests24),
                        TotalByTypePrc = new PcrAntigenPrc { PCR = DevideTwoIntiger(confirmedPcr, totalConfirmed), Antigen = DevideTwoIntiger(confirmedAntigen, totalConfirmed) },
                        LastByTypePrc = new PcrAntigenPrc { PCR = DevideTwoIntiger(confirmedPcr24, totalConfirmed24), Antigen = DevideTwoIntiger(confirmedAntigen24, totalConfirmed24) },
                        //toDo medicalPrc
                        MedicalPcr = 0
                    },
                    Active = new ActivePrc
                    {
                        HospotalizedPerActive = DevideTwoIntiger(hospitalized, active),
                        IcuPerHospitalized = DevideTwoIntiger(intensiveCare, hospitalized)
                    }
                }
            };

            return covidStatistics;

        }

        //private BsonDocument CheckDataState()
        //{
        //    var sb = new StringBuilder();
        //    var fields = new BsonDocument();


        //    if (totalTests24 != totalPcr24 + totalAntigen24)
        //    {
        //        status = "discrepancy";
        //        sb.AppendLine($"Sum of total tests for 24h must be {totalTests24} but it is {totalPcr24 + totalAntigen24}");
        //        fields.Add("TotalTestFor24h ", new BsonDocument { { "expected", totalTests24 }, { "actual", totalPcr24 + totalAntigen24 } });

        //    }
        //    if (totalConfirmed24 != confirmedAntigen24 + confirmedPcr24)
        //    {
        //        status = "discrepancy";
        //        sb.AppendLine($"Sum of total confirmed tests for 24h must be {totalConfirmed24} but it is {confirmedAntigen24 + confirmedPcr24}");
        //        fields.Add("TotalConfrimedTestFor24h ", new BsonDocument { { "expected", totalConfirmed24 }, { "actual", confirmedAntigen24 + confirmedPcr24 } });
        //    }


        //    var totalConfirmed24Test = 0;

        //    for (int i = 0; i < confirmedByRegionTableRecords.Length; i += 3)
        //    {
        //        totalConfirmed24Test += IntParser(confirmedByRegionTableRecords[i + 2]);

        //    }

        //    if (totalConfirmed24 != totalConfirmed24Test)
        //    {
        //        status = "discrepancy";
        //        sb.AppendLine($"Sum of total confirmed tests for 24h for all regions must be {totalConfirmed24} but it is {totalConfirmed24Test}");
        //        fields.Add("TotalConfrimedTestForAllRegions24h ", new BsonDocument { { "expected", totalConfirmed24 }, { "actual", totalConfirmed24Test } });
        //    }

        //    if (vaccinated24 != comirnaty + moderna + astraZeneca + janssen)
        //    {
        //        status = "discrepancy";
        //        sb.AppendLine($"Sum of total vaccinated by type for 24h must be {vaccinated24} but it is {comirnaty + moderna + astraZeneca + janssen}");
        //        fields.Add("TotalVaccinated24h ", new BsonDocument { { "expected", vaccinated24 }, { "actual", comirnaty + moderna + astraZeneca + janssen } });
        //    }

        //    if (sb.Length > 0)
        //    {
        //        fields.Add("description", sb.ToString().Trim());
        //    }

        //    return fields;



        //}


        private BsonDocument GetAllRegionsData(IElement[] allTebles)
        {
            var regionsStatistic = new List<BsonDocument>();
            var regionsNames = new List<string>();
            var dictionary = new Dictionary<string, object>();

            var vaccinatedByRegions = allTebles[5].QuerySelectorAll("td").SkipLast(7).Select(x => x.TextContent).ToArray();
            var confirmedByRegionTableRecords = allTebles[3].QuerySelectorAll("td").SkipLast(3).Select(x => x.TextContent).ToArray();


            for (int i = 0; i < confirmedByRegionTableRecords.Length; i += 3)
            {
                var regionCode = GetRegionЕКАТТЕCode(confirmedByRegionTableRecords[i]).ToLower();
                var confirmed = IntParser(confirmedByRegionTableRecords[i + 1]);
                var confirmed24 = IntParser(confirmedByRegionTableRecords[i + 2]);
                var currentRegionDocument = new BsonDocument();
                regionsNames.Add(regionCode);
                regionsStatistic.Add(new BsonDocument { { "confirmed", new BsonDocument { { "total", confirmed }, { "last", confirmed24 } } } });


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




                regionsStatistic[counter].Add("vaccinated", new BsonDocument
                {
                    { "total", totalVaccinated },
                    { "last", totalVaccinated24 },
                    { "last_by_type", new BsonDocument { { "comirnaty", comirnaty }, { "moderna", moderna }, { "astrazeneca", astrazeneca }, { "janssen", janssen } } },
                    { "total_completed", totalVaccinedComplate}
                });
                counter++;
            }

            for (int i = 0; i < regionsNames.Count; i++)
            {
                dictionary.Add(regionsNames[i], regionsStatistic[i]);
            }

            var bson = new BsonDocument();
            bson.AddRange(dictionary);

            return bson;
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

        private Medical GetMedicalStatistics(string[] medicalTableRecords)
        {

            var totalDoctors = IntParser(medicalTableRecords[1]);
            var totalNurces = IntParser(medicalTableRecords[3]);
            var totalParamedics1 = IntParser(medicalTableRecords[5]);
            var totalParamedics2 = IntParser(medicalTableRecords[7]);
            var others = IntParser(medicalTableRecords[9]);
            var totalMedical = IntParser(medicalTableRecords[11]);

            //TODO:  data for medical for 24h!

            return new Medical
            {
                Total = totalMedical,
                TotalByType = new MedicalTypes
                {
                    Doctror = totalDoctors,
                    Nurces = totalNurces,
                    Paramedics_1 = totalParamedics1,
                    Paramedics_2 = totalParamedics2,
                    Others = others
                }
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

        private static double DevideTwoIntiger(int num1, int num2)
        {
            return Math.Round(((double)(num1) / num2), 4);
        }

    }
}