using AutoMapper;
using CoronavirusWebScraper.Services.Impl.ServiceModels;
using CoronavirusWebScraper.Web.Models;
using System.Linq;

namespace CoronavirusWebScraper.Web.AutoMapperConfiguration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CovidStatistic, CovidStatisticsViewModel>();
            CreateMap<Overall, OverallViewModel>();
            CreateMap<Tested, TestedViewModel>();
            CreateMap<TestedByType, TestedByTypeViewModel>();
            CreateMap<VaccineType, VaccineTypeViewModel>();
            CreateMap<Vaccinated, VaccinatedViewModel>();
            CreateMap<TotalAndLast, TotalAndLastViewModel>();
            CreateMap<Medical, MedicalViewModel>();
            CreateMap<MedicalTypes, MedicalTypesViewModel>();
            CreateMap<Confirmed, ConfirmedViewModel>();
            CreateMap<Active, ActiveViewModel>();
            CreateMap<ActiveTypes, ActiveTypesViewModel>();
        }
    }
}
