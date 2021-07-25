using CoronavirusWebScraper.Services.ServiceModels;
using CoronavirusWebScraper.Web.Models;
using System;
using System.Collections.Generic;

namespace CoronavirusWebScraper.Web.Infrastructure
{
    public static class Conversion
    {
        public static CovidStatisticViewModel ConvertToCovidStatisticViewModel(CovidStatisticServiceModel covidStatistic)
        {
            var statistics = new CovidStatisticViewModel
            {
                Date = DateTime.Parse(covidStatistic.Date).ToString("HH:mm,dd MMM yyy"),
                Active = ConvertToActiveViewModel(covidStatistic.Overall.Active),
                Confirmed = ConvertToConfirmedViewModel(covidStatistic.Overall.Confirmed),
                Recovered = ConverToRecoveredViewModel(covidStatistic.Overall.Recovered),
                Deceased = ConvertToDeceasedViewModel(covidStatistic.Overall.Deceased),
                Tested = ConvertToTestedViewModel(covidStatistic.Overall.Tested),
                Vaccinated = ConvertToVaccinatedViewModel(covidStatistic.Overall.Vaccinated),
                Regions = ConvertToRegionsViewModel(covidStatistic.Regions),
                TotalVaccineByType24 = ConvertToTotalVaccineByTypeViewModel(covidStatistic.TotalVaccineByType24)
            };

            return statistics;
        }

        private static TotalVaccineByType24ViewModel ConvertToTotalVaccineByTypeViewModel(TotalVaccineByType24ServiceModel totalVaccineByType24)
        {
            return new TotalVaccineByType24ViewModel
            {
                TotalAstraZeneca = totalVaccineByType24.TotalAstraZeneca,
                TotalModerna = totalVaccineByType24.TotalModerna,
                TotalComirnaty = totalVaccineByType24.TotalComirnaty,
                TotalJanssen = totalVaccineByType24.TotalJanssen
            };
        }

        private static IEnumerable<RegionsViewModel> ConvertToRegionsViewModel(IEnumerable<RegionsServiceModel> regions)
        {
            var listOfRegions = new List<RegionsViewModel>();

            foreach (var currRegion in regions)
            {
                var regionsToAdd = new RegionsViewModel
                {
                    Name = currRegion.Name,
                    RegionStatistics = new RegionStatisticsViewModel
                    {
                        Confirmed = ConversionToConfirmedViewModel(currRegion.RegionStatistics.Confirmed),
                        Vaccinated = ConversionToVaccinatedViewModel(currRegion.RegionStatistics.Vaccinated),
                    }
                };
                listOfRegions.Add(regionsToAdd);
            }

            return listOfRegions;
        }

        private static VaccinatedViewModel ConversionToVaccinatedViewModel(VaccinatedServiceModel vaccinated)
        {
            return new VaccinatedViewModel
            {
                Total = vaccinated.Total,
                Last = vaccinated.Last,
                LastByType = new VaccineTypeViewModel
                {
                    AstraZeneca = vaccinated.LastByType.AstraZeneca,
                    Comirnaty = vaccinated.LastByType.Comirnaty,
                    Janssen = vaccinated.LastByType.Janssen,
                    Moderna = vaccinated.LastByType.Moderna,
                },
                TotalCompleted = vaccinated.TotalCompleted,
            };
        }

        private static TotalAndLastViewModel ConversionToConfirmedViewModel(TotalAndLastServiceModel confirmed)
        {
            return new TotalAndLastViewModel
            {
                Total = confirmed.Total,
                Last = confirmed.Last,

            };
        }


        private static VaccinatedViewModel ConvertToVaccinatedViewModel(VaccinatedServiceModel vaccinated)
        {
            return new VaccinatedViewModel
            {
                Total = vaccinated.Total,
                Last = vaccinated.Last,
                LastByType = new VaccineTypeViewModel
                {
                    AstraZeneca = vaccinated.LastByType.AstraZeneca,
                    Janssen = vaccinated.LastByType.Janssen,
                    Comirnaty = vaccinated.LastByType.Comirnaty,
                    Moderna = vaccinated.LastByType.Moderna
                },
                TotalCompleted = vaccinated.TotalCompleted
            };
        }

        private static TestedViewModel ConvertToTestedViewModel(TestedServiceModel tested)
        {
            return new TestedViewModel
            {
                Total = tested.Total,
                TotalByType = new TestedByTypeViewModel
                {
                    PCR = tested.TotalByType.PCR,
                    Antigen = tested.TotalByType.Antigen
                },
                Last24 = tested.Last24,
                TotalByType24 = new TestedByTypeViewModel
                {
                    PCR = tested.TotalByType24.PCR,
                    Antigen = tested.TotalByType24.Antigen
                },
            };
        }

        private static TotalAndLastViewModel ConvertToDeceasedViewModel(TotalAndLastServiceModel deceased)
        {
            return new TotalAndLastViewModel
            {
                Total = deceased.Total,
                Last = deceased.Last
            };
        }

        private static TotalAndLastViewModel ConverToRecoveredViewModel(TotalAndLastServiceModel recovered)
        {
            return new TotalAndLastViewModel
            {
                Total = recovered.Total,
                Last = recovered.Last
            };
        }

        private static ConfirmedViewModel ConvertToConfirmedViewModel(ConfirmedServiceModel confirmed)
        {
            return new ConfirmedViewModel
            {
                Total = confirmed.Total,
                Last24 = confirmed.Last24,
                Medical = new MedicalViewModel
                {
                    Total = confirmed.Medical.Total,
                    TotalByType = new MedicalTypesViewModel
                    {
                        Doctror = confirmed.Medical.TotalByType.Doctror,
                        Nurces = confirmed.Medical.TotalByType.Nurces,
                        Paramedics_1 = confirmed.Medical.TotalByType.Paramedics_1,
                        Paramedics_2 = confirmed.Medical.TotalByType.Paramedics_2,
                        Others = confirmed.Medical.TotalByType.Others,
                    },
                    Last24 = confirmed.Medical.Last24,
                    LastByType24 = new MedicalTypesViewModel
                    {
                        Doctror = confirmed.Medical.LastByType24.Doctror,
                        Nurces = confirmed.Medical.LastByType24.Nurces,
                        Paramedics_1 = confirmed.Medical.LastByType24.Paramedics_1,
                        Paramedics_2 = confirmed.Medical.LastByType24.Paramedics_2,
                        Others = confirmed.Medical.LastByType24.Others,
                    }
                },
                TotalByType = new TestedByTypeViewModel
                {
                    PCR = confirmed.TotalByType.PCR,
                    Antigen = confirmed.TotalByType.Antigen,
                },
                TotalByType24 = new TestedByTypeViewModel
                {
                    PCR = confirmed.TotalByType24.PCR,
                    Antigen = confirmed.TotalByType24.Antigen,
                }
            };
        }

        private static ActiveViewModel ConvertToActiveViewModel(ActiveServiceModel active)
        {
            return new ActiveViewModel
            {
                Curent = active.Curent,
                CurrentByType = new ActiveTypesViewModel
                {
                    Hospitalized = active.CurrentByType.Hospitalized,
                    Icu = active.CurrentByType.Icu
                }
            };
        }

    }
}
