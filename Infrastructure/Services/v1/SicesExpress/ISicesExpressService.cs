using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.SicesExpress
{
    public interface ISicesExpressService
    {
        Task<Response> GetAllParametersSicesExpress();
        Task<Response> UpdateSicesExpressParameter(Guid id, UpdateSicesExpressParameterRequest command);
    }
}