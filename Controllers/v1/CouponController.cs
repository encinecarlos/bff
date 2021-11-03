using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Coupon;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/coupons")]
    [ApiController]
    public class CouponController : BaseController
    {
        private readonly ICouponService _couponService;

        public CouponController(
            ILogger<CouponController> logger,
            IResponseService responseService,
            ICouponService couponService) : base(logger, responseService)
        {
            _couponService = couponService;
        }

        /// <summary>
        ///     Find All coupon
        /// </summary>
        /// <returns>List of coupon</returns>
        [HttpPost("search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindAllCoupon(FindAllCouponRequest query)
        {
            return await SafeExecuteAsync(async () => await _couponService.FindAllCoupons(query), HttpMethod.Post);
        }

        /// <summary>
        ///  Find coupon by identifier
        /// </summary>
        /// <returns>Coupon detail</returns>
        [HttpGet("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetCoupon(Guid id)
        {
            return await SafeExecuteAsync(async () => await _couponService.GetCoupon(id), HttpMethod.Get);
        }

        /// <summary>
        ///  Find coupon by coupon code
        /// </summary>
        /// <param name="couponCode">Coupon identifier</param>
        /// <returns>Coupon detail</returns>
        [HttpGet("code/{couponCode}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetCouponByCode(string couponCode)
        {
            return await SafeExecuteAsync(async () => await _couponService.GetCouponByCode(couponCode), HttpMethod.Get);
        }

        /// <summary>
        ///  Add a new coupon
        /// </summary>
        /// <param name="command">Json containing the fields to add coupon</param>
        /// <returns>Coupon added</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> AddCoupon(CouponRequest command)
        {
            return await SafeExecuteAsync(async () => await _couponService.AddCoupon(command), HttpMethod.Post);
        }

        /// <summary>
        ///  Delete coupon by identifier
        /// </summary>
        /// <param name="id">Coupon identifier</param>
        /// <returns>Status Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteCoupon(Guid id) =>
            await SafeExecuteAsync(async () => await _couponService.DeleteCoupon(id), HttpMethod.Delete);

        /// <summary>
        ///  Update coupon by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutCoupon(Guid id, CouponRequest command) =>
            await SafeExecuteAsync(async () => await _couponService.UpdateCoupon(id, command), HttpMethod.Put);

        /// <summary>
        ///  Clone coupon by code
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("clone")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CloneCoupon(CloneCouponRequest request) =>
            await SafeExecuteAsync(async () => await _couponService.CloneCoupon(request), HttpMethod.Post);


    }
}