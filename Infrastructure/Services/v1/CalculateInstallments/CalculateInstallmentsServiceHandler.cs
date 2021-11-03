using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using static POC.Bff.Web.Domain.Requests.CalculateInstallmentsRequest;

namespace POC.Bff.Web.Infrastructure.Services.v1.CalculateInstallments
{
    public class CalculateInstallmentsServiceHandler : ICalculateInstallmentsService
    {
        private readonly IConfigurationClient _configurationClient;
        private readonly IExpressClient _expressClient;
        private readonly IResponseService _responseService;

        public CalculateInstallmentsServiceHandler(
            IExpressClient expressClient,
            IConfigurationClient configurationClient,
            IResponseService responseService)
        {
            _expressClient = expressClient;
            _configurationClient = configurationClient;
            _responseService = responseService;
        }

        public async Task<Response> CalculateInstallments(PaymentValueRequest paymentValue)
        {
            var request = new CalculateInstallmentsRequest { PaymentValue = paymentValue.PaymentValue };
            var parameters = await _configurationClient.GetAllParametersSicesExpress();
            var sicesExpressParameters = parameters.Parse<SicesExpressResponse>();

            request.GetMaxInstallments(sicesExpressParameters);

            var installments = (await _expressClient.CalculateInstallments(request)).Parse<List<CalculateInstallmentsResponse>>();

            var key = new Dictionary<int, decimal>();

            installments?.ForEach(x => { key.Add(x.InstallmentNumber, x.InstallmentValue); });

            return _responseService.CreateSuccessResponse(key);
        }
    }
}