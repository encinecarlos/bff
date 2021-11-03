using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Authorizations
{
    public class AuthorizationServiceHandler : IAuthorizationService
    {
        private readonly ICommercialClient _commercialClient;
        private readonly IIdentityClient _identityClient;
        private readonly IResponseService _responseService;

        public AuthorizationServiceHandler(
            IIdentityClient identityClient,
            ICommercialClient commercialClient,
            IResponseService responseService)
        {
            _identityClient = identityClient;
            _commercialClient = commercialClient;
            _responseService = responseService;
        }

        public async Task<Response> GetAuthorizationDetail()
        {
            await _commercialClient.ValidateCurrentOrganization();

            var authorizationResponse = await _identityClient.GetAuthorizationDetail();
            var userDetails = authorizationResponse.Parse<AuthorizationDetailResponse>();

            if (userDetails == null) return _responseService.CreateFailResponse(null);

            Guid.TryParse(userDetails.OrganizationId, out var organizationId);

            var organization = await _commercialClient.GetAuthorizationDetail(organizationId);
            var response = organization.Parse<CommercialAuthorizationResponse>();
            var countryCode = userDetails.CountryCode ?? string.Empty;

            GetUsefulLinks().TryGetValue(countryCode, out var link);

            return _responseService.CreateSuccessResponse(new
            {
                response.GoalsQuotations,
                Account = userDetails,
                response.OrganizationList,
                UsefulLinksUrl = link
            });
        }


        private static Dictionary<string, string> GetUsefulLinks()
        {
            return new Dictionary<string, string>
            {
                {"BR", "https://suporte.plataformasicessolar.com.br/pt-BR/articles/1662032-links-uteis"}
            };
        }
    }
}