using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Promotions
{
    public interface IPromotionService
    {
        Task<Response> GetDetailConfig();
        Task<Response> AddNewPromotion(PromotionRequest request);
        Task<Response> FindPromotionList(FindPromotionsRequest request);
        Task<Response> UpdatePromotion(Guid id, PromotionRequest request);
        Task<Response> GetPromotion(Guid id);
        Task<Response> GetAvailablePromotion(Guid id);
        Task<Response> ClonePromotion(Guid id);
    }
}