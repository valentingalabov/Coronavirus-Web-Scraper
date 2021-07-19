//using AngleSharp;
//using AngleSharp.Dom;
//using MongoDB.Bson;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace dataScraperExample
//{
//    public class CreateEntity
//    {
//        private IConfiguration config;
//        private IBrowsingContext context;
//        private string covidUrl;
//        private AngleSharp.Dom.IDocument document;
//        private string covidStatisticUrl;
//        private IDocument statisticDocument;
//        private string[] statistics;
//        private IElement[] allTebles;
//        private int totalTests;
//        private string[] totalTestsByTypeTableRecords;
//        private int totalPcr;
//        private int totalAntigen;
//        private int totalTests24;
//        private int totalPcr24;
//        private int totalAntigen24;
//        private int totalConfirmed;
//        private string[] confirmedTestsByTypeTableRecords;
//        private string[] confirmedByRegionTableRecords;
//        private int confirmedPcr;
//        private int confirmedAntigen;
//        private int confirmedPcr24;
//        private int confirmedAntigen24;
//        private int totalConfirmed24;
//        private int active;
//        private int hospitalized;
//        private int intensiveCare;
//        private int vaccinated;
//        private int vaccinated24;
//        private string[] vaccinatedTableRecords;
//        private int comirnaty;
//        private int moderna;
//        private int astraZeneca;
//        private int janssen;
//        private int totalVaccinatedComplate;
//        private string status;

//        public  CreateEntity()
//        {
//            this.config = Configuration.Default.WithDefaultLoader();
//            this.context = BrowsingContext.New(config);

//            this.covidUrl = "https://coronavirus.bg/";
//            this.document = context.OpenAsync(this.covidUrl).GetAwaiter().GetResult();

//            this.covidStatisticUrl = this.covidUrl + document.QuerySelector(".statistics-sub-header.nsi").GetAttribute("href");
//            this.statisticDocument = context.OpenAsync(this.covidStatisticUrl)
//                .GetAwaiter()
//                .GetResult();

//            this.statistics = document.QuerySelectorAll(".statistics-container > div > p").Select(x => x.TextContent).ToArray();
//            this.allTebles = this.statisticDocument.QuerySelectorAll(".table").ToArray();

//            //Tests
//            this.totalTestsByTypeTableRecords = allTebles[1].QuerySelectorAll("td").Select(x => x.TextContent).ToArray();

//            this.totalTests = IntParser(statistics[0]);
//            this.totalPcr = IntParser(totalTestsByTypeTableRecords[1]);
//            this.totalAntigen = IntParser(totalTestsByTypeTableRecords[4]);

//            this.totalTests24 = IntParser(statistics[2]);
//            this.totalPcr24 = IntParser(totalTestsByTypeTableRecords[2]);
//            this.totalAntigen24 = IntParser(totalTestsByTypeTableRecords[5]);

//            //confirmed
//            this.confirmedTestsByTypeTableRecords = allTebles[2].QuerySelectorAll("td").Select(x => x.TextContent).ToArray();
//            this.confirmedByRegionTableRecords = allTebles[3].QuerySelectorAll("td").SkipLast(3).Select(x => x.TextContent).ToArray();

//            this.totalConfirmed = IntParser(this.statistics[4]);
//            this.confirmedPcr = IntParser(confirmedTestsByTypeTableRecords[1]);
//            this.confirmedAntigen = IntParser(confirmedTestsByTypeTableRecords[4]);

//            this.confirmedPcr24 = IntParser(confirmedTestsByTypeTableRecords[2]);
//            this.confirmedAntigen24 = IntParser(confirmedTestsByTypeTableRecords[5]);
//            this.totalConfirmed24 = IntParser(confirmedTestsByTypeTableRecords[8]);


//            this.active = IntParser(statistics[6]);

//            this.hospitalized = IntParser(statistics[12]);
//            this.intensiveCare = IntParser(statistics[14]);

//            //vaccinated
//            this.vaccinated = IntParser(statistics[20]);
//            this.vaccinated24 = IntParser(statistics[22]);

//            this.vaccinatedTableRecords = allTebles[5].QuerySelectorAll("tr").Last().QuerySelectorAll("td").Select(x => x.TextContent).ToArray();
//            this.comirnaty = IntParser(vaccinatedTableRecords[2]);
//            this.moderna = IntParser(vaccinatedTableRecords[3]);
//            this.astraZeneca = IntParser(vaccinatedTableRecords[4]);
//            this.janssen = IntParser(vaccinatedTableRecords[5]);
//            this.totalVaccinatedComplate = IntParser(vaccinatedTableRecords[6]);

