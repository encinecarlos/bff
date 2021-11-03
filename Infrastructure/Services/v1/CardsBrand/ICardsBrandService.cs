using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.CardsBrand
{
    public interface ICardsBrandService
    {
        Task<Response> AddCardsBrand(CardsBrandRequest command);
        Task<Response> UpdateCardsBrand(Guid id, CardsBrandRequest command);
        Task<Response> FindAllCardsBrand();
        Task<Response> DeleteCardsBrand(Guid id);
        Task<Response> GetCardsBrandById(Guid id);
    }
}