using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Systems
{
    public interface ISystemServiceHandler
    {
        Task<Response> FindSystemSetupConfig(Guid quotationId, bool automaticSystem, Guid? promotionId = null);
        Task<BffResponse> SystemInfoValidate(ValidateComponentRequest bindRequest);
        Task<Response> GenerateNewSystem(GenerateNewSystemRequest command);
    }
}