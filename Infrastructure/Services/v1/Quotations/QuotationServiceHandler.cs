using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Fixed;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Bff.Web.Infrastructure.Services.v1.Configurations;
using POC.Shared.Configuration.Extensions;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Quotations
{
    public class QuotationServiceHandler : IQuotationService
    {
        private readonly ICommercialClient _commercialClient;
        private readonly IComponentClient _componentClient;
        private readonly IConfigurationClient _configurationClient;
        private readonly IConfigurationService _configurationService;
        private readonly IIdentityClient _identityClient;
        private readonly IMapper _mapper;
        private readonly IResponseService _responseService;

        public QuotationServiceHandler(
            ICommercialClient commercialClient,
            IConfigurationClient configurationClient,
            IComponentClient componentClient,
            IIdentityClient identityClient,
            IConfigurationService configurationService,
            IMapper mapper,
            IResponseService responseService)
        {
            _commercialClient = commercialClient;
            _configurationClient = configurationClient;
            _componentClient = componentClient;
            _identityClient = identityClient;
            _configurationService = configurationService;
            _mapper = mapper;
            _responseService = responseService;
        }

        public async Task<Response> FindQuotationDetailPageConfig()
        {
            var states = _configurationClient.FindStatesCode();
            var accounts = _configurationClient.FindBankAccounts();
            var canCancelStatusList = _commercialClient.FindCancellationQuotationStatus();
            var canDeleteStatusList = _commercialClient.FindDeletedQuotationStatus();
            var tags = _configurationClient.FindTags();

            await Task.WhenAll(states, accounts, canDeleteStatusList, canCancelStatusList, tags);

            return _responseService.CreateSuccessResponse(new
            {
                stateList = states.Result.Data,
                bankAccountDetail = accounts.Result.Data,
                canCancelStatusList = canCancelStatusList.Result.Data,
                canDeleteStatusList = canDeleteStatusList.Result.Data,
                shippingRulesUrl = "https://sicessolar.com.br/wp-content/uploads/2018/05/condicoes-e-prazos.pdf",
                tagList = tags.Result.Data
            });
        }

        public async Task<Response> FindMyQuotationsPageConfig()
        {
            var accountManager = (await _commercialClient.GetLoggedUserOrganization()).Parse<AccountManagerResponse>();

            var customers = _commercialClient.FindCustomers();
            var canCancelStatusList = _commercialClient.FindCancellationQuotationStatus();
            var canDeleteStatusList = _commercialClient.FindDeletedQuotationStatus();
            var loyaltyTiersList = _configurationService.FindTiers();
            var authorization = _identityClient.GetAccount(accountManager?.AccountManagerUserId ?? Guid.Empty);
            var tags = _configurationClient.FindTags();
            var distributionCenters = _componentClient.FindDistributionCentersComponent();
            var componentsResult = _componentClient.FindComponents();

            await Task.WhenAll(canCancelStatusList, canDeleteStatusList, customers, loyaltyTiersList, authorization,
                tags, distributionCenters, componentsResult);

            var userDetails = authorization.Result.Parse<AccountResponse>();

            var cd = distributionCenters.Result.Data?.ParseRefitResponseJson<List<DistributionCenterDto>>();
            var distributionCenterList = cd.Select(x => _mapper.Map<GenericResponseDto>(x));

            var groupedComponentsDto = componentsResult.Result.Parse<List<GroupComponentsDto>>();

            return _responseService.CreateSuccessResponse(new
            {
                loyaltyTiers = loyaltyTiersList.Result.Data,
                customerList = customers.Result.Data,
                canCancelStatusList = canCancelStatusList.Result.Data,
                canDeleteStatusList = canDeleteStatusList.Result.Data,
                proformaBatchBaseUrl = "https://www.POC.com.br/?quotationsForProforma=",
                ContactDetail = new
                {
                    Name = userDetails?.UserName,
                    userDetails?.Email,
                    Phone = userDetails?.PhoneNumber,
                    Image = userDetails?.AvatarUrlKey
                },
                tagList = tags.Result.Data,
                cdList = distributionCenterList,
                componentList = GroupComponentsDto.Create(groupedComponentsDto)
            });
        }

        public async Task<Response> AddPaymentReceiptQuotation(Guid quotationId, AttachmentRequest attachmentDto)
        {
            attachmentDto.IsPrivate = true;

            var uploadAttachment = await _configurationClient.CreateAttachment(attachmentDto);

            var response =
                await _commercialClient.LinkPaymentReceiptQuotation(quotationId, new { Files = uploadAttachment.Data });

            if (response.Data is null) return response;

            var quotation = response.Parse<LinkPaymentReceiptQuotationResponse>();

            if (quotation?.ProformaFile == null)
                return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);

            var keynames = new List<string> { quotation.ProformaFile.KeyName };
            var findAttachments = GenerateUrlsAttachmentCommand(keynames);
            var proformaFile = await _configurationClient.FindAttachment(findAttachments);
            var files = proformaFile.Parse<List<TermFileResponse>>();
            quotation.ProformaFile = files.FirstOrDefault();

            return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);
        }

        public async Task<Response> AddLiabilityTermQuotation(Guid quotationId, AttachmentRequest attachmentDto)
        {
            attachmentDto.IsPrivate = true;

            var uploadAttachment = await _configurationClient.CreateAttachment(attachmentDto);

            var response = await _commercialClient.LinkLiabilityTermQuotation(quotationId,
                new
                {
                    attachmentDto.SaveInvoicing,
                    TermFile = uploadAttachment.Data
                });

            var termFileResponse = response.Parse<TermFileResponse>();
            var findAttachments = GenerateUrlsAttachmentCommand(new List<string> { termFileResponse.KeyName });
            var invoicingFile = await _configurationClient.FindAttachment(findAttachments);
            var invoicingFileResponse = invoicingFile.Parse<List<AttachmentResponse>>();

            if (invoicingFileResponse.Any())
                termFileResponse.Url = invoicingFileResponse.Select(x => x.Url).FirstOrDefault();

            return _responseService.CreateSuccessResponse(termFileResponse).AddNotifications(response.Notifications);
        }

        public async Task<Response> ValidateInvoicingQuotation(Guid quotationId, ValidateInvoicingQuotationRequest validateInvoicingQuotationDto)
        {
            var validateInvoicing =
                await _commercialClient.ValidateInvoicingQuotation(quotationId, validateInvoicingQuotationDto);

            var notifications = validateInvoicing.Notifications;

            await UpdateQuotationShippingPrice(quotationId);

            return notifications.Any()
                ? _responseService.CreateFailResponse().AddNotifications(notifications)
                : _responseService.CreateSuccessResponse(validateInvoicingQuotationDto);
        }

        public async Task<Response> FindQuotationsList(FindQuotationsDto request)
        {
            var filterRequest = _mapper.Map<FindQuotationsRequest>(request);
            var response = await _commercialClient.FindQuotationsList(filterRequest);
            var quotations = response.Parse<PagedResponse<QuotationListDto>>();
            var quotationsResponse = response.Parse<PagedResponse<QuotationListResponse>>();
            var quotationList = _mapper.Map<List<QuotationListResponse>>(quotations.List);

            foreach (var quotation in quotationList)
            {
                if (quotation.PaymentDetail != null)
                {
                    var bindPayment = quotation.PaymentDetail.ParseRefitResponseJson<PaymentDto>();
                    var payment = _mapper.Map<PaymentResponse>(bindPayment);
                    var keynames = payment.PaymentReceiptFiles.Select(x => x.KeyName);
                    var findAttachments = GenerateUrlsAttachmentCommand(keynames);
                    var paymentReceiptFile = await _configurationClient.FindAttachment(findAttachments);
                    var urls = paymentReceiptFile.Parse<List<TermFileResponse>>();

                    quotation.PaymentDetail = payment.PaymentReceiptFiles.Select(file =>
                    {
                        file.Url = urls.Find(u => u.KeyName == file.KeyName).Url;
                        return file;
                    });
                }

                if (quotation.ProformaFile == null) continue;
                {
                    var keynames = new List<string> { quotation.ProformaFile.KeyName };
                    var findAttachments = GenerateUrlsAttachmentCommand(keynames);
                    var proformaFile = await _configurationClient.FindAttachment(findAttachments);
                    var files = proformaFile.Parse<List<TermFileResponse>>();
                    quotation.ProformaFile.Url = files.Select(y => y.Url).FirstOrDefault();
                }
            }

            quotationsResponse.List = quotationList;

            return _responseService.CreateSuccessResponse(quotationsResponse).AddNotifications(response.Notifications);
        }

        public async Task<Response> BatchDeleteQuotations(BatchDeleteQuotationsRequest command)
        {
            return await _commercialClient.BatchDeleteQuotations(command);
        }

        public async Task<Response> DeleteQuotation(Guid id)
        {
            return await _commercialClient.DeleteQuotation(id);
        }

        public async Task<Response> FindGoalsQuotation()
        {
            return await _commercialClient.FindGoalsQuotation();
        }

        public async Task<Response> CreateQuotation(CreateQuotationRequest request)
        {
            return await _commercialClient.CreateQuotation(request);
        }

        public async Task<Response> ChangeQuotationImportance(Guid id)
        {
            return await _commercialClient.ChangeQuotationImportance(id);
        }

        public async Task<Response> BatchCancelQuotation(CancelQuotationRequest command)
        {
            return await _commercialClient.BatchCancelQuotation(command);
        }

        public async Task<Response> CancelQuotation(Guid id)
        {
            return await _commercialClient.CancelQuotation(id);
        }

        public async Task<Response> ApproveQuotation(Guid id)
        {
            var response = await _commercialClient.ApproveQuotation(id);

            if (response.Data is null) return response;

            var bindQuotation = response.Parse<QuotationDto>();
            var quotation = _mapper.Map<QuotationResponse>(bindQuotation);

            if (quotation?.ProformaFile == null)
                return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);

            var keynames = new List<string> { quotation.ProformaFile.KeyName };
            var findAttachments = GenerateUrlsAttachmentCommand(keynames);
            var proformaFile = await _configurationClient.FindAttachment(findAttachments);
            var files = proformaFile.Parse<List<TermFileResponse>>();
            quotation.ProformaFile = files.FirstOrDefault();

            return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);
        }

        public async Task<Response> CloneQuotation(Guid id)
        {
            return await _commercialClient.CloneQuotation(id);
        }

        public async Task<Response> AddCustomerToQuotation(Guid id, CreateCustomerQuotationRequest command)
        {
            var response = await _commercialClient.AddCustomerToQuotation(id, command);

            await UpdateQuotationShippingPrice(id);

            return response;
        }

        public async Task<Response> SubmitQuotationForValidation(Guid id)
        {
            var response = await _commercialClient.SubmitQuotationForValidation(id);

            if (response.Data is null) return response;

            var bindQuotation = response.Parse<QuotationDto>();
            var quotation = _mapper.Map<QuotationResponse>(bindQuotation);

            if (quotation?.ProformaFile == null)
                return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);

            var keynames = new List<string> { quotation.ProformaFile.KeyName };
            var findAttachments = GenerateUrlsAttachmentCommand(keynames);
            var proformaFile = await _configurationClient.FindAttachment(findAttachments);
            var files = proformaFile.Parse<List<TermFileResponse>>();
            quotation.ProformaFile = files.FirstOrDefault();


            return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);
        }

        public async Task<Response> GetQuotationHistory(Guid id)
        {
            return await _commercialClient.GetQuotationHistory(id);
        }

        public async Task<Response> ValidateQuotationWaitingApproval(Guid id)
        {
            var response = await _commercialClient.ValidateQuotationWaitingApproval(id);

            if (response.Data is null) return response;

            var bindQuotation = response.Parse<QuotationDto>();
            var quotation = _mapper.Map<QuotationResponse>(bindQuotation);

            if (quotation?.ProformaFile == null)
                return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);

            var keynames = new List<string> { quotation.ProformaFile.KeyName };
            var findAttachments = GenerateUrlsAttachmentCommand(keynames);
            var proformaFile = await _configurationClient.FindAttachment(findAttachments);
            var files = proformaFile.Parse<List<TermFileResponse>>();
            quotation.ProformaFile = files.FirstOrDefault();

            return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);
        }

        public async Task<Response> AddInvoicingInQuotation(Guid id, CreateInvoicingQuotationRequest command)
        {
            var request = _mapper.Map<CreateInvoicingQuotationCommercialRequest>(command);
            var pointsConfigsFromUser = await _configurationClient.FindLoyaltyPointsFromUser();
            var pointsConfigurationResponse = pointsConfigsFromUser.Parse<LoyaltyPointsConfigurationRequest>();
            request.LoyaltyPointsConfiguration = pointsConfigurationResponse;

            return await _commercialClient.AddInvoicingInQuotation(id, request);
        }

        public async Task<Response> DeleteSystemInQuotation(Guid id, Guid systemId)
        {
            var pointsConfigsFromUser = await _configurationClient.FindLoyaltyPointsFromUser();
            var pointsConfigurationRequest =
                pointsConfigsFromUser.Parse<LoyaltyPointsConfigurationRequest>();

            var response = await _commercialClient.DeleteSystemInQuotation(id, systemId, pointsConfigurationRequest);

            await UpdateQuotationShippingPrice(id);

            return response;
        }

        public async Task<Response> SaveSystemInQuotation(Guid id)
        {
            var response = await _commercialClient.SaveSystemInQuotation(id);

            if (response.Data is null) return response;

            var bindSystem = response.Parse<PvSystemDto>();

            var system = _mapper.Map<PvSystemResponse>(bindSystem);

            await UpdateQuotationShippingPrice(id);

            return _responseService.CreateSuccessResponse(system).AddNotifications(response.Notifications);
        }

        public async Task<Response> SavePaymentDetail(Guid id, SavePaymentDetailQuotationRequest command)
        {
            var requestCommercial = _mapper.Map<SavePaymentDetailQuotationCommercialRequest>(command);

            var quotationResponse = await _commercialClient.GetQuotationById(id);
            var quotation = quotationResponse.Parse<QuotationResponse>();

            var organizationResponse = await _commercialClient.GetOrganization(quotation.OrganizationDetail.Id);
            var organization = organizationResponse.Parse<OrganizationResponse>();

            var pointsConfigsFromUser = await _configurationClient.FindLoyaltyPointsFromUser();
            var pointsResponse = pointsConfigsFromUser.Parse<FindLoyaltyPointsFromUserResponse>();

            requestCommercial.OrganizationLoyaltyBalance = organization?.LoyaltyPoints ?? 0;
            requestCommercial.PointDiscountValue = pointsResponse?.PointDiscountValue;
            requestCommercial.MaxDiscountPercent = pointsResponse?.MaxDiscountPercent ?? 0M;


            if (requestCommercial.DiscountSices != null && requestCommercial.DiscountPOC.Type == DiscountSicesType.Coupon && requestCommercial.DiscountPOC.CouponCode != null)
            {
                var CouponParametersReponse = await _configurationClient.GetCouponParameters();
                var CouponParameters = CouponParametersReponse.Data.ParseRefitResponseJson<CouponParameterResponse>();

                var CouponResponse = await _configurationClient.GetCouponByCode(requestCommercial.DiscountPOC.CouponCode);
                var coupon = CouponResponse.Data.ParseRefitResponseJson<CouponRequest>();

                requestCommercial.DiscountPOC.Coupon = coupon;
                requestCommercial.DiscountPOC.MaxCouponPercentageDiscount = CouponParameters.MaxCouponPercentageDiscount.Value;

                if (coupon.QuotationId != quotation.Id)
                {
                    coupon.Status = CouponStatusType.Active;
                    await _configurationClient.UpdateCoupon(coupon.Id, coupon);
                }

                requestCommercial.DiscountPOC.Coupon.CountryCode = organization.Address.CountryCode;
            }

            var response = await _commercialClient.SavePaymentDetail(id, requestCommercial);


            if (requestCommercial.DiscountSices != null && requestCommercial.DiscountPOC.Type == DiscountSicesType.Coupon && requestCommercial.DiscountPOC.CouponCode != null)
            {
                var CouponResponse = await _configurationClient.GetCouponByCode(requestCommercial.DiscountPOC.CouponCode);
                var coupon = CouponResponse.Data.ParseRefitResponseJson<CouponRequest>();
                coupon.Status = CouponStatusType.Active;
                coupon.QuotationId = quotation.Id;
                await _configurationClient.UpdateCoupon(coupon.Id, coupon);
            }


            await UpdateQuotationShippingPrice(id);

            return response;
        }

        public async Task<Response> GetQuotationShippingPrice(Guid id, ShippingType shippingType, GetQuotationShippingPriceRequest command)
        {
            var quotation = (await _commercialClient.GetQuotationById(id)).Parse<QuotationResponse>();

            var calculate = await CalculateQuotationShippingPrice(id, shippingType, command);

            if (calculate.Notifications.Exists(x => x.FieldName == "PercentRange"))
            {
                return _responseService.CreateFailResponse(new
                {
                    quotationPrice = (quotation.Price + calculate.ShippingPrice),
                    shippingPrice = calculate.ShippingPrice,
                    freeShipping = calculate.FreeShipping,
                    ivaTaxValue = quotation.IvaTaxValue
                }).AddNotifications(calculate.Notifications);
            }

            return _responseService.CreateSuccessResponse(new
            {
                quotationPrice = (quotation.Price + calculate.ShippingPrice),
                shippingPrice = calculate.ShippingPrice,
                freeShipping = calculate.FreeShipping,
                ivaTaxValue = quotation.IvaTaxValue
            }).AddNotifications(calculate.Notifications);
        }

        public async Task<Response> SaveShippingInQuotation(Guid id, SaveShippingQuotationRequest command)
        {
            var pointsConfigsFromUser = await _configurationClient.FindLoyaltyPointsFromUser();
            var pointsConfigurationResponse =
                pointsConfigsFromUser.Parse<LoyaltyPointsConfigurationRequest>();

            command.LoyaltyPointsConfiguration = pointsConfigurationResponse;

            if (command.Type == 2)
            {
                command.IsManualPrice = false;
            }

            var calculateRequest = new GetQuotationShippingPriceRequest(
                command.Price.GetValueOrDefault(0),
                command.RegionId,
                command.IsManualPrice,
                _mapper.Map<AddressRequest>(command.SelectedAddressDetail)
            );

            var calculateResponse =
                await CalculateQuotationShippingPrice(id, (ShippingType)command.Type, calculateRequest);
            command.Price = calculateResponse.ShippingPrice;

            return await _commercialClient.SaveShippingInQuotation(id, command);
        }

        public async Task<Response> UnlinkPaymentReceipt(Guid id, string filename)
        {
            return await _commercialClient.UnlinkPaymentReceipt(id, filename);
        }

        public async Task<Response> UnlinkLiabilityTerms(Guid id, string filename)
        {
            return await _commercialClient.UnlinkLiabilityTerms(id, filename);
        }

        public async Task<Response> CalculateNewQuotationPrice(Guid id, CalculateQuotationPriceRequest command)
        {
            var pointsConfigsFromUser = await _configurationClient.FindLoyaltyPointsFromUser();
            var pointsResponse = pointsConfigsFromUser.Parse<FindLoyaltyPointsFromUserResponse>();

            var commandCommercial = _mapper.Map<CalculateQuotationPriceCommercialRequest>(command);
            commandCommercial.PointDiscountValue = pointsResponse?.PointDiscountValue;

            if (commandCommercial.DiscountSices != null && commandCommercial.DiscountPOC.Type == DiscountSicesType.Coupon && commandCommercial.DiscountPOC.CouponCode != null)
            {
                var CouponParametersReponse = await _configurationClient.GetCouponParameters();
                var CouponParameters = CouponParametersReponse.Data.ParseRefitResponseJson<CouponParameterResponse>();

                var quotationResponse = await _commercialClient.GetQuotationById(id);
                var quotation = quotationResponse.Data.ParseRefitResponseJson<QuotationResponse>();

                var CouponResponse = await _configurationClient.GetCouponByCode(commandCommercial.DiscountPOC.CouponCode);
                var coupon = CouponResponse.Data.ParseRefitResponseJson<CouponRequest>();

                commandCommercial.DiscountPOC.Coupon = coupon;
                commandCommercial.DiscountPOC.MaxCouponPercentageDiscount = CouponParameters.MaxCouponPercentageDiscount.Value;

                var organizationFound = await _commercialClient.GetOrganization(coupon.OrganizationDetail.Id);
                var organizationResponse = organizationFound.Data.ParseRefitResponseJson<OrganizationResponse>();

                if ((quotation.StatusDetail.Status == QuotationStatusType.Edition || quotation.StatusDetail.Status == QuotationStatusType.InValidation)
                   && (commandCommercial.DiscountPOC.Coupon.ExpirationEnabled && commandCommercial.DiscountPOC.Coupon.ExpirationAt < DateTime.Now))
                {
                    if (coupon.QuotationId == quotation.Id)
                    {
                        coupon.QuotationId = Guid.Empty;
                    }
                    coupon.Status = CouponStatusType.Inactive;
                    await _configurationClient.UpdateCoupon(coupon.Id, coupon);
                }

                commandCommercial.DiscountPOC.Coupon.CountryCode = organizationResponse.Address.CountryCode;
            }

            return await _commercialClient.CalculateNewQuotationPrice(id, commandCommercial);
        }

        public async Task<Response> AddNewSaleFiscalDetail(Guid id, CreateSalesQuotationRequest command)
        {
            return await _commercialClient.AddNewSaleFiscalDetail(id, command);
        }

        public async Task<Response> ValidateReceipt(Guid id, QuotationStatusTypeRequest command)
        {
            var response = await _commercialClient.ValidateReceipt(id, command);

            if (response.Data is null) return response;

            var bindQuotation = response.Parse<QuotationDto>();
            var quotation = _mapper.Map<QuotationResponse>(bindQuotation);

            return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);
        }

        public async Task<Response> InitProduction(Guid id, QuotationStatusTypeRequest command)
        {
            var response = await _commercialClient.InitProduction(id, command);

            if (response.Data is null) return response;

            var bindQuotation = response.Parse<QuotationDto>();
            var quotation = _mapper.Map<QuotationResponse>(bindQuotation);

            if (quotation?.ProformaFile != null)
            {
                var keynames = new List<string> { quotation.ProformaFile.KeyName };
                var findAttachments = GenerateUrlsAttachmentCommand(keynames);
                var proformaFile = await _configurationClient.FindAttachment(findAttachments);
                var files = proformaFile.Parse<List<TermFileResponse>>();
                quotation.ProformaFile = files.FirstOrDefault();
            }

            return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);
        }

        public async Task<Response> ChangeToInProduction(Guid id, QuotationStatusTypeRequest command)
        {
            var response = await _commercialClient.ChangeToInProduction(id, command);

            if (response.Data is null) return response;

            var dto = response.Parse<QuotationDto>();
            var quotation = _mapper.Map<QuotationResponse>(dto);

            var keynames = new List<string> { quotation.ProformaFile.KeyName };
            var findAttachments = GenerateUrlsAttachmentCommand(keynames);
            var proformaFile = await _configurationClient.FindAttachment(findAttachments);
            var files = proformaFile.Parse<List<TermFileResponse>>();
            quotation.ProformaFile = files.FirstOrDefault();

            return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);
        }

        public async Task<Response> ChangeToPickupAvailable(Guid id)
        {
            var quotationFound = await _commercialClient.GetQuotationById(id);
            var quotationResponse = quotationFound.Parse<QuotationResponse>();

            var organizationFound = await _commercialClient.GetOrganization(quotationResponse.OrganizationDetail.Id);
            var organizationResponse = organizationFound.Parse<OrganizationResponse>();

            var pointsForQuotationFound =
                await _configurationClient.GetLoyaltyTierById(organizationResponse.Tier.Id ?? default);
            var pointsConfiguration = pointsForQuotationFound.Parse<LoyaltyTierResponse>();

            var commandCommercial = new ChangeToPickupAvailableCommercialRequest
            {
                QuotationId = id,
                Points = pointsConfiguration?.PointsPerKwpFromCountry ?? 0
            };

            var response = await _commercialClient.ChangeToPickupAvailable(id, commandCommercial);

            if (response.Data is null) return response;

            var bindQuotation = response.Parse<QuotationDto>();
            var quotation = _mapper.Map<QuotationResponse>(bindQuotation);

            if (quotation?.ProformaFile == null)
                return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);

            var keynames = new List<string> { quotation.ProformaFile.KeyName };
            var findAttachments = GenerateUrlsAttachmentCommand(keynames);
            var proformaFile = await _configurationClient.FindAttachment(findAttachments);
            var files = proformaFile.Parse<List<TermFileResponse>>();
            quotation.ProformaFile = files.FirstOrDefault();

            return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);
        }

        public async Task<Response> ChangeQuotationToPickUp(Guid id)
        {
            var response = await _commercialClient.ChangeQuotationToPickUp(id);

            if (response.Data is null) return response;

            var bindQuotation = response.Parse<QuotationDto>();
            var quotation = _mapper.Map<QuotationResponse>(bindQuotation);

            if (quotation?.ProformaFile == null)
                return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);

            var keynames = new List<string> { quotation.ProformaFile.KeyName };
            var findAttachments = GenerateUrlsAttachmentCommand(keynames);
            var proformaFile = await _configurationClient.FindAttachment(findAttachments);
            var files = proformaFile.Parse<List<TermFileResponse>>();
            quotation.ProformaFile = files.FirstOrDefault();

            return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);
        }

        public async Task<Response> RemoveInvoice(Guid id, string filename)
        {
            return await _commercialClient.RemoveInvoice(id, filename);
        }

        public async Task<Response> AddNewSystemInQuotation(Guid quotationId, SystemQuotationDto command)
        {
            var request = _mapper.Map<SystemQuotationRequest>(command);

            var validationComponentRequest = new ValidateComponentRequest
            {
                ComponentList = command.PvSystem.ComponentList.ParseRefitResponseJson<List<ComponentValuesRequest>>()
            };

            var fdi = await _configurationClient.FindFdis();
            validationComponentRequest.Fdi = fdi.Parse<FdiRequest>();

            var componentValidateResponse = await _componentClient.ValidateSystemPower(validationComponentRequest);
            var componentValidate = componentValidateResponse.Parse<ComponentValidateResponse>();

            validationComponentRequest.ComponentList?.ForEach(x =>
            {
                x.Power = componentValidate.Components.FirstOrDefault(y => y.ErpCode == x.ErpCode)?.Power ?? default;
            });

            var pointsConfigsFromUser = await _configurationClient.FindLoyaltyPointsFromUser();
            var pointsConfigurationResponse = pointsConfigsFromUser.Parse<LoyaltyPointsConfigurationRequest>();

            request.LoyaltyPointsConfiguration = pointsConfigurationResponse;
            request.PvSystem.Components = validationComponentRequest.ComponentList;

            var response = await _commercialClient.AddNewSystemInQuotation(quotationId, request);
            var systemResponse = response.Parse<SystemQuotationRequest>();
            var bindSystemResponse = _mapper.Map<SystemQuotationDto>(systemResponse);

            await UpdateQuotationShippingPrice(quotationId);

            return _responseService.CreateSuccessResponse(bindSystemResponse).AddNotifications(response.Notifications);
        }

        public async Task<Response> GetQuotationDetail(Guid quotationId)
        {
            var response = await _commercialClient.GetQuotationById(quotationId);

            if (response.Data is null) return response;

            var bindQuotation = response.Parse<QuotationDto>();
            var quotation = _mapper.Map<QuotationResponse>(bindQuotation);

            var organizationAccountManager = quotation.OrganizationDetail?.AccountManagerUserId;

            if (quotation.OrganizationDetail != null && organizationAccountManager.HasValue && organizationAccountManager.Value != Guid.Empty)
            {
                var authorization = await _identityClient.GetAccount(organizationAccountManager.Value);
                var user = authorization.Parse<AccountResponse>();
                quotation.OrganizationDetail.AccountManagerUserName = user.UserName;
            }

            if (quotation?.PaymentDetail?.PaymentReceiptFiles != null && quotation.PaymentDetail.PaymentReceiptFiles.Any())
            {
                var keynames = quotation.PaymentDetail.PaymentReceiptFiles.Select(x => x.KeyName);
                var findAttachments = GenerateUrlsAttachmentCommand(keynames);
                var paymentFiles = await _configurationClient.FindAttachment(findAttachments);

                PopulatePaymentReceiptFiles(quotation, paymentFiles);
            }

            if (quotation?.InvoicingDetail?.LiabilityTermFiles != null &&
                quotation.InvoicingDetail.LiabilityTermFiles.Any())
            {
                var keynames = quotation.InvoicingDetail.LiabilityTermFiles.Select(x => x.KeyName);
                var findAttachments = GenerateUrlsAttachmentCommand(keynames);
                var invoicingFiles = await _configurationClient.FindAttachment(findAttachments);

                PopulateLiabilityTermFiles(quotation, invoicingFiles);
            }

            if (quotation?.InvoicingDetail?.InvoiceList != null && quotation.InvoicingDetail.InvoiceList.Any())
            {
                var keynames = quotation.InvoicingDetail.InvoiceList.Select(x => x.KeyName);
                var findAttachments = GenerateUrlsAttachmentCommand(keynames);
                var invoicingFiles = await _configurationClient.FindAttachment(findAttachments);

                PopulateInvoicingList(quotation, invoicingFiles);
            }

            if (quotation?.PaymentDetail != null)
            {
                var keynames = quotation.PaymentDetail?.PaymentReceiptFiles?.Select(x => x.KeyName);
                var findAttachments = GenerateUrlsAttachmentCommand(keynames);
                var paymentReceiptFile = await _configurationClient.FindAttachment(findAttachments);
                var urls = paymentReceiptFile.Parse<List<TermFileResponse>>();

                if (quotation.PaymentDetail?.PaymentReceiptFiles != null)
                {
                    PopulateUrlFromPaymentDetailInQuotationDetail(quotation, urls);
                }
            }

            if (quotation?.ProformaFile != null)
            {
                var keynames = new List<string> { quotation.ProformaFile.KeyName };
                var findAttachments = GenerateUrlsAttachmentCommand(keynames);
                var proformaFile = await _configurationClient.FindAttachment(findAttachments);
                var files = proformaFile.Parse<List<TermFileResponse>>();
                quotation.ProformaFile = files.FirstOrDefault();
            }

            if (quotation?.PvSystemDetail?.PvSystemList == null)
                return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);

            var codes = quotation.PvSystemDetail.PvSystemList.SelectMany(s => s.ComponentList).Select(c => c.ErpCode.Value);
            var keyList = await _componentClient.FindKeysByErpCode(new SystemComponentRequest { ErpCodes = codes });
            var keys = keyList.Parse<List<ComponentKeyResponse>>();

            if (keys != null && keys.Any() && quotation.PvSystemDetail?.PvSystemList != null)
            {
                PopulateKeyNamesFromSystemInQuotationDetail(quotation, keys);
            }

            return _responseService.CreateSuccessResponse(quotation).AddNotifications(response.Notifications);
        }

        public async Task<Response> FindLoyaltyPoints(Guid quotationId)
        {
            var quotationResponse = await _commercialClient.GetQuotationById(quotationId);
            var quotation = quotationResponse.Parse<QuotationResponse>();

            if (quotation is null) return quotationResponse;

            var organizationResponse = await _commercialClient.GetOrganization(quotation.OrganizationDetail.Id);
            var organization = organizationResponse.Parse<OrganizationResponse>();

            var pointsConfigsFromUser = await _configurationClient.FindLoyaltyPointsFromUser();

            var request = pointsConfigsFromUser.Parse<FindLoyaltyPointsRequest>();
            request.OrganizationLoyaltyBalance = organization?.LoyaltyPoints ?? 0;

            var response = await _commercialClient.FindLoyaltyPoints(quotationId, request);
            return _responseService.CreateSuccessResponse(response.Data).AddNotifications(response.Notifications);
        }

        public async Task<Response> CreateDanfeCsv(Guid id)
        {
            return await _commercialClient.CreateDanfeCsv(id);
        }

        public async Task<Response> FindQuotationIdsFromTag(Guid tagId)
        {
            return await _commercialClient.FindQuotationIdsFromTag(tagId);
        }

        public async Task<Response> BatchRemoveTagQuotation(Guid tagId, RemoveTagQuotationRequest request)
        {
            await _configurationClient.GetTagById(tagId);

            return await _commercialClient.BatchRemoveTagQuotation(tagId, request);
        }

        public async Task<Response> RemoveTagQuotation(Guid id, Guid tagId)
        {
            await _configurationClient.GetTagById(tagId);

            return await _commercialClient.RemoveTagQuotation(id, tagId);
        }

        public async Task<Response> AttachTagQuotation(Guid id, Guid tagId)
        {
            await _configurationClient.GetTagById(tagId);

            return await _commercialClient.AttachTagQuotation(id, tagId);
        }

        public async Task UpdateQuotationShippingPrice(Guid quotationId)
        {
            var quotation = (await _commercialClient.GetQuotationById(quotationId)).Parse
                <QuotationResponse>();
            var saveShippingRequest = _mapper.Map<SaveShippingQuotationRequest>(quotation.ShippingDetail);

            if (saveShippingRequest.IsValid)
            {
                await SaveShippingInQuotation(quotationId, saveShippingRequest);
            }
        }

        public async Task<CalculateShippingQuotationResponse> CalculateQuotationShippingPrice(Guid quotationId, ShippingType shippingType, GetQuotationShippingPriceRequest command)
        {
            var regionRule = (await _configurationClient.GetRegionById(command.RegionId ?? Guid.Empty)).Parse<RegionRuleDto>();

            if (shippingType.Equals(ShippingType.Own) || regionRule is null && !command.IsManualPrice)
            {
                return new CalculateShippingQuotationResponse(
                    0M,
                    false,
                    null
                );
            }

            var userDetails = (await _identityClient.GetAuthorizationDetail()).Parse<AccountResponse>();

            if (userDetails.IsInternalUser && command.IsManualPrice)
                return new CalculateShippingQuotationResponse(
                    command.Price,
                    false,
                    null
                );

            if (!userDetails.IsInternalUser)
            {
                var quotation = (await _commercialClient.GetQuotationById(quotationId)).Data.ParseRefitResponseJson<QuotationResponse>();

                if (quotation.ShippingDetail.IsManualPrice)
                {
                    return new CalculateShippingQuotationResponse(
                        quotation.ShippingDetail.Price.Value,
                        false,
                        null
                    );
                }
            }

            var systemsShippingResponse =
                await _commercialClient.GetQuotationShippingSystem(quotationId,
                    command.SelectedAddressDetail.StateCode);

            if (systemsShippingResponse.Data is null)
                return new CalculateShippingQuotationResponse(
                    0M,
                    false,
                    null
                );

            var systems = systemsShippingResponse.Parse<IList<PvSystemListDto>>();

            if (!systems.Any())
                return new CalculateShippingQuotationResponse(
                    0M,
                    true,
                    systemsShippingResponse.Notifications
                );

            var totalSystemsPrice = systems.Sum(x => x.Price);

            var shippingPriceResponse = await _configurationClient.CalculateShippingPrice(regionRule?.RuleId ?? Guid.Empty, totalSystemsPrice);

            var notifications = new List<NotificationResponse>();
            notifications.AddRange(systemsShippingResponse.Notifications);
            notifications.AddRange(shippingPriceResponse.Notifications);

            return new CalculateShippingQuotationResponse(
                shippingPriceResponse.Parse<decimal>(),
                false,
                notifications
            );
        }

        public async Task<Response> AttachObservation(Guid id, AttachObservationRequest request)
        {
            return await _commercialClient.AttachObservation(id, request);
        }
        private static void PopulateKeyNamesFromSystemInQuotationDetail(QuotationResponse quotation, IReadOnlyCollection<ComponentKeyResponse> keys)
        {
            quotation.PvSystemDetail.PvSystemList = quotation.PvSystemDetail.PvSystemList.Select(systemItem =>
            {
                systemItem.ComponentList = systemItem.ComponentList.Join(keys,
                    s => s.ErpCode.Value,
                    k => k.ErpCode,
                    (s, k) =>
                    {
                        s.KeyName = k.KeyName;
                        s.ImageKeyName = k.ImageKeyName;

                        return s;
                    }).ToList();

                return systemItem;
            }).ToList();
        }

        private static GenerateUrlsAttachmentRequest GenerateUrlsAttachmentCommand(IEnumerable<string> keynames, bool isPrivate = true)
        {
            return new GenerateUrlsAttachmentRequest { KeyNames = keynames.ToList(), IsPrivate = isPrivate };
        }

        private static void PopulateLiabilityTermFiles(QuotationResponse quotation, Response invoicingFiles)
        {
            quotation.InvoicingDetail.LiabilityTermFiles = invoicingFiles.Parse
                <List<AttachmentResponse>>()
                .Join(quotation.InvoicingDetail.LiabilityTermFiles, a => a.KeyName, t => t.KeyName,
                    (attachmentResponse, termFileResponse) => new TermFileResponse
                    {
                        FileName = termFileResponse.FileName,
                        KeyName = termFileResponse.KeyName,
                        Url = attachmentResponse.Url,
                        UploadedAt = termFileResponse.UploadedAt
                    });
        }

        private static void PopulatePaymentReceiptFiles(QuotationResponse quotation, Response paymentFiles)
        {
            quotation.PaymentDetail.PaymentReceiptFiles = paymentFiles.Parse
                <List<AttachmentResponse>>()
                .Join(quotation.PaymentDetail.PaymentReceiptFiles, a => a.KeyName, t => t.KeyName,
                    (attachmentResponse, termFileResponse) => new TermFileResponse
                    {
                        FileName = termFileResponse.FileName,
                        KeyName = termFileResponse.KeyName,
                        Url = attachmentResponse.Url,
                        UploadedAt = termFileResponse.UploadedAt
                    });
        }

        private static void PopulateInvoicingList(QuotationResponse quotation, Response invoicingList)
        {
            quotation.InvoicingDetail.InvoiceList = invoicingList.Parse
                <List<AttachmentResponse>>()
                .Join(quotation.InvoicingDetail.InvoiceList, a => a.KeyName, t => t.KeyName,
                    (attachmentResponse, termFileResponse) => new TermFileResponse
                    {
                        FileName = termFileResponse.FileName,
                        KeyName = termFileResponse.KeyName,
                        Url = attachmentResponse.Url,
                        UploadedAt = termFileResponse.UploadedAt,
                        Number = termFileResponse.Number
                    });
        }

        private static void PopulateUrlFromPaymentDetailInQuotationDetail(QuotationResponse quotation, List<TermFileResponse> urls)
        {
            quotation.PaymentDetail.PaymentReceiptFiles = quotation.PaymentDetail.PaymentReceiptFiles.Select(
                file =>
                {
                    file.Url = urls.Find(u => u.KeyName == file.KeyName)?.Url;
                    return file;
                });
        }
    }
}