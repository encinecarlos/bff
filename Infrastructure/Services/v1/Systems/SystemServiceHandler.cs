using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Domain.Fixed;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Systems
{
    public class SystemServiceHandler : ISystemServiceHandler
    {
        private readonly ICommercialClient _commercialClient;
        private readonly IComponentClient _componentClient;
        private readonly IConfigurationClient _configurationClient;
        private readonly IResponseService _responseService;

        public SystemServiceHandler(
            ICommercialClient commercialClient,
            IConfigurationClient configurationClient,
            IComponentClient componentClient,
            IResponseService responseService)
        {
            _commercialClient = commercialClient;
            _configurationClient = configurationClient;
            _componentClient = componentClient;
            _responseService = responseService;
        }

        public async Task<Response> FindSystemSetupConfig(Guid quotationId, bool automaticSystem, Guid? promotionId = null)
        {
            var promotionIncluded = promotionId.HasValue && !promotionId.Equals(Guid.Empty);

            //Validar se promoção é valida
            if (promotionIncluded) await _commercialClient.CheckPromotionActive(promotionId.Value);

            //Pegar organizationDetail do quotation.
            var organizationResponse = await _commercialClient.GetOrganizationDetailInQuotation(quotationId);
            var quotationOrganizationDetailDto =
                organizationResponse.Parse<QuotationOrganizationDetailResponse>();

            var systemRequest = new SystemComponentRequest
            {
                DistributionCenterId = quotationOrganizationDetailDto.DistributionCenterId ?? Guid.Empty,
                TierId = quotationOrganizationDetailDto.Tier.Id,
                PromotionId = promotionId
            };

            //Pegar erpCodes da promoção (commercial), senão pegar erpCodes pelo Pricing ou Generator tiers (component).
            var erpCodesResponse = promotionIncluded
                ? await _commercialClient.FindErpCodesOnPromotion(promotionId.Value, systemRequest.DistributionCenterId, systemRequest.TierId)
                : await _componentClient.FindErpCodeComponents(systemRequest.DistributionCenterId, systemRequest.TierId,
                    automaticSystem);

            //Lista importante p/ pega fabricantes e components. (aqui já contem erpCodes por tiers ou pela promotion) 
            systemRequest.ErpCodes = erpCodesResponse.Parse<List<string>>();

            //Pegar lista de components.
            var groupedComponentsByType = await _commercialClient.FindGroupedComponentsByTypeAsync(systemRequest);
            var componentGroupList =
                groupedComponentsByType.Parse<List<ComponentGroupListResponse>>();

            //Pegar keys dos components.
            systemRequest.ErpCodes = componentGroupList.SelectMany(x => x.ComponentOptionList).Select(x => x.ErpCode)
                .ToList();
            var keysList = _componentClient.FindKeysByErpCode(systemRequest);

            //Pegar lista de manufacturers de components existentes.
            var inverterManufacturerList =
                _componentClient.FindManufacturerUsedOnComponents((int)ComponentType.Inverter, systemRequest);
            var stringBoxManufacturerList =
                _componentClient.FindManufacturerUsedOnComponents((int)ComponentType.StringBox, systemRequest);
            var structureManufacturerList =
                _componentClient.FindManufacturerUsedOnComponents((int)ComponentType.Structure, systemRequest);

            //Pegar itens de configuração.
            var structureVarietyList = _configurationClient.FindStructureVarietiesByDistributionCenterAndTier(
                systemRequest.DistributionCenterId, systemRequest.TierId, automaticSystem);
            var voltageList = _configurationClient.FindVoltages();
            var phaseList = _configurationClient.FindPhases();

            await Task.WhenAll(
                inverterManufacturerList,
                stringBoxManufacturerList,
                structureManufacturerList,
                voltageList,
                phaseList,
                structureVarietyList,
                keysList);

            return _responseService.CreateSuccessResponse(new
            {
                GeneratePVSystemFormOptions = new
                {
                    InverterManufacturerList = inverterManufacturerList.Result.Data,
                    StringBoxManufacturerList = stringBoxManufacturerList.Result.Data,
                    StructureManufacturerList = structureManufacturerList.Result.Data,
                    StructureVarietyList = structureVarietyList.Result.Data,
                    VoltageList = voltageList.Result.Data,
                    PhaseList = phaseList.Result.Data
                },
                ComponentGroupList = PopulateKeysInComponentGroupList(keysList, componentGroupList)
            });
        }

        public async Task<BffResponse> SystemInfoValidate(ValidateComponentRequest request)
        {
            var organizationResponse = _commercialClient.GetOrganizationDetailInQuotation(request.QuotationId);
            var fdiResponse = _configurationClient.FindFdis();

            await Task.WhenAll(organizationResponse, fdiResponse);

            if (request.VoltageId.HasValue)
            {
                var voltage = await _configurationClient.GetVoltage(request.VoltageId.Value);
                request.Voltage = voltage.Parse<VoltageRequest>();
            }

            var quotationOrganizationDetailDto = organizationResponse.Result.Parse<QuotationOrganizationDetailResponse>();

            var systemRequest = new SystemComponentRequest
            {
                DistributionCenterId = quotationOrganizationDetailDto?.DistributionCenterId ?? default,
                TierId = quotationOrganizationDetailDto?.Tier?.Id ?? default,
                PromotionId = request.PromotionId
            };

            request.Fdi = fdiResponse.Result.Parse<FdiRequest>();

            var componentValidateSystemPower = _componentClient.ValidateSystemPower(request);
            var componentList = _commercialClient.FindGroupedComponentsByTypeAsync(systemRequest);

            await Task.WhenAll(componentValidateSystemPower, componentList);

            var componentValidate = componentValidateSystemPower.Result.Parse<ComponentValidateResponse>();
            var componentListResponse = componentList.Result.Parse<List<ComponentListResponse>>();

            systemRequest.ErpCodes = componentListResponse
                .SelectMany(x => x.ComponentOptionList)
                .Select(x => x.ErpCode);

            var keysList = await _componentClient.FindKeysByErpCode(systemRequest);

            var compoValidate = PopulateComponentValidate(request, componentValidate, systemRequest);

            var systemValidate = await _commercialClient.ValidateSystem(compoValidate);
            var systemValidateResponse = systemValidate.Parse<ValidateSystemResponse>();

            return new BffResponse
            {
                Data = new
                {
                    componentValidate.Power,
                    componentValidate.PowerLabel,
                    systemValidateResponse.SystemPrice,
                    systemValidateResponse.PvSystemMarkup,
                    systemValidateResponse.PvSystemCMV,
                    systemValidateResponse.PvSystemTax,
                    ComponentList = PopulateComponentList(componentListResponse, componentValidate, keysList.Parse<List<ComponentKeyResponse>>()),
                    IsValid = VerifySystemIsCheckValid(systemValidate, componentValidate)
                },
                Notifications = systemValidate.Notifications.Select(x => new { componentGroupId = x.FieldName, x.Type, x.Notification }),
                Success = !systemValidate.Notifications.Any()
            };
        }

        public async Task<Response> GenerateNewSystem(GenerateNewSystemRequest command)
        {
            return await _commercialClient.GenerateNewSystem(command);
        }

        private static bool VerifySystemIsCheckValid(Response systemValidateResponse, ComponentValidateResponse componentValidate)
        {
            return !systemValidateResponse.Notifications.Any() || systemValidateResponse.Notifications?.Count == 1 &&
                   (componentValidate.IsMaxFdi || componentValidate.IsMinFdi);
        }

        private static List<ComponentGroupListResponse> PopulateKeysInComponentGroupList(Task<Response> keysList, List<ComponentGroupListResponse> componentGroupList)
        {
            var keys = keysList.Result.Parse<List<ComponentKeyResponse>>();

            componentGroupList = componentGroupList.Select(g =>
            {
                g.ComponentOptionList = g.ComponentOptionList.Join(keys, c => c.ErpCode, k => k.ErpCode, (c, k) =>
                {
                    c.ImageKeyName = k.ImageKeyName;
                    c.KeyName = k.KeyName;
                    return c;
                }).ToList();
                return g;
            }).ToList();
            return componentGroupList;
        }

        private static ComponentValidateResponse PopulateComponentValidate(ValidateComponentRequest request, ComponentValidateResponse componentValidate, SystemComponentRequest systemComponentsDto)
        {
            request.ComponentList.ForEach(y =>
            {
                y.Power = componentValidate.Components.Where(z => z.ErpCode == y.ErpCode).Select(p => p.Power)
                    .FirstOrDefault();
            });

            componentValidate.Components = request.ComponentList;
            componentValidate.DistributionCenterId = systemComponentsDto.DistributionCenterId;
            componentValidate.TierId = systemComponentsDto.TierId;
            componentValidate.PromotionId = request.PromotionId;

            return componentValidate;
        }

        private static IEnumerable<ComponentListResponse> PopulateComponentList(IEnumerable<ComponentListResponse> componentListResponse, ComponentValidateResponse componentValidate, List<ComponentKeyResponse> keys)
        {

            return componentListResponse.Select(comp =>
            {
                comp.ComponentOptionList = comp.ComponentOptionList.Join(keys, c => c.ErpCode, k => k.ErpCode, (c, k) =>
                {
                    c.ImageKeyName = k.ImageKeyName;
                    c.KeyName = k.KeyName;
                    c.Price = GetPriceValue(componentValidate, c) ?? 0M;
                    c.Prices = null;

                    return c;
                }).ToList();

                return comp;
            });
        }

        private static decimal? GetPriceValue(ComponentValidateResponse componentValidate, ComponentListResponse.ComponentOption component)
        {
            if (component.Prices == null) return 0M;

            var priceValue = component.Prices.FirstOrDefault(p =>
                p.Power?.Min < componentValidate.Power && p.Power?.Max >= componentValidate.Power)?.Value;

            return ((priceValue ?? 0) == 0 ? GetExtremesPriceValue(componentValidate, component) : priceValue) ?? 0M;
        }

        private static decimal? GetExtremesPriceValue(ComponentValidateResponse componentValidate, ComponentListResponse.ComponentOption componentOption)
        {
            var maxPowerValue = componentOption.Prices.Max(x => x.Power.Max);
            return componentValidate.Power == 0
                ? componentOption.Prices.FirstOrDefault(y => y.Power?.Min == 0)?.Value
                : componentOption.Prices.FirstOrDefault(y => y.Power?.Max == maxPowerValue)?.Value;
        }
    }
}