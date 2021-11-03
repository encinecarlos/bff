using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Products
{
    public class ProductServiceHandler : IProductService
    {
        private readonly IComponentClient _componentClient;
        private readonly IConfigurationClient _configurationClient;
        private readonly IExpressClient _expressClient;
        private readonly IResponseService _responseService;
        private readonly ICommercialClient _commercialClient;
        private readonly IIdentityClient _identityClient;

        public ProductServiceHandler(
            IExpressClient expressClient,
            IConfigurationClient configurationClient,
            IComponentClient componentClient,
            IResponseService responseService,
            ICommercialClient commercialClient,
            IIdentityClient identityClient)
        {
            _expressClient = expressClient;
            _configurationClient = configurationClient;
            _componentClient = componentClient;
            _responseService = responseService;
            _commercialClient = commercialClient;
            _identityClient = identityClient;
        }

        public async Task<Response> CreateProduct(AddNewProductRequest command)
        {
            if (!string.IsNullOrEmpty(command.ImageDetail?.FileName) &&
                !string.IsNullOrEmpty(command.ImageDetail?.FileBase64))
                command.ImageDetail.KeyName = await AttachmentImage(
                    command.ImageDetail.FileName, command.ImageDetail.FileBase64);

            return await _expressClient.PostProduct(command);
        }

        public async Task<Response> DeleteProduct(string erpCode)
        {
            return await _expressClient.DeleteProduct(erpCode);
        }

        public async Task<Response> FindProducts(FindProductsRequest query)
        {
            await GetOrganizationParameters(query);
            var products = await _expressClient.FindProducts(query);
            var productsResponse = products.Parse<PagedResponse<ProductResponse>>();

            var keyNames = productsResponse.List
                .Where(x => !string.IsNullOrEmpty(x.ImageDetail?.KeyName))
                .Select(x => x.ImageDetail?.KeyName)
                .ToList();

            var findAttachments = new GenerateUrlsAttachmentRequest { KeyNames = keyNames, IsPrivate = true };
            var productsFile = await _configurationClient.FindAttachment(findAttachments);
            var files = productsFile.Parse<List<TermFileResponse>>();

            productsResponse.List.Where(x => !string.IsNullOrEmpty(x.ImageDetail?.KeyName))
                .ToList().ForEach(product =>
                {
                    product.ImageDetail.Url = files?.Find(u => u.KeyName == product.ImageDetail?.KeyName)?.Url;
                });

            return _responseService.CreateSuccessResponse(new
            {
                productsResponse.Total,
                productList = productsResponse.List.Select(x => new
                {
                    x.Id,
                    x.ErpCode,
                    x.ProductStatusType,
                    x.ImageDetail,
                    Model = x.Description,
                    x.Price,
                    x.Power,
                    PowerLabel = $"{x.Power} kWp",
                    x.InStorageQuantity
                })
            });
        }

        public async Task<Response> GetProduct(string id)
        {
            var product = await _expressClient.GetProduct(id);
            var productResponse = product.Parse<ProductResponse>();

            if (string.IsNullOrEmpty(productResponse?.ImageDetail?.KeyName))
                return _responseService.CreateSuccessResponse(new
                {
                    productResponse.Id,
                    productResponse.ErpCode,
                    productResponse?.ImageDetail,
                    Model = productResponse.Description,
                    productResponse.Price,
                    PowerLabel = $"{productResponse.Power} kWp",
                    productResponse.Power,
                    DistributionCenterList = productResponse.DistributionCenters,
                    productResponse.ProductStatusType,
                    productResponse.ProductType,
                    productResponse.Position,
                    productResponse.InStorageQuantity,
                    ComponentGroupList = productResponse.Components.OrderBy(x => x.Id)
                });
            var keyName = new List<string> { productResponse.ImageDetail.KeyName };
            var findAttachments = new GenerateUrlsAttachmentRequest { KeyNames = keyName, IsPrivate = true };
            var productFile = await _configurationClient.FindAttachment(findAttachments);
            var files = productFile.Parse<List<TermFileResponse>>();

            productResponse.ImageDetail.Url = files.FirstOrDefault()?.Url;

            return _responseService.CreateSuccessResponse(new
            {
                productResponse.Id,
                productResponse.ErpCode,
                productResponse?.ImageDetail,
                Model = productResponse.Description,
                productResponse.Price,
                PowerLabel = $"{productResponse.Power} kWp",
                productResponse.Power,
                DistributionCenterList = productResponse.DistributionCenters,
                productResponse.ProductStatusType,
                productResponse.ProductType,
                productResponse.Position,
                productResponse.InStorageQuantity,
                ComponentGroupList = productResponse.Components.OrderBy(x => x.Id)
            });
        }

        public async Task<Response> UpdateProduct(string id, UpdateProductRequest command)
        {
            if (!string.IsNullOrEmpty(command.ImageDetail?.FileName) &&
                !string.IsNullOrEmpty(command.ImageDetail?.FileBase64))
                command.ImageDetail.KeyName = await AttachmentImage(
                    command.ImageDetail.FileName, command.ImageDetail.FileBase64);

            return await _expressClient.PutProduct(id, command);
        }

        public async Task<Response> GetProductDetailConfig()
        {
            var distributionCentersResult = _componentClient.FindDistributionCenters();
            var componentsResult = _componentClient.FindComponents();

            await Task.WhenAll(distributionCentersResult, componentsResult);

            var distributionCenters = distributionCentersResult.Result
                .Parse<List<DistributionCenterDto>>().Select(x => new { x.Id, Name = x.Description });

            var groupedComponentsDto = componentsResult.Result.Parse<List<GroupComponentsDto>>();

            return _responseService.CreateSuccessResponse(new
            {
                DistributionCenterList = distributionCenters,
                ComponentGroupList = GroupComponentsDto.Create(groupedComponentsDto)
            });
        }

        public async Task<string> AttachmentImage(string fileName, string fileBase64)
        {
            var attachmentDto = new AttachmentRequest
            {
                IsPrivate = true,
                Content = fileBase64,
                FileName = fileName
            };

            var uploadAttachment = await _configurationClient.CreateAttachment(attachmentDto);
            var uploadAttachmentResponse = uploadAttachment.Parse<AttachmentResponse>();

            return uploadAttachmentResponse.KeyName;
        }

        private async Task GetOrganizationParameters(FindProductsRequest query)
        {
            var organization = await _commercialClient.GetLoggedUserOrganization();
            query.SicesExpressDistributionCenterId = organization.Parse<OrganizationResponse>().SicesExpressDistributionCenterId;
        }
    }
}