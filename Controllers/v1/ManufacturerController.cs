using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Manufacturers;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/manufacturers")]
    public class ManufacturerController : BaseController
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(
            ILogger<ManufacturerController> logger,
            IResponseService responseService,
            IManufacturerService manufacturerService) : base(logger, responseService)
        {
            _manufacturerService = manufacturerService;
        }

        /// <summary>
        ///     Get Manufacturer by identifier
        /// </summary>
        /// <param name="id">Manufacturer Identifier</param>
        /// <returns> Manufacturer </returns>
        [HttpGet("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetManufacturer(Guid id)
        {
            return await SafeExecuteAsync(async () => await _manufacturerService.GetManufacturer(id), HttpMethod.Get);
        }

        /// <summary>
        ///     Find All Manufacturers or find all Manufacturers by Components Type filter
        /// </summary>
        /// <returns>List of components Manufacturer</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindManufacturer([FromQuery] FindManufacturersRequest query)
        {
            return await SafeExecuteAsync(async () => await _manufacturerService.FindManufacturers(query),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new Manufacturer
        /// </summary>
        /// <param name="command">Json containing the fields to create the manufacturer</param>
        /// <returns>Manufacturer Created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostManufacturer(ManufacturerRequest command)
        {
            return await SafeExecuteAsync(async () => await _manufacturerService.CreateManufacturer(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Delete Manufacturer by identifier
        /// </summary>
        /// <param name="id">Manufacturer Identifier</param>
        /// <returns>Status Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteManufacturer(Guid id)
        {
            return await SafeExecuteAsync(async () => await _manufacturerService.DeleteManufacturer(id),
                HttpMethod.Delete);
        }

        /// <summary>
        ///     Update Manufacturer by identifier
        /// </summary>
        /// <param name="id">Manufacturer Identifier</param>
        /// <param name="command">Json containing the fields to create the manufacturer</param>
        /// <returns>Manufacturer Updated</returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutManufacturer(Guid id, ManufacturerRequest command)
        {
            return await SafeExecuteAsync(async () => await _manufacturerService.UpdateManufacturer(id, command),
                HttpMethod.Put);
        }

        /// <summary>
        ///     Find All Manufacturers group by built component
        /// </summary>
        /// <returns>List of components Manufacturer</returns>
        [HttpGet("group")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindGroupedBuiltComponents()
        {
            return await SafeExecuteAsync(async () => await _manufacturerService.FindGroupedBuiltComponents(),
                HttpMethod.Get);
        }
    }
}