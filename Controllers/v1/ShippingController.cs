using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Configurations;
using POC.Bff.Web.Infrastructure.Services.v1.Shippings;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/shippings")]
    public class ShippingController : BaseController
    {
        private readonly IConfigurationService _configurationService;
        private readonly IShippingService _shippingService;

        public ShippingController(
            ILogger<ShippingController> logger,
            IResponseService responseService,
            IConfigurationService configurationService,
            IShippingService shippingService) : base(logger, responseService)
        {
            _configurationService = configurationService;
            _shippingService = shippingService;
        }

        [HttpGet("setup/configuration")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindShippingStepConfig()
        {
            return await SafeExecuteAsync(async () => await _shippingService.FindShippingStepConfig(), HttpMethod.Get);
        }


        /// <summary>
        ///     Find All shipping address types
        /// </summary>
        /// <returns>List of shipping address types</returns>
        [HttpGet("address-types")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindShippingAddressType()
        {
            return await SafeExecuteAsync(async () => await _shippingService.FindAddressTypes(), HttpMethod.Get);
        }

        /// <summary>
        ///     Find All shipping types
        /// </summary>
        /// <returns>List of shipping types</returns>
        [HttpGet("types")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindShippingTypes()
        {
            return await SafeExecuteAsync(async () => await _shippingService.FindShippingTypes(), HttpMethod.Get);
        }

        /// <summary>
        ///     Find All companies
        /// </summary>
        /// <returns>List of companies</returns>
        [HttpGet("companies")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> FindShippingCompanies()
        {
            return await SafeExecuteAsync(async () => await _shippingService.FindShippingCompanies(), HttpMethod.Get);
        }

        /// <summary>
        ///     Get company by document
        /// </summary>
        /// <returns>ShippingDetail Company</returns>
        [HttpGet("companies/{document}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetShippingCompanyByDocument(string document)
        {
            return await SafeExecuteAsync(async () => await _shippingService.GetShippingCompany(document),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Create shipping company
        /// </summary>
        /// <returns>Company created</returns>
        [HttpPost("companies")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> AddNewShippingCompany(ShippingCompanyRequest command)
        {
            return await SafeExecuteAsync(async () => await _shippingService.CreateShippingCompany(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Delete Company from current organization user
        /// </summary>
        /// <returns></returns>
        [HttpDelete("companies/{document}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteShippingCompany(string document)
        {
            return await SafeExecuteAsync(async () => await _shippingService.DeleteShippingCompanies(document),
                HttpMethod.Delete);
        }


        /// <summary>
        ///     Change shipping company
        /// </summary>
        /// <returns>Company updated</returns>
        [HttpPut("companies/{document}")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> UpdateShippingCompany(string document, ShippingCompanyRequest command)
        {
            return await SafeExecuteAsync(async () => await _shippingService.UpdateShippingCompany(document, command),
                HttpMethod.Put);
        }


        /// <summary>
        ///     Creates a new region for the informed state and links a shipping rule with the shipping configuration
        /// </summary>
        /// <param name="distributionCenterId">Identifier for distribution center shipping</param>
        /// <param name="request">Additional info</param>
        /// <returns></returns>
        [HttpPatch("distribution-center/{distributionCenterId}")]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> BindRuleForRegionInDistributionCenter(Guid distributionCenterId,
            AddNewRegionAndLinkToShippingRuleRequest request)
        {
            return await SafeExecuteAsync(
                async () => await _configurationService.BindRuleForRegionInDistributionCenter(distributionCenterId,
                    request), HttpMethod.Patch);
        }

        /// <summary>
        ///     Find All regions in states and rules by distribution center identifier
        /// </summary>
        /// <param name="distributionCenterId">Identifier for distribution center</param>
        /// <returns>List of regions from the Identifier shipping configuration</returns>
        [HttpGet("distribution-center/{distributionCenterId}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindRegionsByDistributionCenter(Guid distributionCenterId)
        {
            return await SafeExecuteAsync(
                async () => await _configurationService.FindRegionsByDistributionCenter(distributionCenterId),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Delete a specific region from distribution center shipping configuration
        /// </summary>
        /// <param name="distributionCenterId">Identifier for distribution center</param>
        /// <param name="regionId">Region identifier</param>
        /// <returns></returns>
        [HttpDelete("distribution-center/{distributionCenterId}/region/{regionId}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> DeleteRegionRuleInShippingRule(Guid distributionCenterId, Guid regionId)
        {
            return await SafeExecuteAsync(
                async () => await _configurationService.DeleteRegionRuleInShippingRule(distributionCenterId, regionId),
                HttpMethod.Delete);
        }


    }
}