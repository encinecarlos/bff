using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;

namespace POC.Bff.Web.Domain.Profiles
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<CouponResponse, CouponRequest>()
                .ReverseMap();

            CreateMap<CouponProductsPriceDto, CouponProductsPriceRequest>()
                .ForMember(x => x.Products, y => y.MapFrom(z => z.ProductsList))
                .ReverseMap();
        }
    }
}
