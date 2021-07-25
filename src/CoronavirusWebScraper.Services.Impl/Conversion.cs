
using CoronavirusWebScraper.Data.Models;
using CoronavirusWebScraper.Services.ServiceModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace CoronavirusWebScraper.Services.Impl
{
    public static class Conversion
    {
        public static CovidStatisticServiceModel ConvertToCovidStatisticServiceModel(CovidStatistic covidStatistic)
        {
            var statistics = new CovidStatisticServiceModel
            {
                Date = covidStatistic.Date,
                Overall = ConversionOverallServiceModel(covidStatistic.Overall),
                Regions = ConversionRegionsServiceModel(covidStatistic.Regions)
            };

            return statistics;
        }

        public static string RegionЕКАТТЕCodeConversion(string region)
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

            if (regionsWithCodes.ContainsValue(region.ToUpper()))
            {
                return regionsWithCodes.Where(x => x.Value == region.ToUpper()).Select(x => x.Key).FirstOrDefault();
            }

            return regionsWithCodes[region].ToLower();
        }

        private static IEnumerable<RegionsServiceModel> ConversionRegionsServiceModel(BsonDocument regions)
        {
            var listOfRegins = new List<RegionsServiceModel>();

            foreach (var currRegion in regions)
            {
                var regionToAdd = new RegionsServiceModel
                {
                    Name = RegionЕКАТТЕCodeConversion(currRegion.Name),
                    RegionStatistics = BsonSerializer.Deserialize<RegionStatisticsServiceModel>(currRegion.Value.AsBsonDocument)
                };

                listOfRegins.Add(regionToAdd);
            }

            return listOfRegins;

        }

        private static OverallServiceModel ConversionOverallServiceModel(Overall overall)
        {
            return new OverallServiceModel
            {
                Active = ConvertActive(overall.Active),
                Confirmed = ConvertConfirmed(overall.Confirmed),
                Recovered = ConvertRecovered(overall.Recovered),
                Deceased = ConvertDeceased(overall.Deceased),
                Tested = ConvertTested(overall.Tested),
                Vaccinated = ConverVaccinated(overall.Vaccinated)
            };
        }

        private static VaccinatedServiceModel ConverVaccinated(Vaccinated vaccinated)
        {
            return new VaccinatedServiceModel
            {
                Total = vaccinated.Total,
                Last = vaccinated.Last,
                LastByType = new VaccineTypeServiceModel
                {
                    AstraZeneca = vaccinated.LastByType.AstraZeneca,
                    Janssen = vaccinated.LastByType.Janssen,
                    Comirnaty = vaccinated.LastByType.Comirnaty,
                    Moderna = vaccinated.LastByType.Moderna
                },
                TotalCompleted = vaccinated.TotalCompleted
            };
        }

        private static TestedServiceModel ConvertTested(Tested tested)
        {
            return new TestedServiceModel
            {
                Total = tested.Total,
                TotalByType = new TestedByTypeServiceModel
                {
                    PCR = tested.TotalByType.PCR,
                    Antigen = tested.TotalByType.Antigen
                },
                Last24 = tested.Last24,
                TotalByType24 = new TestedByTypeServiceModel
                {
                    PCR = tested.TotalByType24.PCR,
                    Antigen = tested.TotalByType24.Antigen
                },
            };
        }

        private static TotalAndLastServiceModel ConvertDeceased(TotalAndLast deceased)
        {
            return new TotalAndLastServiceModel
            {
                Total = deceased.Total,
                Last = deceased.Last
            };
        }

        private static TotalAndLastServiceModel ConvertRecovered(TotalAndLast recovered)
        {
            return new TotalAndLastServiceModel
            {
                Total = recovered.Total,
                Last = recovered.Last
            };
        }

        private static ConfirmedServiceModel ConvertConfirmed(Confirmed confirmed)
        {
            return new ConfirmedServiceModel
            {
                Total = confirmed.Total,
                Last24 = confirmed.Last24,
                Medical = new MedicalServiceModel
                {
                    Total = confirmed.Medical.Total,
                    TotalByType = new MedicalTypesServiceModel
                    {
                        Doctror = confirmed.Medical.TotalByType.Doctror,
                        Nurces = confirmed.Medical.TotalByType.Nurces,
                        Paramedics_1 = confirmed.Medical.TotalByType.Paramedics_1,
                        Paramedics_2 = confirmed.Medical.TotalByType.Paramedics_2,
                        Others = confirmed.Medical.TotalByType.Others,
                    },
                    Last24 = confirmed.Medical.Last24,
                    //LastByType24 = new MedicalTypesServiceModel
                    //{
                    //    Doctror = confirmed.Medical.LastByType24.Doctror,
                    //    Nurces = confirmed.Medical.LastByType24.Nurces,
                    //    Paramedics_1 = confirmed.Medical.LastByType24.Paramedics_1,
                    //    Paramedics_2 = confirmed.Medical.LastByType24.Paramedics_2,
                    //    Others = confirmed.Medical.LastByType24.Others,
                    //}
                },
                TotalByType = new TestedByTypeServiceModel
                {
                    PCR = confirmed.TotalByType.PCR,
                    Antigen = confirmed.TotalByType.Antigen,
                },
                TotalByType24 = new TestedByTypeServiceModel
                {
                    PCR = confirmed.TotalByType24.PCR,
                    Antigen = confirmed.TotalByType24.Antigen,
                }
            };
        }

        private static ActiveServiceModel ConvertActive(Active active)
        {
            return new ActiveServiceModel
            {
                Curent = active.Curent,
                CurrentByType = new ActiveTypesServiceModel
                {
                    Hospitalized = active.CurrentByType.Hospitalized,
                    Icu = active.CurrentByType.Icu
                }
            };
        }
    }
}
