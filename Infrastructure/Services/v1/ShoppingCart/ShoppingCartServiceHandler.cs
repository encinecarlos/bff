using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Configuration.Extensions;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.ShoppingCart
{
    public class ShoppingCartServiceHandler : IShoppingCartService
    {
        private readonly IExpressClient _expressClient;
        private readonly IMapper _mapper;
        private readonly IConfigurationClient _configurationClient;
        private readonly IResponseService _responseService;

        public ShoppingCartServiceHandler(
            IExpressClient expressClient,
            IMapper mapper,
            IConfigurationClient configurationClient,
            IResponseService responseService)
        {
            _expressClient = expressClient;
            _mapper = mapper;
            _configurationClient = configurationClient;
            _responseService = responseService;
        }

        public async Task<Response> SumProductsPrice(ProductsPriceDto command)
        {
            var commandExpress = _mapper.Map<ProductsPriceRequest>(command);
            var response = (await _expressClient.SumProductsPrice(commandExpress))?.Parse<SumShoppingCartResponse>();

            return _responseService.CreateSuccessResponse(new
            {
                response?.Price,
                productList = response?.Products.Select(x => new
                {
                    x.ErpCode,
                    x.Price,
                    x.Quantity
                }),
            });
        }

        public async Task<Response> ApplyCouponCart(CouponProductsPriceDto command)
        {
            var couponCart = _mapper.Map<CouponProductsPriceRequest>(command);
            couponCart.CouponParameter = (await _configurationClient.GetCouponParameters())?.Data.ParseRefitResponseJson<CouponParameterResponse>();
            couponCart.CouponDetail = (await _configurationClient.GetCouponByCode(command.CouponCode))?.Data.ParseRefitResponseJson<CouponResponse>();
            return await _expressClient.ApplyCouponCart(couponCart);
        }
    }
}