using POC.Bff.Web.Domain.Dtos;
using POC.Shared.Responses;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.ShoppingCart
{
    public interface IShoppingCartService
    {
        Task<Response> SumProductsPrice(ProductsPriceDto command);
        Task<Response> ApplyCouponCart(CouponProductsPriceDto command);
    }
}