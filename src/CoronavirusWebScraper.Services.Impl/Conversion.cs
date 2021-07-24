
using CoronavirusWebScraper.Data.Models;
using CoronavirusWebScraper.Services.ServiceModels;
using System;

namespace CoronavirusWebScraper.Services.Impl
{
    public static class Conversion
    {
        public static CovidStatisticServiceModel ConvertToCovidStatisticDto(CovidStatistic covidStatistic)
        {
            var statistic = new CovidStatisticServiceModel
            {
                Date = covidStatistic.Date,
                Overall = ConversionOverallServiceModel(covidStatistic.Overall),

            };

            return new CovidStatisticServiceModel();
        }

        public static OverallServiceModel ConversionOverallServiceModel(Overall overall)
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

        public static VaccinatedServiceModel ConverVaccinated(Vaccinated vaccinated)
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

        public static TestedServiceModel ConvertTested(Tested tested)
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

        public static TotalAndLastServiceModel ConvertDeceased(TotalAndLast deceased)
        {
            return new TotalAndLastServiceModel
            {
                Total = deceased.Total,
                Last = deceased.Last
            };
        }

        public static TotalAndLastServiceModel ConvertRecovered(TotalAndLast recovered)
        {
            return new TotalAndLastServiceModel
            {
                Total = recovered.Total,
                Last = recovered.Last
            };
        }

        public static ConfirmedServiceModel ConvertConfirmed(Confirmed confirmed)
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

        public static ActiveServiceModel ConvertActive(Active active)
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
