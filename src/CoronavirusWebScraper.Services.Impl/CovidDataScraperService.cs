using AngleSharp;
using AngleSharp.Dom;
using CoronavirusWebScraper.Data;
using CoronavirusWebScraper.Data.Models;
using CoronavirusWebScraper.Services.ServiceModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            if (document != null)
            {
                await _repository.InsertOneAsync(document);
            }
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
            var currDateAsString = string.Join(" ", date, month, year, time);
            var currDate = DateTime.Parse(currDateAsString);

            var dataDate = currDate.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");

            var dateScraped = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            //No scrape data if already scraped for current day
            var currentDayStatistics = await _repository.FindOneAsync(filter => filter.Date == dataDate);
            if (currentDayStatistics != null)
            {
                return null;
            }

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
                        Medical = GetMedicalStatistics(medicalTableRecords, currDate),
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
                    },
                    Active = new ActivePrc
                    {
                        HospotalizedPerActive = DevideTwoIntiger(hospitalized, active),
                        IcuPerHospitalized = DevideTwoIntiger(intensiveCare, hospitalized)
                    }
                }
            };

            if (covidStatistics.Overall.Confirmed.Medical.Last24 != 0)
            {
                covidStatistics.Stats.ConfirmedPrc.MedicalPcr
                    = DevideTwoIntiger(covidStatistics.Overall.Confirmed.Medical.Last24, covidStatistics.Overall.Confirmed.Last24);
            }

            var convertedRegions = Conversion.ConvertToRegionsServiceModel(covidStatistics.Regions);

            covidStatistics.ConditionResult =
                GetConditionResult(covidStatistics.Overall.Tested, covidStatistics.Overall.Confirmed, covidStatistics.Overall.Vaccinated, convertedRegions);

            return covidStatistics;
        }

        private BsonDocument GetConditionResult(Tested tested, Confirmed confirmed, Vaccinated vaccinated, IEnumerable<RegionsServiceModel> convertedRegions)
        {
            var condition = "approved";
            var sb = new StringBuilder();
            var conditionResult = new BsonDocument();

            if (tested.Total != tested.TotalByType.PCR + tested.TotalByType.Antigen)
            {
                condition = "discrepancy";
                sb.AppendLine($"Sum of total tests must be equal to sum of antigen and  pcr total tests!");
                conditionResult.Add("tested/total", new BsonDocument { { "expected", tested.Total }, { "actual", tested.TotalByType.PCR + tested.TotalByType.Antigen } });

            }
            if (tested.TotalByType24.PCR + tested.TotalByType24.Antigen != tested.Last24)
            {
                condition = "discrepancy";
                sb.AppendLine($"Sum of total tests for last 24h must be equal to sum of antigen and pcr total tests for last 24h!");
                conditionResult.Add("tested/last", new BsonDocument { { "expected", tested.Last24 }, { "actual", tested.TotalByType24.PCR + tested.TotalByType24.Antigen } });

            }
            if (confirmed.Total != confirmed.TotalByType.PCR + confirmed.TotalByType.Antigen)
            {
                condition = "discrepancy";
                sb.AppendLine($"Sum of total confirmed tests is not equal to sum of total confirmed antigen and pcr tests!");
                conditionResult.Add("confirmed/total ", new BsonDocument { { "expected", confirmed.Total }, { "actual", confirmed.TotalByType.PCR + confirmed.TotalByType.Antigen } });
            }
            if (confirmed.Last24 != confirmed.TotalByType24.PCR + confirmed.TotalByType24.Antigen)
            {
                condition = "discrepancy";
                sb.AppendLine($"Sum of total confirmed tests for last 24h is not equal to sum of total confirmed antigen and pcr tests for last 24h!");
                conditionResult.Add("confirmed/last ", new BsonDocument { { "expected", confirmed.Last24 }, { "actual", confirmed.TotalByType24.PCR + confirmed.TotalByType24.Antigen } });
            }
            if (vaccinated.Last != vaccinated.LastByType.AstraZeneca + vaccinated.LastByType.Comirnaty + vaccinated.LastByType.Moderna + vaccinated.LastByType.Janssen)
            {
                condition = "discrepancy";
                sb.AppendLine($"Sum of total vaccinated for last 24h is not equal to sum of total vaccinated by vaccine type for last 24h!");
                conditionResult.Add("vaccinated/last ", new BsonDocument { { "expected", vaccinated.Last }, { "actual", vaccinated.LastByType.AstraZeneca + vaccinated.LastByType.Comirnaty + vaccinated.LastByType.Moderna + vaccinated.LastByType.Janssen } });
            }

            if (confirmed.Total != convertedRegions.Sum(x => x.RegionStatistics.Confirmed.Total))
            {
                condition = "discrepancy";
                sb.AppendLine($"Sum of total confirmed tests is not equal to sum of total confirmed tests for all regions!");
                conditionResult.Add("confirmed/total ", new BsonDocument { { "expected", confirmed.Total }, { "actual", convertedRegions.Sum(x => x.RegionStatistics.Confirmed.Total) } });
            }
            if (confirmed.Last24 != convertedRegions.Sum(x => x.RegionStatistics.Confirmed.Last))
            {
                condition = "discrepancy";
                sb.AppendLine($"Sum of total confirmed tests for last 24h is not equal to sum of total confirmed tests for all regions for last 24h!");
                conditionResult.Add("confirmed/last ", new BsonDocument { { "expected", confirmed.Last24 }, { "actual", convertedRegions.Sum(x => x.RegionStatistics.Confirmed.Last) } });
            }
            if (vaccinated.Total != convertedRegions.Sum(x => x.RegionStatistics.Vaccinated.Total))
            {
                condition = "discrepancy";
                sb.AppendLine($"Sum of total vaccinated is not equal to sum of vaccinated for all regions!");
                conditionResult.Add("vaccinated/total ", new BsonDocument { { "expected", vaccinated.Total }, { "actual", convertedRegions.Sum(x => x.RegionStatistics.Vaccinated.Total) } });
            }
            if (vaccinated.Last != convertedRegions.Sum(x => x.RegionStatistics.Vaccinated.Last))
            {
                condition = "discrepancy";
                sb.AppendLine($"Sum of total vaccinated for last 24h is not equal to sum of vaccinated for all regions for last 24h!");
                conditionResult.Add("vaccinated/last ", new BsonDocument { { "expected", vaccinated.Last }, { "actual", convertedRegions.Sum(x => x.RegionStatistics.Vaccinated.Last) } });
            }
            if (vaccinated.Last != convertedRegions.Sum(x => x.RegionStatistics.Vaccinated.LastByType.AstraZeneca) +
                convertedRegions.Sum(x => x.RegionStatistics.Vaccinated.LastByType.Comirnaty) +
                convertedRegions.Sum(x => x.RegionStatistics.Vaccinated.LastByType.Janssen) +
                convertedRegions.Sum(x => x.RegionStatistics.Vaccinated.LastByType.Moderna))
            {
                condition = "discrepancy";
                sb.AppendLine($"Sum of total vaccinated for last 24h is not equal to sum of vaccinated for all regions by type for last 24h!");
                conditionResult.Add("vaccinated/last ", new BsonDocument { { "expected", vaccinated.Last }, { "actual",convertedRegions.Sum(x => x.RegionStatistics.Vaccinated.LastByType.AstraZeneca) +
                convertedRegions.Sum(x => x.RegionStatistics.Vaccinated.LastByType.Comirnaty) +
                convertedRegions.Sum(x => x.RegionStatistics.Vaccinated.LastByType.Janssen) +
                convertedRegions.Sum(x => x.RegionStatistics.Vaccinated.LastByType.Moderna) } });
            }

            var result = new BsonDocument
            {
                { "condition" , condition}
            };

            if (condition == "discrepancy")
            {
                result.Add("condition-description", sb.ToString());
                result.Add("tested-fields", conditionResult);
            }

            return result;
        }

        private BsonDocument GetAllRegionsData(IElement[] allTebles)
        {
            var regionsNames = new List<string>();
            var regionsStatistics = new List<BsonDocument>();
            var regionsStatisticsData
                = new Dictionary<string, BsonDocument>();

            var vaccinatedByRegions = allTebles[5]
                .QuerySelectorAll("td")
                .SkipLast(7)
                .Select(x => x.TextContent)
                .ToArray();
            var confirmedByRegionTableRecords = allTebles[3]
                .QuerySelectorAll("td")
                .SkipLast(3)
                .Select(x => x.TextContent)
                .ToArray();

            for (int i = 0; i < confirmedByRegionTableRecords.Length; i += 3)
            {
                var regionCode = Conversion.RegionЕКАТТЕCodeConversion(confirmedByRegionTableRecords[i]);
                var confirmed = IntParser(confirmedByRegionTableRecords[i + 1]);
                var confirmed24 = IntParser(confirmedByRegionTableRecords[i + 2]);

                regionsNames.Add(regionCode);
                regionsStatistics.Add(new BsonDocument {
                    { "confirmed", new BsonDocument {
                        { "total", confirmed }, { "last", confirmed24 } }
                    }
                });
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

                regionsStatistics[counter].Add("vaccinated", new BsonDocument
                {
                    { "total", totalVaccinated },
                    { "last", totalVaccinated24 },
                    { "last_by_type", new BsonDocument {
                        { "comirnaty", comirnaty },
                        { "moderna", moderna },
                        { "astrazeneca", astrazeneca },
                        { "janssen", janssen } } 
                    },
                    { "total_completed", totalVaccinedComplate}
                });
                counter++;
            }

            for (int i = 0; i < regionsNames.Count; i++)
            {
                regionsStatisticsData.Add(regionsNames[i], regionsStatistics[i]);
            }

            return regionsStatisticsData.ToBsonDocument();
        }

        private Medical GetMedicalStatistics(string[] medicalTableRecords, DateTime currentDate)
        {
            var totalDoctors = IntParser(medicalTableRecords[1]);
            var totalNurces = IntParser(medicalTableRecords[3]);
            var totalParamedics1 = IntParser(medicalTableRecords[5]);
            var totalParamedics2 = IntParser(medicalTableRecords[7]);
            var others = IntParser(medicalTableRecords[9]);
            var totalMedical = IntParser(medicalTableRecords[11]);

            var previousDate = currentDate.AddDays(-1).ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");

            var medialForPreviousDay = _repository
                .FilterBy(filter => filter.Date == previousDate, projected
                => projected.Overall.Confirmed.Medical)
                .FirstOrDefault();

            if (medialForPreviousDay == null)
            {
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
                    },
                    Last24 = 0,
                    LastByType24 = new MedicalTypes
                    {
                        Doctror = 0,
                        Nurces = 0,
                        Paramedics_1 = 0,
                        Paramedics_2 = 0,
                        Others = 0
                    }
                };
            }

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
                },
                Last24 = totalMedical - medialForPreviousDay.Total,
                LastByType24 = new MedicalTypes
                {
                    Doctror = totalDoctors - medialForPreviousDay.TotalByType.Doctror,
                    Nurces = totalNurces - medialForPreviousDay.TotalByType.Nurces,
                    Paramedics_1 = totalParamedics1 - medialForPreviousDay.TotalByType.Paramedics_1,
                    Paramedics_2 = totalParamedics2 - medialForPreviousDay.TotalByType.Paramedics_2,
                    Others = others - medialForPreviousDay.TotalByType.Others
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