//            //medicalTable
//            var medicalTableRecords = allTebles[4].QuerySelectorAll("td").Select(x => x.TextContent).ToArray();


//            this.status = "approved";
//        }



//        public BsonDocument GetDateAndCountryData()
//        {

//            var currentDateSpan = this.document.QuerySelector(".statistics-header-wrapper span").TextContent.Split(" ");
//            var time = currentDateSpan[2];
//            var date = currentDateSpan[5];
//            var month = currentDateSpan[6];
//            var year = currentDateSpan[7];
//            var currDateAsString = date + " " + month + " " + year + " " + time;
//            var currDate = DateTime.Parse(currDateAsString);

//            var dateToAdd = currDate.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
//            var dateScraped = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

//            var statusDoc = CheckDataState();

//            var document = new BsonDocument
//            {
//                { "date", dateToAdd },
//                { "date_scraped", dateScraped },
//                { "country", "BG"},
//                { "overall", GetOverallStats()},
//                {"regions",  new BsonArray(GetAllRegionsData()) },
//                { "stats", GetStatsByPercentage()},
//                { "status", this.status}

//            };



//            if (this.status == "discrepancy")
//            {
//                document.Add("status_result", statusDoc);
//            }


//            return document;
//        }

//        private BsonDocument CheckDataState()
//        {
//            var sb = new StringBuilder();
//            var fields = new BsonDocument();


//            if (totalTests24 != totalPcr24 + totalAntigen24)
//            {
//                status = "discrepancy";
//                sb.AppendLine($"Sum of total tests for 24h must be {totalTests24} but it is {totalPcr24 + totalAntigen24}");
//                fields.Add("TotalTestFor24h ", new BsonDocument { { "expected", totalTests24 }, { "actual", totalPcr24 + totalAntigen24 } });

//            }
//            if (totalConfirmed24 != confirmedAntigen24 + confirmedPcr24)
//            {
//                status = "discrepancy";
//                sb.AppendLine($"Sum of total confirmed tests for 24h must be {totalConfirmed24} but it is {confirmedAntigen24 + confirmedPcr24}");
//                fields.Add("TotalConfrimedTestFor24h ", new BsonDocument { { "expected", totalConfirmed24 }, { "actual", confirmedAntigen24 + confirmedPcr24 } });
//            }


//            var totalConfirmed24Test = 0;

//            for (int i = 0; i < confirmedByRegionTableRecords.Length; i += 3)
//            {
//                totalConfirmed24Test += IntParser(confirmedByRegionTableRecords[i + 2]);

//            }

//            if (totalConfirmed24 != totalConfirmed24Test)
//            {
//                status = "discrepancy";
//                sb.AppendLine($"Sum of total confirmed tests for 24h for all regions must be {totalConfirmed24} but it is {totalConfirmed24Test}");
//                fields.Add("TotalConfrimedTestForAllRegions24h ", new BsonDocument { { "expected", totalConfirmed24 }, { "actual", totalConfirmed24Test } });
//            }

//            if (vaccinated24 != comirnaty + moderna + astraZeneca + janssen)
//            {
//                status = "discrepancy";
//                sb.AppendLine($"Sum of total vaccinated by type for 24h must be {vaccinated24} but it is {comirnaty + moderna + astraZeneca + janssen}");
//                fields.Add("TotalVaccinated24h ", new BsonDocument { { "expected", vaccinated24 }, { "actual", comirnaty + moderna + astraZeneca + janssen } });
//            }

//            if (sb.Length > 0)
//            {
//                fields.Add("description", sb.ToString().Trim());
//            }

//            return fields;



//        }

//        private BsonDocument GetStatsByPercentage()
//        {

//            return new BsonDocument
//            {
//                { "tested", GetTestedPercentage()},
//                { "confirmed", GetConfirmedPercentage()},
//                { "active" , GetActivePercentage()}
//            };

//        }

//        private BsonDocument GetActivePercentage()
//        {
//            var hospitalizedPerActive = DevideTwoIntiger(hospitalized, active);
//            var icuPerHospitalized = DevideTwoIntiger(intensiveCare, active);



//            return new BsonDocument
//            {
//                { "hospitalized_per_active",hospitalizedPerActive },
//                { "icu_per_hospitalized", icuPerHospitalized}
//            };
//        }

//        private BsonDocument GetConfirmedPercentage()
//        {
//            var totalPerTestedPcr = DevideTwoIntiger(totalConfirmed, totalTests);
//            var lastPerTestedPrc = DevideTwoIntiger(totalConfirmed24, totalTests24);

//            var totalByTypePrcPCR = DevideTwoIntiger(confirmedPcr, totalConfirmed);
//            var totalByTypePrcAntigen = DevideTwoIntiger(confirmedAntigen, totalConfirmed);


