using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Organizations
{
    public interface IOrganizationService
    {
        Task<Response> FindCustomers();
        Task<Response> GetCustomer(string document);
        Task<Response> CreateCustomer(CustomerRequest command);
        Task<Response> UpdateCustomer(string document, CustomerRequest command);
        Task<Response> DeleteCustomer(string document);
        Task<Response> FindDiscountTypes();
        Task<Response> FindInsurances(FindInsuranceRequest query);
        Task<Response> GetInsuranceByRange(FindInsuranceByRangesRequest query);
        Task<Response> GetOrganization(Guid id);
        Task<Response> DeclineOrganization(Guid id);
        Task<Response> FindOrganizations();
        Task<Response> CreateOrganization(OrganizationRequest command);
        Task<Response> DeleteOrganization(Guid id);
        Task<Response> UpdateOrganization(Guid id, OrganizationRequest command);
        Task<Response> FindPaymentMethods();
        Task<Response> FindPaymentConditions();
        Task<Response> GetPaymentCondition(Guid id);
        Task<Response> UpdatePaymentCondition(Guid id, PaymentConditionRequest request);
        Task<Response> CreatePaymentCondition(PaymentConditionRequest command);
        Task<Response> DeletePaymentCondition(Guid id);
        Task<Response> FindQuotationsStatus();
        Task<Response> FindCancellationQuotationStatus();
        Task<Response> FindDeletedQuotationStatus();
        Task<Response> FindOrganizationsList(FindOrganizationsListRequest request);
        Task<Response> UpdateIgnoreTierChangeUntilAsync(Guid id, UpdateIgnoreTierChangeUntilRequest command);
        Task<Response> GetInsuranceByRange(Guid promotionId);
        Task<Response> AcceptTerms(AcceptTermRequest request);
        Task<Response> GetAllTerms();
        Task<Response> TransferLinkedOrganizations(TransferLinkedOrganizationRequest request);
        Task<Response> ApproveOrganization(ApproveOrganizationRequest request);

        Task<Response> UploadOrganizationAttachment(Guid organizationId, string keyName,
            UpdateOrganizationAttachmentRequest request);

        Task<Response> ResendOrganizationConfirmationEmail(ResendOrganizationConfirmationEmailRequest request);
        Task<Response> AddNewOrganizationAttachment(Guid id, AttachmentRequest request);

        Task<Response> DeleteOrganizationAttachment(Guid organizationId, string keyName,
            DeleteOrganizationAttachmentRequest request);

        Task<Response> ActivateOrganization(Guid organizationId);
        Task<Response> ConfirmationOrganizationEmail(ConfirmationOrganizationEmailRequest request);
        Task<Response> ResendOrganizationApproveEmail(ResendOrganizationApproveEmailRequest request);
        Task<Response> DisableOrganization(Guid id);
    }
}