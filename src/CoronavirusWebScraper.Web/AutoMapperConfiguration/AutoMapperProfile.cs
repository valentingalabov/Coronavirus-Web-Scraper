using AutoMapper;
using CoronavirusWebScraper.Data.Models;
using CoronavirusWebScraper.Web.Models;

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
        }
    }
}
