using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Requests;

namespace POC.Bff.Web.Domain.Profiles
{
    public class ComponentProfile : Profile
    {
        public ComponentProfile()
        {
            CreateMap<ComponentListBySearchTermRequest, ComponentListBySearchTermComponentRequest>()
                .ReverseMap();

            CreateMap<FindPagedDistributionCentersQueryRequest, FindPagedDistributionCentersDto>()
                .ForMember(x => x.StateCodes, y => y.MapFrom(z => z.StateCodeList))
                .ReverseMap();
        }
    }
}