//            var lastByTypePrcPCR24 = DevideTwoIntiger(confirmedPcr24, totalConfirmed24);
//            var lastByTypePrcAntigen24 = DevideTwoIntiger(confirmedAntigen24, totalConfirmed24);


//            return new BsonDocument
//            {
//                { "total_per_tested_prc", totalPerTestedPcr},
//                { "last_per_tested_prc", lastPerTestedPrc},
//                { "total_by_type_prc", new BsonDocument { { "pcr", totalByTypePrcPCR }, { "antigen", totalByTypePrcAntigen } } },
//                { "last_by_type_prc" , new BsonDocument { { "pcr", lastByTypePrcPCR24 }, { "antigen", lastByTypePrcAntigen24 } } },
//                { "medical_prc" , "-"}
//            };
//        }

//        private BsonDocument GetTestedPercentage()
//        {

//            var pcrPercentage = DevideTwoIntiger(totalPcr, totalTests);
//            var antigenPercentage = DevideTwoIntiger(totalAntigen, totalTests);
//            var pcrPercentage24 = DevideTwoIntiger(totalPcr24, totalTests24);
//            var antigenPercentage24 = DevideTwoIntiger(totalAntigen24, totalTests24);

//            return new BsonDocument
//            {
//                { "total_by_type_prc", new BsonDocument { {"pcr", pcrPercentage }, {"antigen", antigenPercentage } } },
//                { "last_by_type_prc", new BsonDocument { {"pcr", pcrPercentage24 }, {"antigen", antigenPercentage24 } } },

//            };
//        }

//        private List<BsonDocument> GetAllRegionsData()
//        {
//            var regions = new List<BsonDocument>();


//            var vaccinatedByRegions = allTebles[5].QuerySelectorAll("td").SkipLast(7).Select(x => x.TextContent).ToArray();



//            for (int i = 0; i < confirmedByRegionTableRecords.Length; i += 3)
//            {
//                var regionCode = GetRegionЕКАТТЕCode(confirmedByRegionTableRecords[i]).ToLower();
//                var confirmed = IntParser(confirmedByRegionTableRecords[i + 1]);
//                var confirmed24 = IntParser(confirmedByRegionTableRecords[i + 2]);
//                var currentRegionDocument = new BsonDocument();

//                currentRegionDocument.Add(regionCode, new BsonDocument { { "confirmed", new BsonDocument { { "total", confirmed }, { "last", confirmed24 } } } });

//                regions.Add(currentRegionDocument);
//            }

//            var counter = 0;
//            for (int i = 0; i < vaccinatedByRegions.Length; i += 7)
//            {
//                var totalVaccinated = IntParser(vaccinatedByRegions[i + 1]);
//                var comirnaty = IntParser(vaccinatedByRegions[i + 2]);
//                var moderna = IntParser(vaccinatedByRegions[i + 3]);
//                var astrazeneca = IntParser(vaccinatedByRegions[i + 4]);
//                var janssen = IntParser(vaccinatedByRegions[i + 5]);
//                var totalVaccinedComplate = IntParser(vaccinatedByRegions[i + 6]);
//                var totalVaccinated24 = comirnaty + moderna + astrazeneca + janssen;

//                var doc = regions[counter];


//                regions[counter].Add("vaccinated", new BsonDocument
//                {
//                    { "total", totalVaccinated },
//                    { "last", totalVaccinated24 },
//                    { "last_by_type", new BsonDocument { { "comirnaty", comirnaty }, { "moderna", moderna }, { "astrazeneca", astrazeneca }, { "janssen", janssen } } }
//                });
//                regions[counter].Add("total_completed", totalVaccinedComplate);
//                counter++;
//            }


//            return regions;
//        }


//        private string GetRegionЕКАТТЕCode(string region)
//        {
//            var regionsWithCodes = new Dictionary<string, string>()
//            {
//                { "Благоевград", "BLG" },
//                { "Бургас", "BGS" },
//                { "Варна", "VAR" },
//                { "Велико Търново", "VTR" },
//                { "Видин", "VID" },
//                { "Враца", "VRC" },
//                { "Габрово", "GAB" },
//                { "Добрич", "DOB" },
//                { "Кърджали", "KRZ" },
//                { "Кюстендил", "KNL" },
//                { "Ловеч", "LOV" },
//                { "Монтана", "MON" },
//                { "Пазарджик", "PAZ" },
//                { "Перник", "PER" },
//                { "Плевен", "PVN" },
//                { "Пловдив", "PDV" },
//                { "Разград", "RAZ" },
//                { "Русе", "RSE" },
//                { "Силистра", "SLS" },
//                { "Сливен", "SLV" },
//                { "Смолян", "SML" },
//                { "София", "SFO" },
//                { "София (столица)", "SOF" },
//                { "Стара Загора", "SZR" },
//                { "Търговище", "TGV" },
//                { "Хасково", "HKV" },
//                { "Шумен", "SHU" },
//                { "Ямбол", "JAM" },
//            };

