using POC.Shared.Responses;
using System.Threading.Tasks;
using static POC.Bff.Web.Domain.Requests.CalculateInstallmentsRequest;

namespace POC.Bff.Web.Infrastructure.Services.v1.CalculateInstallments
{
    public interface ICalculateInstallmentsService
    {
        Task<Response> CalculateInstallments(PaymentValueRequest paymentValue);
    }
}