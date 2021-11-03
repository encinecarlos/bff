using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Loyalty
{
    public class LoyaltyServiceHandler : ILoyaltyService
    {
        private readonly ICommercialClient _commercialClient;
        private readonly IResponseService _responseService;

        public LoyaltyServiceHandler(ICommercialClient commercialClient, IResponseService responseService)
        {
            _commercialClient = commercialClient;
            _responseService = responseService;
        }

        public async Task<Response> FetchLoyaltyPoints(FetchLoyaltyPointsRequest filters)
        {
            var response = await _commercialClient.FetchLoyaltyPoints(filters);

            var ret = response.Parse<FetchLoyaltyPointsResponse>();

            return _responseService.CreateSuccessResponse(ret).AddNotifications(response.Notifications);
        }
    }
}