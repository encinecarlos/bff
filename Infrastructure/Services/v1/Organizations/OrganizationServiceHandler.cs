using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Domain.ValueObjects;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Organizations
{
    public class OrganizationServiceHandler : IOrganizationService
    {
        private readonly ICommercialClient _commercialClient;
        private readonly IComponentClient _componentClient;
        private readonly IConfigurationClient _configurationClient;
        private readonly IIdentityClient _identityClient;
        private readonly IMapper _mapper;
        private readonly IResponseService _responseService;

        public OrganizationServiceHandler(
            ICommercialClient commercialClient,
            IConfigurationClient configurationClient,
            IMapper mapper,
            IIdentityClient identityClient,
            IComponentClient componentClient,
            IResponseService responseService)
        {
            _commercialClient = commercialClient;
            _configurationClient = configurationClient;
            _mapper = mapper;
            _identityClient = identityClient;
            _componentClient = componentClient;
            _responseService = responseService;
        }

        public async Task<Response> FindCustomers()
        {
            return await _commercialClient.FindCustomers();
        }

        public async Task<Response> GetCustomer(string document)
        {
            return await _commercialClient.GetCustomer(document);
        }

        public async Task<Response> CreateCustomer(CustomerRequest command)
        {
            if (command.Address != null)
            {
                await _configurationClient.ValidStateCode(command.Address.CountryCode, command.Address.StateCode);
            }

            return await _commercialClient.CreateCustomer(command);
        }

        public async Task<Response> UpdateCustomer(string document, CustomerRequest command)
        {
            if (command.Address != null)
            {
                await _configurationClient.ValidStateCode(command.Address.CountryCode, command.Address.StateCode);
            }

            var response = await _commercialClient.UpdateCustomer(document, command);

            await _commercialClient.UpdateQuotationCustomer(command.QuotationId, command);

            return response;
        }

        public async Task<Response> DeleteCustomer(string document)
        {
            return await _commercialClient.DeleteCustomer(document);
        }

        public async Task<Response> FindDiscountTypes()
        {
            return await _commercialClient.FindDiscountTypes();
        }

        public async Task<Response> FindInsurances(FindInsuranceRequest query)
        {
            if (query.PromotionId == Guid.Empty) return await _commercialClient.FindInsurances(query);

            var promotionInsurances =
                (await _commercialClient.GetInsurancesByPromotionId(query.PromotionId)).Parse<List<Guid>>();

            query.Ids = promotionInsurances;

            return await _commercialClient.FindInsurances(query);
        }

        public async Task<Response> GetInsuranceByRange(FindInsuranceByRangesRequest query)
        {
            var request = _mapper.Map<FindInsuranceByRangesRequest, FindInsuranceByRangesCommercialRequest>(query);

            if (query.PromotionId != Guid.Empty)
            {
                var promotionInsurances =
                    (await _commercialClient.GetInsurancesByPromotionId(query.PromotionId)).Parse<List<Guid>>();
                request.InsuranceIds = promotionInsurances;
            }

            return await _commercialClient.GetInsuranceByRange(request);
        }

        public async Task<Response> GetOrganization(Guid id)
        {
            var organization = await _commercialClient.GetOrganization(id);

            if (organization.Data is null) return _responseService.CreateFailResponse(null).AddNotifications(organization.Notifications);

            var organizationResponse = organization.Parse<OrganizationResponse>();

            if (organizationResponse.DocumentFiles != null || organizationResponse.DocumentFiles.Any())
            {
                var keyNames = organizationResponse.DocumentFiles.Select(x => x.KeyName).ToList();
                var findAttachments = GenerateUrlsAttachment(keyNames);
                var file = await _configurationClient.FindAttachment(findAttachments);
                organizationResponse.DocumentFiles =
                    PopulateLiabilityTermFiles(organizationResponse.DocumentFiles, file);
            }

            var userByOrganization = await _identityClient.GetAccountByOrganizationId(id);

            if (userByOrganization.Data is null)
                return _responseService.CreateFailResponse(null).AddNotifications(userByOrganization.Notifications);

            organizationResponse.AccountList = userByOrganization.Data;

            return _responseService.CreateSuccessResponse(organizationResponse);
        }

        public async Task<Response> DeclineOrganization(Guid id)
        {
            return await _commercialClient.DeclineOrganization(id);
        }

        public async Task<Response> FindOrganizations()
        {
            return await _commercialClient.FindOrganizations();
        }

        public async Task<Response> CreateOrganization(OrganizationRequest command)
        {
            var culture = await _configurationClient.GetCultureByCountryCode(command.CountryCode);
            var cultureResponse = culture.Parse<CultureResponse>();

            command.Language = cultureResponse.Language;
            command.Currency = cultureResponse.Currency;

            var responseValidate = await _identityClient.ValidateUserIsLogged();
            var validate = responseValidate.Parse<bool>();

            if (command.Address != null)
            {
                await _configurationClient.ValidStateCode(command.CountryCode, command.Address.StateCode);

                if (!validate)
                {
                    var resp = await _componentClient.GetDistributionCenterByStatusCode(command.CountryCode,
                        command.Address.StateCode);
                    var distributionCenter = resp.Parse<DistributionCenterDto>();
                    var distributionCenterValid = distributionCenter?.Id != Guid.Empty ? distributionCenter?.Id : null;

                    command.DistributionCenterId = distributionCenterValid;
                    command.DistributionCenterName = distributionCenter?.Description;
                    command.SicesExpressDistributionCenterId = distributionCenterValid;
                }
                else
                {
                    if (!command.DistributionCenterId.HasValue || command.DistributionCenterId.Value == Guid.Empty)
                        return _responseService.CreateFailResponse(null);

                    var distributionCenter =
                        await _componentClient.GetDistributionCenter(command.DistributionCenterId.Value);
                    var distributionCenterResponse =
                        distributionCenter.Parse<DistributionCenterDto>();
                    command.DistributionCenterName = distributionCenterResponse.Description;
                }
            }

            var response = await _commercialClient.CreateOrganization(command);
            var organization = response.Parse<OrganizationResponse>();
            var externalAccount = new AddNewOwnerExternalAccountRequest
            {
                UserName = organization.ContactName,
                Email = organization.Email,
                OrganizationId = organization.Id,
                Language = command.Language,
                CountryCode = command.CountryCode,
                CurrencyCode = command.Currency,
                Country = cultureResponse.Country
            };

            await _identityClient.CreateOwnerExternalAccount(externalAccount);

            return _responseService.CreateSuccessResponse(organization);
        }

        public async Task<Response> DeleteOrganization(Guid id)
        {
            return await _commercialClient.DeleteOrganization(id);
        }

        public async Task<Response> UpdateOrganization(Guid id, OrganizationRequest command)
        {
            if (command.Address != null)
            {
                await _configurationClient.ValidStateCode(command.CountryCode, command.Address.StateCode);
            }

            if (command.DistributionCenterId != null)
            {
                var distributionCenter = await _componentClient.GetDistributionCenter(command.DistributionCenterId.Value);

                if (distributionCenter.Data is null)
                    return _responseService.CreateFailResponse(null).AddNotifications(distributionCenter.Notifications);

                var distributionCenterResponse = distributionCenter?.Parse<DistributionCenterDto>();
                command.DistributionCenterName = distributionCenterResponse.Description;
            }

            var updateOrganizationCommand = _mapper.Map<UpdateOrganizationDto>(command);
            updateOrganizationCommand.ErpCustomerCode = new ErpCode(command.ErpCustomerCode?.Value);

            var organization = await _commercialClient.UpdateOrganization(id, updateOrganizationCommand);

            if (organization.Data is null) return _responseService.CreateFailResponse(null).AddNotifications(organization.Notifications);

            var organizationResponse = organization.Parse<UpdateOrganizationDto>();

            var request = new UpdateExternalAccountByOrganizationRequest
            {
                Email = organizationResponse.Email,
                OldEmail = organizationResponse.OldEmail,
                OldContactName = organizationResponse.OldContactName,
                ContactName = organizationResponse.ContactName
            };

            if (!request.Email.Equals(request.OldEmail) || !request.ContactName.Equals(request.OldContactName))
                await _identityClient.UpdateOwnerExternalAccount(request);

            if (organizationResponse.InternalUser)
                return _responseService.CreateSuccessResponse();


            var accountManager = (await _commercialClient.GetLoggedUserOrganization()).Parse<AccountManagerResponse>();
            var account = await _identityClient.GetAccount(accountManager?.AccountManagerUserId ?? Guid.Empty);

            if (account.Data is null) return _responseService.CreateFailResponse(null).AddNotifications(account.Notifications);

            var accountResponse = account.Parse<AccountResponse>();
            var contactDetail = new
            {
                Name = accountResponse.UserName,
                accountResponse.Email,
                Phone = accountResponse.PhoneNumber,
                ProfileImgUrl = accountResponse.AvatarUrlKey
            };
            var response = new { organizationResponse, contactDetail };

            return _responseService.CreateSuccessResponse(response);
        }

        public async Task<Response> FindPaymentMethods()
        {
            return await _commercialClient.FindPaymentMethods();
        }

        public async Task<Response> FindPaymentConditions()
        {
            return await _commercialClient.FindPaymentConditions();
        }

        public async Task<Response> GetPaymentCondition(Guid id)
        {
            return await _commercialClient.GetPaymentCondition(id);
        }

        public async Task<Response> UpdatePaymentCondition(Guid id, PaymentConditionRequest request)
        {
            return await _commercialClient.UpdatePaymentCondition(id, request);
        }

        public async Task<Response> CreatePaymentCondition(PaymentConditionRequest command)
        {
            return await _commercialClient.CreatePaymentCondition(command);
        }

        public async Task<Response> DeletePaymentCondition(Guid id)
        {
            return await _commercialClient.DeletePaymentCondition(id);
        }

        public async Task<Response> FindQuotationsStatus()
        {
            return await _commercialClient.FindQuotationsStatus();
        }

        public async Task<Response> FindCancellationQuotationStatus()
        {
            return await _commercialClient.FindCancellationQuotationStatus();
        }

        public async Task<Response> FindDeletedQuotationStatus()
        {
            return await _commercialClient.FindDeletedQuotationStatus();
        }

        public async Task<Response> FindOrganizationsList(FindOrganizationsListRequest request)
        {
            var response = await _commercialClient.SearchOrganizations(request);
            var organizations = response.Parse<PagedResponse<OrganizationListResponse>>();

            return _responseService.CreateSuccessResponse(organizations).AddNotifications(response.Notifications);
        }

        public async Task<Response> UpdateIgnoreTierChangeUntilAsync(Guid id, UpdateIgnoreTierChangeUntilRequest command)
        {
            var loyaltyTierResponse = await _configurationClient.GetLoyaltyTierById(command.TierId);

            if (loyaltyTierResponse?.Data is null)
                return _responseService.CreateFailResponse().AddNotifications(loyaltyTierResponse?.Notifications);

            var loyaltyTier = loyaltyTierResponse.Parse<LoyaltyTierResponse>();

            var commandCommercial = _mapper.Map<UpdateIgnoreTierChangeUntilCommercialRequest>(command);

            commandCommercial.TierName = loyaltyTier.Name;

            var response = await _commercialClient.UpdateIgnoreTierChangeUntil(id, commandCommercial);

            return _responseService.CreateSuccessResponse(response.Data).AddNotifications(response.Notifications);
        }

        public async Task<Response> GetInsuranceByRange(Guid promotionId)
        {
            return await _commercialClient.ValidateFreeInsurancePromotion(promotionId);
        }

        public async Task<Response> AcceptTerms(AcceptTermRequest request)
        {
            return await _commercialClient.AcceptTerms(request);
        }

        public async Task<Response> GetAllTerms()
        {
            return await _commercialClient.GetAllTerms();
        }

        public async Task<Response> TransferLinkedOrganizations(TransferLinkedOrganizationRequest request)
        {
            var accounts = new List<Guid> { request.OriginSicesUserId, request.DestinationSicesUserId };
            var validateInternalUserRequest = new ValidateInternalAccountRequest { Accounts = accounts };
            await _identityClient.ValidateInternalAccount(validateInternalUserRequest);

            return await _commercialClient.TransferLinkedOrganizations(request);
        }

        public async Task<Response> AddNewOrganizationAttachment(Guid id, AttachmentRequest request)
        {
            var permission = new PermissionInternalAccountRequest { ModulesType = new List<int> { 2 }, PermissionType = 3 };
            request.IsPrivate = true;
            var hasPermission = await _identityClient.VerifyInternalAccountPermission(permission);

            if (hasPermission.Success is false)
                return _responseService.CreateFailResponse(null).AddNotifications(hasPermission.Notifications);

            await _commercialClient.ValidateOrganizationDocumentFiles(id);

            var attachment = await _configurationClient.CreateAttachment(request);
            if (attachment.Data is null) return _responseService.CreateFailResponse(null).AddNotifications(attachment.Notifications);
            var attachmentResponse = attachment.Parse<AttachmentResponse>();
            var commercialRequest = new AddNewOrganizationAttachmentRequest { Id = id, Files = attachmentResponse };

            var uploadDocument = await _commercialClient.UploadOrganizationDocument(id, commercialRequest);

            if (uploadDocument.Data is null)
                return _responseService.CreateFailResponse(null).AddNotifications(uploadDocument.Notifications);
            var documentResponse = uploadDocument.Parse<AddNewOrganizationAttachmentRequest>();
            var response = new
            {
                attachmentResponse.FileName,
                attachmentResponse.KeyName,
                UploadAt = documentResponse.Files.UploadedAt,
                Url = attachmentResponse.DownloadLink
            };
            return _responseService.CreateSuccessResponse(response);
        }

        public async Task<Response> ApproveOrganization(ApproveOrganizationRequest request)
        {
            var response = await _commercialClient.ApproveOrganization(request.OrganizationId);

            var email = response.Parse<string>();

            return await _identityClient.WaitApproveAccount(new { email });
        }

        public async Task<Response> UploadOrganizationAttachment(Guid organizationId, string keyName, UpdateOrganizationAttachmentRequest request)
        {
            var organization = await _commercialClient.UpdateOrganizationAttachment(organizationId, keyName, request);
            var response = organization.Parse<OrganizationResponse>();
            PopulateDocumentFiles(response);
            return _responseService.CreateSuccessResponse(response);
        }

        public async Task<Response> ResendOrganizationConfirmationEmail(ResendOrganizationConfirmationEmailRequest request)
        {
            return await _commercialClient.ResendOrganizationConfirmationEmail(request.OrganizationId);
        }

        public async Task<Response> DeleteOrganizationAttachment(Guid organizationId, string keyName, DeleteOrganizationAttachmentRequest request)
        {
            await _configurationClient.DeleteAttachment(request);
            return await _commercialClient.DeleteOrganizationDocument(organizationId, keyName, request);
        }

        public async Task<Response> ActivateOrganization(Guid organizationId)
        {
            var organization = await _commercialClient.GetOrganization(organizationId);
            var organizationResponse = organization.Parse<OrganizationResponse>();

            var externalAccountFlags = await _identityClient.GetExternalAccountFlagsByEmail(organizationResponse.Email);
            var externalAccountFlagsResponse =
                externalAccountFlags.Parse<GetExternalAccountFlagsResponse>();

            return await _commercialClient.ActivateOrganization(organizationId,
                new { externalAccountFlagsResponse.EmailVerified, externalAccountFlagsResponse.PasswordDefined });
        }

        public async Task<Response> ConfirmationOrganizationEmail(ConfirmationOrganizationEmailRequest request)
        {
            await _commercialClient.ConfirmationOrganizationEmail(request);

            return await _identityClient.ConfirmationEmailOwner(new { request.Email, request.VerificationCode });
        }

        public async Task<Response> ResendOrganizationApproveEmail(ResendOrganizationApproveEmailRequest request)
        {
            var response = await _commercialClient.ResendApproveEmail(request.OrganizationId);

            var email = response.Parse<string>();

            return await _identityClient.WaitApproveAccount(new { email });
        }

        public async Task<Response> DisableOrganization(Guid id)
        {
            await _commercialClient.DisableOrganization(id);

            return await _identityClient.DisableOrganizationUsers(id);
        }

        private static List<TermFileResponse> PopulateLiabilityTermFiles(List<TermFileResponse> documentFiles, Response files)
        {
            documentFiles = files.Parse
                <List<TermFileResponse>>()
                .Join(documentFiles, a => a.KeyName, t => t.KeyName,
                    (attachmentResponse, termFileResponse) => new TermFileResponse
                    {
                        FileName = termFileResponse.FileName,
                        KeyName = termFileResponse.KeyName,
                        Url = attachmentResponse.Url,
                        UploadedAt = termFileResponse.UploadedAt
                    }).ToList();
            return documentFiles;
        }

        private static GenerateUrlsAttachmentRequest GenerateUrlsAttachment(IEnumerable<string> keynames, bool isPrivate = true)
        {
            return new GenerateUrlsAttachmentRequest { KeyNames = keynames.ToList(), IsPrivate = isPrivate };
        }

        private async void PopulateDocumentFiles(OrganizationResponse organization)
        {
            var keyNames = organization.DocumentFiles.Select(x => x.KeyName);
            var command = GenerateUrlsAttachment(keyNames);
            var response = await _configurationClient.FindAttachment(command);
            var files = response.Parse<List<TermFileResponse>>();
            organization.DocumentFiles = organization.DocumentFiles.Join(files, x => x.KeyName, y => y.KeyName,
                (organizationDocumentFile, attachedFile) =>
                {
                    organizationDocumentFile.Url = attachedFile.Url;

                    return organizationDocumentFile;
                }).ToList();
        }
    }
}