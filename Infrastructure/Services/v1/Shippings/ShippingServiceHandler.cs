using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Shippings
{
    public class ShippingServiceHandler : IShippingService
    {
        private const int PickupAvailabilityAfterPayment = 7;
        private const int DeliveryTimeAfterPickUp = 15;

        private readonly ICommercialClient _commercialClient;
        private readonly IConfigurationClient _configurationClient;
        private readonly IResponseService _responseService;

        public ShippingServiceHandler(ICommercialClient commercialClient,
            IConfigurationClient configurationClient, IResponseService responseService)
        {
            _commercialClient = commercialClient;
            _configurationClient = configurationClient;
            _responseService = responseService;
        }

        public async Task<Response> FindShippingStepConfig()
        {
            var shippingCompanies = await _commercialClient.FindShippingCompanies();

            return _responseService.CreateSuccessResponse(new
            {
                ShippingCompanyList = shippingCompanies.Data,
                PickupAvailabilityAfterPayment,
                DeliveryTimeAfterPickUp
            });
        }

        public async Task<Response> FindAddressTypes()
        {
            return await _commercialClient.FindAddressTypes();
        }

        public async Task<Response> FindShippingTypes()
        {
            return await _commercialClient.FindShippingTypes();
        }

        public async Task<Response> FindShippingCompanies()
        {
            return await _commercialClient.FindShippingCompanies();
        }

        public async Task<Response> GetShippingCompany(string document)
        {
            return await _commercialClient.GetShippingCompany(document);
        }

        public async Task<Response> CreateShippingCompany(ShippingCompanyRequest command)
        {
            if (command.Address != null)
            {
                await _configurationClient.ValidStateCode(command.Address.CountryCode, command.Address.StateCode);
            }

            return await _commercialClient.CreateShippingCompany(command);
        }

        public async Task<Response> DeleteShippingCompanies(string document)
        {
            return await _commercialClient.DeleteShippingCompanies(document);
        }

        public async Task<Response> UpdateShippingCompany(string document, ShippingCompanyRequest command)
        {
            if (command.Address != null)
            {
                await _configurationClient.ValidStateCode(command.Address.CountryCode, command.Address.StateCode);
            }

            return await _commercialClient.UpdateShippingCompany(document, command);
        }
    }
}