//            return regionsWithCodes[region];
//        }


//        private BsonDocument GetOverallStats()
//        {
//            return new BsonDocument
//            {
//                { "tested", GetTestedStatistics()},
//                {"confirmed",  GetConfirmedStatistics()},
//                { "active", GetActiveStatistic()},
//                {"recovered", GetRecoveredStatistic()},
//                { "deceased", GetDeceasedStatistic()},
//                { "vaccinated", GetVaccinatedStatistic()}
//            };

//        }

//        private BsonDocument GetVaccinatedStatistic()
//        {
//            return new BsonDocument()
//            {
//                { "total", 590495 },
//                // Ваксинирани (24 ч)
//                { "last", 10503 },
//                { "last_by_type", new BsonDocument { { "Comirnaty", comirnaty }, { "Moderna", moderna }, { "AstraZeneca", astraZeneca }, { "Janssen", janssen } } },
//                { "total_completed", totalVaccinatedComplate}
//            };
//        }

//        private BsonDocument GetDeceasedStatistic()
//        {
//            var deceased = IntParser(statistics[16]);
//            var deceased24 = IntParser(statistics[18]);

//            return new BsonDocument
//            {
//                // Починали (общо)
//                { "total", deceased },
//                // Починали (24 ч)
//                { "last", deceased24 }
//            };
//        }


//        private BsonDocument GetRecoveredStatistic()
//        {
//            var totalRecovered = IntParser(statistics[8]);
//            var totalRecovered24 = IntParser(statistics[10]);

//            return new BsonDocument
//            {
//                { "total", totalRecovered },
//                // Излекувани (24 ч)
//                { "last", totalRecovered24 }
//            };
//        }

//        private BsonDocument GetActiveStatistic()
//        {
//            return new BsonDocument
//            {
//                {"current", active },
//                { "current_by_type", new BsonDocument { { "hospitalized", hospitalized }, { "icu", intensiveCare } }}
//            };
//        }

//        private BsonDocument GetConfirmedStatistics()
//        {
//            return new BsonDocument
//            {
//                {"total", totalConfirmed },
//                { "total_by_type", new BsonDocument
//                    { { "pcr", confirmedPcr }, {"antigen", confirmedAntigen } } },
//                {"last",  totalConfirmed24},
//                {"last_by_type", new BsonDocument { { "pcr", confirmedPcr24 }, { "antigen", confirmedAntigen24 } } },
//                { "medical", GetMedicalStatistics() },


//            };
//        }

//        private BsonDocument GetMedicalStatistics()
//        {
            
//            var totalDoctors = IntParser(medicalTableRecords[1]);
//            var totalNurces = IntParser(medicalTableRecords[3]);
//            var totalParamedics1 = IntParser(medicalTableRecords[5]);
//            var totalParamedics2 = IntParser(medicalTableRecords[7]);
//            var others = IntParser(medicalTableRecords[9]);
//            var totalMedical = IntParser(medicalTableRecords[11]);

//            //TODO:  data for medical for 24h!

//            return new BsonDocument
//            {
//                { "total", totalMedical },
//                { "total_by_type", new BsonDocument{{ "doctor", totalDoctors },
//                        { "nurces", totalNurces }, { "paramedics_1", totalParamedics1 },
//                        { "paramedics_2", totalParamedics2 }, { "other", others } }},
//                { "last", "-" },
//                { "last_by_type", new BsonDocument{{ "doctor", "-" },
//                        { "nurces", "-" }, { "paramedics_1", "-" },
//                        { "paramedics_2", "-" }, { "other", "-" } }}
//            };


//        }
//        private BsonDocument GetTestedStatistics()
//        {
//            return new BsonDocument()
//            {
//                { "total", totalTests},
//                { "total_by_type", new BsonDocument { { "pcr", totalPcr }, { "antigen", totalAntigen} } },
//                { "last",totalTests24 },
//                { "last_by_type", new BsonDocument { { "pcr", totalPcr24 }, { "antigen", totalAntigen24 } } }
//            };
//        }


//        private static int IntParser(string num)
//        {
//            if (num == "-")
//            {
//                return 0;
//            }

//            return int.Parse(num.Trim().Replace(" ", string.Empty));
//        }

//        private static double DevideTwoIntiger(int num1, int num2)
//        {
//            return Math.Round(((double)(num1) / num2), 4);
//        }

//    }
//}
