namespace CoronavirusWebScraper.Services.Impl
{
    using System.Collections.Generic;
    using System.Linq;

    using CoronavirusWebScraper.Data.Models;
    using CoronavirusWebScraper.Services.ServiceModels;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;

    public static class Conversion
    {
        public static CovidStatisticServiceModel ConvertToCovidStatisticServiceModel(CovidStatistic covidStatistic)
        {
            var statistics = new CovidStatisticServiceModel
            {
                Date = covidStatistic.Date,
                Overall = ConvertToOverallServiceModel(covidStatistic.Overall),
                Regions = ConvertToRegionsServiceModel(covidStatistic.Regions),
            };
            statistics.TotalVaccineByType24 = new TotalVaccineByType24ServiceModel
            {
                TotalAstraZeneca = statistics.Regions.Sum(x => x.RegionStatistics.Vaccinated.LastByType.AstraZeneca),
                TotalComirnaty = statistics.Regions.Sum(x => x.RegionStatistics.Vaccinated.LastByType.Comirnaty),
                TotalJanssen = statistics.Regions.Sum(x => x.RegionStatistics.Vaccinated.LastByType.Janssen),
                TotalModerna = statistics.Regions.Sum(x => x.RegionStatistics.Vaccinated.LastByType.Moderna),
            };

            return statistics;
        }

        public static AnalysisServiceModel ConvertToAnalysisServiceModel(CovidStatistic covidStatistic)
        {
            return new AnalysisServiceModel
            {
                Active = covidStatistic.Overall.Active.Curent,
                Hospitalized = covidStatistic.Overall.Active.CurrentByType.Hospitalized,
                Icu = covidStatistic.Overall.Active.CurrentByType.Icu,
                Infected = covidStatistic.Overall.Confirmed.Total,
                TotalTests = covidStatistic.Overall.Tested.Total,
            };
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

        public static IEnumerable<RegionsServiceModel> ConvertToRegionsServiceModel(BsonDocument regions)
        {
            var listOfRegins = new List<RegionsServiceModel>();

            foreach (var currRegion in regions)
            {
                var regionToAdd = new RegionsServiceModel
                {
                    Name = RegionЕКАТТЕCodeConversion(currRegion.Name),
                    RegionStatistics = BsonSerializer.Deserialize<RegionStatisticsServiceModel>(currRegion.Value.AsBsonDocument),
                };

                listOfRegins.Add(regionToAdd);
            }

            return listOfRegins;
        }

        private static OverallServiceModel ConvertToOverallServiceModel(Overall overall)
        {
            return new OverallServiceModel
            {
                Active = ConvertToActiveServiceModel(overall.Active),
                Confirmed = ConvertToConfirmedServiceModel(overall.Confirmed),
                Recovered = ConvertToRecoveredServiceModel(overall.Recovered),
                Deceased = ConvertToDeceasedServiceModel(overall.Deceased),
                Tested = ConvertToTestedServicModel(overall.Tested),
                Vaccinated = ConverToVaccinatedServiceModel(overall.Vaccinated),
            };
        }

        private static VaccinatedServiceModel ConverToVaccinatedServiceModel(Vaccinated vaccinated)
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
                    Moderna = vaccinated.LastByType.Moderna,
                },
                TotalCompleted = vaccinated.TotalCompleted,
            };
        }

        private static TestedServiceModel ConvertToTestedServicModel(Tested tested)
        {
            return new TestedServiceModel
            {
                Total = tested.Total,
                TotalByType = new TestedByTypeServiceModel
                {
                    PCR = tested.TotalByType.PCR,
                    Antigen = tested.TotalByType.Antigen,
                },
                Last24 = tested.Last24,
                TotalByType24 = new TestedByTypeServiceModel
                {
                    PCR = tested.TotalByType24.PCR,
                    Antigen = tested.TotalByType24.Antigen,
                },
            };
        }

        private static TotalAndLastServiceModel ConvertToDeceasedServiceModel(TotalAndLast deceased)
        {
            return new TotalAndLastServiceModel
            {
                Total = deceased.Total,
                Last = deceased.Last,
            };
        }

        private static TotalAndLastServiceModel ConvertToRecoveredServiceModel(TotalAndLast recovered)
        {
            return new TotalAndLastServiceModel
            {
                Total = recovered.Total,
                Last = recovered.Last,
            };
        }

        private static ConfirmedServiceModel ConvertToConfirmedServiceModel(Confirmed confirmed)
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
                    LastByType24 = new MedicalTypesServiceModel
                    {
                        Doctror = confirmed.Medical.LastByType24.Doctror,
                        Nurces = confirmed.Medical.LastByType24.Nurces,
                        Paramedics_1 = confirmed.Medical.LastByType24.Paramedics_1,
                        Paramedics_2 = confirmed.Medical.LastByType24.Paramedics_2,
                        Others = confirmed.Medical.LastByType24.Others,
                    },
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
                },
            };
        }

        private static ActiveServiceModel ConvertToActiveServiceModel(Active active)
        {
            return new ActiveServiceModel
            {
                Curent = active.Curent,
                CurrentByType = new ActiveTypesServiceModel
                {
                    Hospitalized = active.CurrentByType.Hospitalized,
                    Icu = active.CurrentByType.Icu,
                },
            };
        }
    }
}
