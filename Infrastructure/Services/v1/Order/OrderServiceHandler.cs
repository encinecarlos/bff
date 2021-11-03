using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Order
{
    public class OrderServiceHandler : IOrderService
    {

        private readonly IExpressClient _expressClient;
        private readonly ICommercialClient _commercialClient;
        private readonly IIdentityClient _identityClient;
        private readonly IConfigurationClient _configurationClient;
        private readonly IResponseService _responseService;
        private readonly IMapper _mapper;

        public OrderServiceHandler(
            IExpressClient expressClient,
            ICommercialClient commercialClient,
            IIdentityClient identityClient,
            IConfigurationClient configurationClient,
            IResponseService responseService,
            IMapper mapper)
        {
            _expressClient = expressClient;
            _commercialClient = commercialClient;
            _identityClient = identityClient;
            _configurationClient = configurationClient;
            _responseService = responseService;
            _mapper = mapper;
        }

        public async Task<Response> GetOrder(Guid orderId)
        {
            var response = (await _expressClient.GetOrder(orderId)).Parse<OrderResponse>();

            var keyNames = response?.Products
                .Where(x => !string.IsNullOrEmpty(x.ImageDetail?.KeyName))
                .Select(x => x.ImageDetail?.KeyName)
                .ToList();

            var findAttachments = new GenerateUrlsAttachmentRequest { KeyNames = keyNames, IsPrivate = true };
            var productsFile = await _configurationClient.FindAttachment(findAttachments);
            var files = productsFile.Parse<List<TermFileResponse>>();

            response?.Products.Where(x => !string.IsNullOrEmpty(x.ImageDetail?.KeyName))
                .ToList().ForEach(product =>
                {
                    product.ImageDetail.Url = files?.Find(u => u.KeyName == product.ImageDetail?.KeyName)?.Url;
                });

            return _responseService.CreateSuccessResponse(ResponseOrderById(response));
        }

        public async Task<Response> GetOrder(string orderNumber)
        {
            var response = (await _expressClient.GetOrder(orderNumber))?.Parse<OrderResponse>();
            return _responseService.CreateSuccessResponse(ResponseOrderById(response));
        }

        public async Task<Response> FindOrders(FindOrdersRequest query)
        {
            var responseList = (await _expressClient.FindOrders(query))?.Parse<FindOrderResponse>();
            var orderList = CreateResponseFindOrders(responseList);

            return _responseService.CreateSuccessResponse(new
            {
                responseList?.Total,
                OrderList = orderList
            });
        }

        public async Task<Response> AddNewOrder(AddNewOrderRequest command)
        {
            command.SicesExpressParameter = (await _configurationClient.GetAllParametersSicesExpress())?.Parse<SicesExpressResponse>();
            if (!string.IsNullOrEmpty(command.CouponCode))
            {
                command.CouponParameter = (await _configurationClient.GetCouponParameters())?.Parse<CouponParameterResponse>();
                command.CouponDetail = (await _configurationClient.GetCouponByCode(command.CouponCode))?.Parse<CouponResponse>();
                var couponRequest = _mapper.Map<CouponRequest>(command.CouponDetail);
                await _configurationClient.UpdateCoupon(command.CouponDetail.Id, couponRequest);
            }
            await GetOrganizationParameters(command);
            var response = (await _expressClient.AddNewOrder(command))?.Parse<OrderResponse>();
            return _responseService.CreateSuccessResponse(response);
        }

        public async Task<Response> CancelOrder(Guid orderId)
        {
            var command = new UpdateOrderRequest();
            await GetOrganizationParameters(command);
            return await _expressClient.CancelOrder(orderId, command);
        }

        private async Task GetOrganizationParameters(OrderRequest command)
        {
            var accountManager = (await _commercialClient.GetLoggedUserOrganization())?.Parse<AccountManagerResponse>();
            var authorization = await _identityClient.GetAccount(accountManager?.AccountManagerUserId ?? Guid.Empty);
            command.AccountOrder = authorization.Parse<AccountResponse>();
            var organization = await _commercialClient.GetLoggedUserOrganization();
            command.OrganizationParameters = organization.Parse<OrganizationResponse>();
        }

        [Obsolete("Changed to specific type and use automapper")]
        private object ResponseOrderById(OrderResponse response)
        {
            response.CreditCards.ForEach(
                async cc => cc.BrandDetail =
                    (await _configurationClient.GetCardsBrandById(cc.BrandId))?.Parse<BrandDto>());

            return new
            {
                response.OrderNumber,
                response.Amount,
                productList = response.Products.Select(x => new
                {
                    x.ErpCode,
                    ImageDetail = new { x.ImageDetail.Url },
                    Model = x.Description,
                    x.Price,
                    x.Quantity,
                    PowerLabel = $"{x.Power} kWp"
                }),
                response.CouponDetail,
                statusDetail = new
                {
                    accountId = response.AccountOrder.Id,
                    status = response.StatusDetail.Status,
                    updatedAt = response.UpdatedAt,
                    response.CreatedBy.NameInitials
                },
                shippingDetail = new { response.ShippingAddress },
                invoicingDetail = new
                {
                    response.InvoicingDetail.Name,
                    response.InvoicingDetail.ContactPerson,
                    Document = new
                    {
                        response.InvoicingDetail.Document.Type,
                        response.InvoicingDetail.Document.Number
                    },
                    personType = response.InvoicingDetail.PersonType,
                    response.InvoicingDetail.Email,
                    response.InvoicingDetail.StateInscription,
                    response.InvoicingDetail.Phone,
                    Address = new
                    {
                        response.InvoicingDetail.Address.Street,
                        response.InvoicingDetail.Address.District,
                        response.InvoicingDetail.Address.City,
                        response.InvoicingDetail.Address.Number,
                        response.InvoicingDetail.Address.StateCode,
                        response.InvoicingDetail.Address.ZipCode,
                        response.InvoicingDetail.Address.CountryCode
                    }
                },
                paymentDetail = new
                {
                    paymentMethod = response.PaymentDetail.PaymentMethod,
                    boletoUrl = response.PaymentDetail.BoletoUrl,
                    expirationAt = response.PaymentDetail?.ExpirationAt,
                    creditCardList = response.CreditCards
                },
                response.UpdatedAt,
                response.CreatedAt,
                response.IvaTaxValue
            };
        }

        [Obsolete("Changed to specific type and use automapper")]
        private static IEnumerable<object> CreateResponseFindOrders(FindOrderResponse responseList)
        {
            return responseList.List.Select(item => new
            {
                item.Id,
                item.OrderNumber,
                item.Price,
                item.CreatedBy,
                item.OrganizationDetail,
                ProductList = item.Products,
                item.Status,
                item.PaymentDetail,
                item.UpdatedAt,
                item.CreatedAt
            });
        }

    }
}