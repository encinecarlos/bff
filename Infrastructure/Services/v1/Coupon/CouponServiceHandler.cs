using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Configuration.Extensions;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Coupon
{
    public class CouponServiceHandler : ICouponService
    {
        private readonly IConfigurationClient _configurationClient;
        private readonly IResponseService _responseService;
        private readonly ICommercialClient _commercialClient;

        public CouponServiceHandler(
            IConfigurationClient configurationClient,
            IResponseService responseService,
            ICommercialClient commercialClient)
        {
            _configurationClient = configurationClient;
            _responseService = responseService;
            _commercialClient = commercialClient;
        }

        public async Task<Response> AddCoupon(CouponRequest command)
        {
            if (command.OrganizationId.HasValue && command.OrganizationId.Value != Guid.Empty)
                await ValidateOrganization(command);

            return await _configurationClient.AddNewCoupon(command);
        }

        private async Task ValidateOrganization(CouponRequest command)
        {
            var organization = await _commercialClient.GetOrganization(command.OrganizationId.Value);
            var organizationResponse = organization.Data.ParseRefitResponseJson<OrganizationResponse>();

            if (organizationResponse != null)
            {
                command.OrganizationDetail = new CouponOrganizationDetail
                {
                    Id = organizationResponse.Id,
                    TradeName = organizationResponse.TradeName
                };
            }
            else
                command.OrganizationIsValid = false;
        }

        public async Task<Response> DeleteCoupon(Guid id)
        {
            return await _configurationClient.DeleteCoupon(id);
        }

        public async Task<Response> FindAllCoupons(FindAllCouponRequest query)
        {
            var responseList = (await _configurationClient.FindAllCoupons(query)).Parse<FindCouponResponse>();

            foreach (var item in responseList.List.Where(x => x.OrganizationDetail != null && x.OrganizationDetail?.Id != Guid.Empty))
            {
                await CreateOrganizationDetail(item);
            }
            var list = CreateFindAllCouponResponse(responseList);

            return _responseService.CreateSuccessResponse(new
            {
                responseList?.Total,
                list
            });
        }

        private async Task CreateOrganizationDetail(CouponResponse response)
        {
            var organization = await _commercialClient.GetOrganization(response.OrganizationDetail.Id);
            var organizationResponse = organization.Data.ParseRefitResponseJson<OrganizationResponse>();
            response.OrganizationDetail.Document = new Shared.Domain.ValueObjects.Document
            {
                Number = organizationResponse?.Document.Number,
                Type = organizationResponse?.Document.Type
            };
        }

        private List<object> CreateFindAllCouponResponse(FindCouponResponse responseList)
        {
            var response = new List<object>();
            foreach(var item in responseList.List)
            {
                response.Add( new
                {
                    item.Id,
                    item.Name,
                    Code = item.CouponCode,
                    item.OrganizationDetail,
                    ContextList = item.Contexts,
                    item.Status,
                    item.DiscountValue,
                    item.ExpirationAt
                });
            }

            return response;
        }

        public async Task<Response> GetCoupon(Guid id)
        {
            var response = (await _configurationClient.GetCouponById(id)).Parse<CouponResponse>();
            return CreateCouponResponse(response);
        }

        private Response CreateCouponResponse(CouponResponse response)
        {
            return _responseService.CreateSuccessResponse(new
            {
                response?.Id,
                Code = response?.CouponCode,
                response?.Name,
                response?.Status,
                response?.ExpirationAt,
                response?.ExpirationEnabled,
                OrganizationId = response?.OrganizationDetail.Id,
                ContextList = response?.Contexts,
                response?.DiscountValue
            });
        }

        public async Task<Response> GetCouponByCode(string couponCode)
        {
            var response = (await _configurationClient.GetCouponByCode(couponCode))?.Parse<CouponResponse>();
            return CreateCouponResponse(response);
        }

        public async Task<Response> UpdateCoupon(Guid id, CouponRequest command)
        {
            return await _configurationClient.UpdateCoupon(id, command);
        }

        public async Task<Response> CloneCoupon(CloneCouponRequest request)
        {
            return await _configurationClient.CloneCoupon(request);
        }
    }
}
