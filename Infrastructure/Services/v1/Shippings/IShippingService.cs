using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Shippings
{
    public interface IShippingService
    {
        Task<Response> FindShippingStepConfig();
        Task<Response> FindAddressTypes();
        Task<Response> FindShippingTypes();
        Task<Response> FindShippingCompanies();
        Task<Response> GetShippingCompany(string document);
        Task<Response> CreateShippingCompany(ShippingCompanyRequest command);
        Task<Response> DeleteShippingCompanies(string document);
        Task<Response> UpdateShippingCompany(string document, ShippingCompanyRequest command);
    }
}