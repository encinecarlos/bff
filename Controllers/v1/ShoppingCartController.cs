using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Infrastructure.Services.v1.ShoppingCart;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/shopping-cart")]
    [ApiController]
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(
            ILogger<ShoppingCartController> logger,
            IShoppingCartService shoppingCartService,
            IResponseService responseService)
            : base(logger, responseService)
        {
            _shoppingCartService = shoppingCartService;
        }

        /// <summary>
        ///     Price of the sum products
        /// </summary>
        /// <returns>Order by orderId</returns>
        [HttpPost("products/price")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> SumProduct(ProductsPriceDto command)
        {
            return await SafeExecuteAsync(async () => await _shoppingCartService.SumProductsPrice(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Apply Coupon into cart
        /// </summary>
        /// <returns>Coupon details</returns>
        [HttpPost("coupon/apply")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ApplyCouponCart(CouponProductsPriceDto command)
        {
            return await SafeExecuteAsync(async () => await _shoppingCartService.ApplyCouponCart(command), HttpMethod.Post);
        }
    }
}