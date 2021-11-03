using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.CardsBrand;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/cardsbrand")]
    [ApiController]
    public class CardsBrandController : BaseController
    {
        private readonly ICardsBrandService _cardsBrandService;

        public CardsBrandController(
            ILogger<CardsBrandController> logger,
            ICardsBrandService cardsBrandService,
            IResponseService responseService) : base(logger, responseService)
        {
            _cardsBrandService = cardsBrandService;
        }
        /// <summary>
        ///  Find all card brand
        /// </summary>
        /// <returns>List of Cards Brand</returns>

        [HttpPost("search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindAllCardsBrand()
        {
            return await SafeExecuteAsync(async () => await _cardsBrandService.FindAllCardsBrand(), HttpMethod.Post);
        }

        /// <summary>
        ///  Get card brand by identifier
        /// </summary>
        /// <returns>List of Cards Brand</returns>
        [HttpGet("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetCardsBrandById(Guid id)
        {
            return await SafeExecuteAsync(async () => await _cardsBrandService.GetCardsBrandById(id), HttpMethod.Get);
        }
        /// <summary>
        ///  Add a new cards brand
        /// </summary>
        /// <param name="command">Json containing the fields to add cards brand</param>
        /// <returns>Customer added</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> AddCardsBrand(CardsBrandRequest command)
        {
            return await SafeExecuteAsync(async () => await _cardsBrandService.AddCardsBrand(command), HttpMethod.Post);
        }

        /// <summary>
        ///     Delete brand of cards by identifier
        /// </summary>
        /// <param name="id">CardsBrand identifier</param>
        /// <returns>Status Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await SafeExecuteAsync(async () => await _cardsBrandService.DeleteCardsBrand(id), HttpMethod.Delete);
        }

        /// <summary>
        ///     Update cards brand by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutCardsBrand(Guid id, CardsBrandRequest command)
        {
            return await SafeExecuteAsync(async () => await _cardsBrandService.UpdateCardsBrand(id, command),
                HttpMethod.Put);
        }
    }
}