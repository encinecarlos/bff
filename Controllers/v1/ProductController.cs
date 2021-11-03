using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Products;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/products")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService,
            IResponseService responseService) : base(logger, responseService)
        {
            _productService = productService;
        }

        /// <summary>
        ///     Get Product by identifier
        /// </summary>
        /// <param name="id">Product code</param>
        /// <returns>Product</returns>
        [HttpGet("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetProduct(string id)
        {
            return await SafeExecuteAsync(async () => await _productService.GetProduct(id), HttpMethod.Get);
        }

        /// <summary>
        ///     Find All Product by filters
        /// </summary>
        /// <returns>List of Product</returns>
        [HttpPost("search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindProduct([FromBody] FindProductsRequest query)
        {
            return await SafeExecuteAsync(async () => await _productService.FindProducts(query), HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new Product
        /// </summary>
        /// <param name="command">Json containing the fields to create the Product</param>
        /// <returns>Product created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostProduct([FromBody] AddNewProductRequest command)
        {
            return await SafeExecuteAsync(async () =>
                await _productService.CreateProduct(command), HttpMethod.Post);
        }

        /// <summary>
        ///     Delete Product by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return await SafeExecuteAsync(async () => await _productService.DeleteProduct(id), HttpMethod.Delete);
        }

        /// <summary>
        ///     Update Product by identifier
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <param name="command">Json containing the fields to update the Product</param>
        /// <returns>Product Updated</returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutProduct(string id, UpdateProductRequest command)
        {
            return await SafeExecuteAsync(async () =>
                await _productService.UpdateProduct(id, command), HttpMethod.Put);
        }

        /// <summary>
        ///     Get Product page detail
        /// </summary>
        /// <returns>List with distribution centers and grouped components.</returns>
        [HttpGet("page/detail/configuration")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetProductDetailConfig()
        {
            return await SafeExecuteAsync(async () => await _productService.GetProductDetailConfig(), HttpMethod.Get);
        }
    }
}