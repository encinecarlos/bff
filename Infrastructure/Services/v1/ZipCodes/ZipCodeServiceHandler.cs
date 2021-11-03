using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.ZipCodes
{
    public class ZipCodeServiceHandler : IZipCodeService
    {
        private readonly ICommercialClient _commercialClient;
        private readonly IMapper _mapper;
        private readonly IResponseService _responseService;

        public ZipCodeServiceHandler(ICommercialClient commercialClient, IMapper mapper,
            IResponseService responseService)
        {
            _commercialClient = commercialClient;
            _mapper = mapper;
            _responseService = responseService;
        }

        public async Task<Response> GetAddress(string zipcode, string countryCode)
        {
            var response = await _commercialClient.GetAddress(countryCode, zipcode);
            return CreateResponse(response);
        }

        public async Task<Response> GetAddress(string zipcode)
        {
            var response = await _commercialClient.GetAddress(zipcode);
            return CreateResponse(response);
        }

        private Response CreateResponse(Response response)
        {
            if (response?.Data is null) return response;

            var dto = response.Parse<ZipCodeDto>();

            return _responseService.CreateSuccessResponse(_mapper.Map<ZipCodeResponse>(dto))
                .AddNotifications(response.Notifications);
        }
    }
}