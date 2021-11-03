using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Order;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(
            ILogger<OrderController> logger,
            IOrderService orderService,
            IResponseService responseService) : base(logger, responseService)
        {
            _orderService = orderService;
        }

        /// <summary>
        ///     Find All Orders by filters
        /// </summary>
        /// <returns>List of Orders</returns>
        [HttpPost("search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindOrder([FromBody] FindOrdersRequest query)
        {
            return await SafeExecuteAsync(async () => await _orderService.FindOrders(query), HttpMethod.Post);
        }

        /// <summary>
        ///     Find order by orderId
        /// </summary>
        /// <returns>List of Orders</returns>
        [HttpGet("{orderId}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            return await SafeExecuteAsync(async () => await _orderService.GetOrder(orderId), HttpMethod.Get);
        }

        /// <summary>
        ///     Find order by orderNumber
        /// </summary>
        /// <returns>Order</returns>
        [HttpGet("orderNumber/{orderNumber}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetOrderByOrderNumber(string orderNumber)
        {
            return await SafeExecuteAsync(async () => await _orderService.GetOrder(orderNumber), HttpMethod.Get);
        }

        /// <summary>
        ///     Add a new order on sices express
        /// </summary>
        /// <param name="command">Json containing the fields to add cards brand</param>
        /// <returns>Customer added</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostOrder(AddNewOrderRequest command) =>
            await SafeExecuteAsync(async () => await _orderService.AddNewOrder(command), HttpMethod.Post);

        /// <summary>
        ///     Update to cancel order by orderId
        /// </summary>
        /// <param name="orderId">Order identifier</param>
        /// <returns>Order Cancelled</returns>
        [HttpPut("{orderId}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        {
            return await SafeExecuteAsync(async () =>
                await _orderService.CancelOrder(orderId), HttpMethod.Put);
        }
    }
}