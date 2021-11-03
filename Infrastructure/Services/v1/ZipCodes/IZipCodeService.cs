using POC.Shared.Responses;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.ZipCodes
{
    public interface IZipCodeService
    {
        Task<Response> GetAddress(string zipcode, string countryCode);
        Task<Response> GetAddress(string zipcode);
    }
}