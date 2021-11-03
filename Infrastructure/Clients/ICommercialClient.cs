using Refit;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Clients
{
    public interface ICommercialClient
    {

        #region Discounts

        [Get("/api/v1/discount-types")]
        Task<Response> FindDiscountTypes();

        #endregion

        #region Invoicing

        [Get("/api/v1/invoicing-types")]
        Task<Response> FindInvoicingTypes();

        #endregion

        #region Systems

        [Post("/api/v1/systems")]
        Task<Response> GenerateNewSystem([Body] GenerateNewSystemRequest request);

        [Post("/api/v1/systems/validate")]
        Task<Response> ValidateSystem([Body] ComponentValidateResponse request);

        #endregion

        #region Address


        [Get("/api/v1/address/{countryCode}/{zipcode}")]
        Task<Response> GetAddress(string countryCode, string zipcode);

        [Get("/api/v1/address/{zipcode}")]
        Task<Response> GetAddress(string zipcode);

        #endregion

        #region Insurances

        [Post("/api/v1/insurances/search")]
        Task<Response> FindInsurances([Body] FindInsuranceRequest request);

        [Post("/api/v1/insurances/ranges")]
        Task<Response> GetInsuranceByRange([Body] FindInsuranceByRangesCommercialRequest request);

        [Post("/api/v1/insurances/promotion-insurance/{promotionId}")]
        Task<Response> ValidateFreeInsurancePromotion(Guid promotionId);


        #endregion

        #region Payments

        [Get("/api/v1/payments/methods")]
        Task<Response> FindPaymentMethods();

        [Get("/api/v1/payments/conditions")]
        Task<Response> FindPaymentConditions();

        [Get("/api/v1/payments/conditions/{id}")]
        Task<Response> GetPaymentCondition(Guid id);

        [Put("/api/v1/payments/conditions/{id}")]
        Task<Response> UpdatePaymentCondition(Guid id, [Body] PaymentConditionRequest request);

        [Post("/api/v1/payments/conditions")]
        Task<Response> CreatePaymentCondition([Body] PaymentConditionRequest request);

        [Delete("/api/v1/payments/conditions/{id}")]
        Task<Response> DeletePaymentCondition(Guid id);

        #endregion

        #region Loyalty

        [Post("/api/v1/loyalty/search")]
        Task<Response> FetchLoyaltyPoints([Body] FetchLoyaltyPointsRequest request);

        [Post("/api/v1/loyalty")]
        Task<Response> AddLoyaltyPoints([Body] AddLoyaltyPointsEntryRequest command);

        [Post("/api/v1/loyalty/{id}/revert")]
        Task<Response> RevertLoyaltyPoints(Guid id);

        #endregion

        #region Shipping

        [Get("/api/v1/shipping/types")]
        Task<Response> FindShippingTypes();

        [Get("/api/v1/shipping/address-types")]
        Task<Response> FindAddressTypes();

        [Get("/api/v1/shipping/companies")]
        Task<Response> FindShippingCompanies();

        [Post("/api/v1/shipping/companies")]
        Task<Response> CreateShippingCompany([Body] ShippingCompanyRequest request);

        [Get("/api/v1/shipping/companies/{document}")]
        Task<Response> GetShippingCompany(string document);

        [Put("/api/v1/shipping/companies/{document}")]
        Task<Response> UpdateShippingCompany(string document, [Body] ShippingCompanyRequest request);

        [Delete("/api/v1/shipping/companies/{document}")]
        Task<Response> DeleteShippingCompanies(string document);

        #endregion

        #region Customers

        [Get("/api/v1/customers")]
        Task<Response> FindCustomers();

        [Post("/api/v1/customers")]
        Task<Response> CreateCustomer([Body] CustomerRequest request);

        [Get("/api/v1/customers/{document}")]
        Task<Response> GetCustomer(string document);

        [Put("/api/v1/customers/{document}")]
        Task<Response> UpdateCustomer(string document, [Body] CustomerRequest request);

        [Delete("/api/v1/customers/{document}")]
        Task<Response> DeleteCustomer(string document);

        #endregion

        #region Components

        [Post("/api/v1/components/search")]
        Task<Response> FindGroupedComponentsByTypeAsync([Body] SystemComponentRequest request);

        [Get("/api/v1/components")]
        Task<Response> FindPublishedMemorialComponentsGroupedByType([Query] FindPublishedMemorialComponentsGroupedByTypeRequest request);


        [Delete("/api/v1/components/memorial/{memorialId}")]
        Task<Response> DeleteComponentsByMemorial(Guid memorialId, [Body] List<string> erpCodes);

        [Post("/api/v1/components/clone/memorial/{memorialFrom}/{memorialTo}")]
        Task<Response> CloneComponentsFromMemorial(Guid memorialFrom, Guid memorialTo, [Body] List<string> erpCodes);

        [Post("/api/v1/components/clone/memorial/published/{memorialTo}")]
        Task<Response> CloneComponentsFromPublishedMemorial(Guid memorialTo);

        #endregion

        #region Comments

        [Get("/api/v1/comments/{memorialId}/memorial")]
        Task<Response> FindMemorialComments(Guid memorialId);

        [Post("/api/v1/comments/memorial/message")]
        Task<Response> CreateMemorialComment([Body] CommentRequest request);

        [Post("/api/v1/comments/quotation/note")]
        Task<Response> CreateQuotationNote([Body] CommentDto request);

        [Post("/api/v1/comments/quotation/Message")]
        Task<Response> CreateQuotationMessage([Body] CommentDto request);

        [Post("/api/v1/comments/quotation/message/{id}/replay")]
        Task<Response> CreateQuotationReply(Guid id, [Body] CommentDto request);

        [Get("/api/v1/comments/quotation/{id}/notes")]
        Task<Response> GetQuotationNotes(Guid id);

        [Get("/api/v1/comments/quotation/{id}/messages")]
        Task<Response> GetQuotationMessages(Guid id);

        [Delete("/api/v1/comments/{id}")]
        Task<Response> DeleteComment(Guid id);

        [Patch("/api/v1/comments/notification/{id}/read")]
        Task<Response> ChangeNotificationAsRead(Guid id, ChangeNotificationAsReadRequest request);

        [Patch("/api/v1/comments/message/read")]
        Task<Response> ChangeMessageAsRead(ChangeMessageAsReadDto request);

        [Patch("/api/v1/comments/notes/read")]
        Task<Response> ChangeNotesAsRead(ChangeNotesAsReadDto request);

        [Post("/api/v1/comments/notification")]
        Task<Response> FindNotifications(FindNotificationsRequest request);

        [Get("/api/v1/comments/{id}/type")]
        Task<Response> GetCommentType(Guid id);


        [Post("/api/v1/comments/quotation/note/notification")]
        Task<Response> AddNewNoteNotification([Body] object request);

        [Post("/api/v1/comments/quotation/message/notification")]
        Task<Response> AddNewMessageNotification([Body] object request);
        #endregion

        #region Promotions

        [Get("/api/v1/promotions/{promotionId}/components")]
        Task<Response> FindErpCodesOnPromotion(Guid promotionId, Guid distributionCenterId, int tierId);

        [Get("/api/v1/promotions/{id}/check-active")]
        Task<Response> CheckPromotionActive(Guid id);

        [Post("/api/v1/promotions/validate-status")]
        Task<Response> ValidatePromotionStatus();

        [Patch("/api/v1/promotions/{id}/status/{status}")]
        Task<Response> ValidatePromotionStatus(Guid id, int status);

        [Post("/api/v1/promotions")]
        Task<Response> AddNewPromotion([Body] PromotionCommercialRequest request);

        [Post("/api/v1/promotions/search")]
        Task<Response> FindPromotions([Body] FindPromotionsRequest filters);

        [Get("/api/v1/promotions/{id}")]
        Task<Response> GetPromotion(Guid id);

        [Put("/api/v1/promotions/{id}")]
        Task<Response> UpdatePromotion(Guid id, [Body] PromotionCommercialRequest request);

        [Delete("/api/v1/promotions/{id}")]
        Task<Response> DeletePromotion(Guid id);

        [Post("/api/v1/promotions/{id}/clone")]
        Task<Response> ClonePromotion(Guid id, ClonePromotionRequest request);

        [Get("/api/v1/promotions/available")]
        Task<Response> GetAvailablePromotions([Query] Guid quotationId);

        [Get("/api/v1/promotions/active")]
        Task<Response> GetActivePromotions();

        [Get("/api/v1/promotions/{id}/insurances")]
        Task<Response> GetInsurancesByPromotionId(Guid id);

        #endregion

        #region Memorials

        [Get("/api/v1/memorials/published")]
        Task<Response> GetPublishedMemorial();

        [Post("/api/v1/memorials/published/clone")]
        Task<Response> ClonePublishedMemorial();

        [Post("/api/v1/memorials")]
        Task<Response> CreateMemorial([Body] CreateMemorialRequest request);

        [Post("/api/v1/memorials/search")]
        Task<Response> FindMemorials([Body] FindMemorialsRequest request);

        [Get("/api/v1/memorials/{id}")]
        Task<Response> GetMemorial(Guid id);

        [Put("/api/v1/memorials/{id}/components")]
        Task<Response> SaveMemorialComponents(Guid id, [Body] SaveMemorialRequest request);

        [Patch("/api/v1/memorials/{id}/components/{erpCode}/cmv")]
        Task<Response> UpdateMemorialComponentCmv(Guid id, string erpCode, [Body] UpdateMemorialComponentCmvRequest request);

        [Patch("/api/v1/memorials/{id}/components/cmv")]
        Task<Response> BatchChangeMemorialComponentCmv(Guid id, [Body] BatchChangeMemorialComponentRequest request);

        [Patch("/api/v1/memorials/{id}/components/{erpCode}/markup")]
        Task<Response> UpdateMemorialComponentMarkup(Guid id, string erpCode, [Body] UpdateMemorialComponentMarkupRequest request);

        [Patch("/api/v1/memorials/{id}/components/markup")]
        Task<Response> BatchChangeComponentMarkup(Guid id, [Body] BatchChangeComponentMarkupRequest request);

        [Put("/api/v1/organizations/{id}")]
        Task<Response> UpdateOrganization(Guid id, [Body] UpdateOrganizationDto request);

        [Post("/api/v1/memorials/{id}/components/search")]
        Task<Response> FindMemorialComponents(Guid id, [Body] FindComponentRequest request);

        [Post("/api/v1/memorials/{id}/clone")]
        Task<Response> CloneMemorial(Guid id);

        [Post("/api/v1/memorials/{id}/publish")]
        Task<Response> PublishMemorial(Guid id);

        [Post("/api/v1/memorials/delete")]
        Task<Response> BatchDeleteMemorials([Body] BatchDeleteMemorialsRequest request);

        [Get("/api/v1/memorials/session")]
        Task<Response> GetMemorialSession();

        [Post("/api/v1/memorials/{id}/group-components/search")]
        Task<Response> FindMemorialGroupComponents(Guid id, [Body] GetMemorialRequest request);

        [Patch("/api/v1/memorials/{id}")]
        Task<Response> ChangeMemorial(Guid id, [Body] ChangeMemorialRequest request);

        #endregion

        #region Quotations

        [Get("/api/v1/quotation-status/cancel")]
        Task<Response> FindCancellationQuotationStatus();

        [Get("/api/v1/quotation-status")]
        Task<Response> FindQuotationsStatus();

        [Get("/api/v1/quotation-status/delete")]
        Task<Response> FindDeletedQuotationStatus();

        [Post("/api/v1/quotations/search")]
        Task<Response> FindQuotationsList([Body] FindQuotationsRequest request);

        [Post("/api/v1/quotations/delete")]
        Task<Response> BatchDeleteQuotations([Body] BatchDeleteQuotationsRequest request);

        [Delete("/api/v1/quotations/{id}")]
        Task<Response> DeleteQuotation(Guid id);

        [Get("/api/v1/quotations/{quotationId}")]
        Task<Response> GetQuotationById(Guid quotationId);

        [Post("/api/v1/quotations")]
        Task<Response> CreateQuotation([Body] CreateQuotationRequest request);

        [Patch("/api/v1/quotations/{id}/importance")]
        Task<Response> ChangeQuotationImportance(Guid id);

        [Post("/api/v1/quotations/cancel")]
        Task<Response> BatchCancelQuotation([Body] CancelQuotationRequest request);

        [Patch("/api/v1/quotations/{id}/cancel")]
        Task<Response> CancelQuotation(Guid id);

        [Patch("/api/v1/quotations/{id}/approve")]
        Task<Response> ApproveQuotation(Guid id);

        [Post("/api/v1/quotations/{id}/clone")]
        Task<Response> CloneQuotation(Guid id);

        [Patch("/api/v1/quotations/{id}/customer")]
        Task<Response> AddCustomerToQuotation(Guid id, [Body] CreateCustomerQuotationRequest request);

        [Post("/api/v1/quotations/{id}/validate")]
        Task<Response> SubmitQuotationForValidation(Guid id);

        [Patch("/api/v1/quotations/{id}/wait-approval")]
        Task<Response> ValidateQuotationWaitingApproval(Guid id);

        [Patch("/api/v1/quotations/{id}/invoicing")]
        Task<Response> AddInvoicingInQuotation(Guid id, [Body] CreateInvoicingQuotationCommercialRequest request);

        [Patch("/api/v1/quotations/{id}/systems")]
        Task<Response> AddNewSystemInQuotation(Guid id, [Body] SystemQuotationRequest request);

        [Delete("/api/v1/quotations/{id}/systems/{systemId}")]
        Task<Response> DeleteSystemInQuotation(Guid id, Guid systemId,
            [Query] LoyaltyPointsConfigurationRequest loyaltyPointsConfiguration);

        [Patch("/api/v1/quotations/{id}/systems/save")]
        Task<Response> SaveSystemInQuotation(Guid id);

        [Patch("/api/v1/quotations/{id}/payment/save")]
        Task<Response> SavePaymentDetail(Guid id, [Body] SavePaymentDetailQuotationCommercialRequest request);

        [Patch("/api/v1/quotations/{id}/shipping/save")]
        Task<Response> SaveShippingInQuotation(Guid id, [Body] SaveShippingQuotationRequest request);

        [Delete("/api/v1/quotations/{id}/attachment/payment/{filename}")]
        Task<Response> UnlinkPaymentReceipt(Guid id, string filename);

        [Delete("/api/v1/quotations/{id}/attachment/liability-term/{filename}")]
        Task<Response> UnlinkLiabilityTerms(Guid id, string filename);

        [Get("/api/v1/quotations/goals")]
        Task<Response> FindGoalsQuotation();

        [Post("/api/v1/quotations/{id}/price/calculate")]
        Task<Response> CalculateNewQuotationPrice(Guid id, [Body] CalculateQuotationPriceCommercialRequest command);

        [Patch("/api/v1/quotations/{id}/attachment/payment")]
        Task<Response> LinkPaymentReceiptQuotation(Guid id, [Body] object request);

        [Patch("/api/v1/quotations/{id}/attachment/liability-term")]
        Task<Response> LinkLiabilityTermQuotation(Guid id, [Body] object request);

        [Post("/api/v1/quotations/{id}/invoicing/validate")]
        Task<Response> ValidateInvoicingQuotation(Guid id, [Body] object request);

        [Get("/api/v1/quotations/{quotationId}/history")]
        Task<Response> GetQuotationHistory(Guid quotationId);

        [Patch("/api/v1/quotations/{id}/sale")]
        Task<Response> AddNewSaleFiscalDetail(Guid id, [Body] CreateSalesQuotationRequest command);

        [Patch("/api/v1/quotations/{id}/payment/receipt/validate")]
        Task<Response> ValidateReceipt(Guid id, [Body] QuotationStatusTypeRequest command);

        [Get("/api/v1/quotations/{id}/organization-detail")]
        Task<Response> GetOrganizationDetailInQuotation(Guid id);


        [Patch("/api/v1/quotations/{id}/initialize-production")]
        Task<Response> InitProduction(Guid id, [Body] QuotationStatusTypeRequest command);

        [Patch("/api/v1/quotations/{id}/produce")]
        Task<Response> ChangeToInProduction(Guid id, [Body] QuotationStatusTypeRequest command);

        [Patch("/api/v1/quotations/{id}/pickup-available")]
        Task<Response> ChangeToPickupAvailable(Guid id, [Body] ChangeToPickupAvailableCommercialRequest command);

        [Patch("/api/v1/quotations/{id}/pickedup")]
        Task<Response> ChangeQuotationToPickUp(Guid id);

        [Delete("/api/v1/quotations/{id}/attachment/invoice/{filename}")]
        Task<Response> RemoveInvoice(Guid id, [Body] string fileName);

        [Post("/api/v1/quotations/{id}/loyalty-points")]
        Task<Response> FindLoyaltyPoints(Guid id, [Body] FindLoyaltyPointsRequest request);

        [Patch("/api/v1/quotations/{id}/danfe/csv")]
        Task<Response> CreateDanfeCsv(Guid id);

        [Post("/api/v1/quotations/{id}/validate-expiration")]
        Task<Response> ValidateQuotationExpiration(Guid id);

        [Get("/api/v1/quotations/{id}/state/{stateCode}/shipping/system")]
        Task<Response> GetQuotationShippingSystem(Guid id, string stateCode);

        [Get("/api/v1/quotations/tag/{tagId}")]
        Task<Response> FindQuotationIdsFromTag(Guid tagId);

        [Patch("/api/v1/quotations/tag/{tagId}/remove")]
        Task<Response> BatchRemoveTagQuotation(Guid tagId, [Body] RemoveTagQuotationRequest request);

        [Patch("/api/v1/quotations/{id}/tag/{tagId}/remove")]
        Task<Response> RemoveTagQuotation(Guid id, Guid tagId);

        [Patch("/api/v1/quotations/{id}/tag/{tagId}/attach")]
        Task<Response> AttachTagQuotation(Guid id, Guid tagId);

        [Post("/api/v1/quotations/comment/message/validate")]
        Task<Response> ValidateCommentMessage([Body]object request);


        [Patch("/api/v1/quotations/{id}/observation")]
        Task<Response> AttachObservation(Guid id, [Body]AttachObservationRequest request);

        [Put("/api/v1/quotations/{id}/customer")]
        Task<Response> UpdateQuotationCustomer(Guid id, [Body] CustomerRequest request);

        #endregion

        #region Organizations

        [Get("/api/v1/organizations/{organizationId}/authorization")]
        Task<Response> GetAuthorizationDetail(Guid organizationId);

        [Get("/api/v1/organizations/{id}")]
        Task<Response> GetOrganization(Guid id);

        [Get("/api/v1/organizations/current")]
        Task<Response> GetLoggedUserOrganization();

        [Get("/api/v1/organizations")]
        Task<Response> FindOrganizations();

        [Post("/api/v1/organizations/search")]
        Task<Response> SearchOrganizations([Body] FindOrganizationsListRequest request);

        [Post("/api/v1/organizations")]
        Task<Response> CreateOrganization([Body] OrganizationRequest request);

        [Delete("/api/v1/organizations/{id}")]
        Task<Response> DeleteOrganization(Guid id);

        [Put("/api/v1/organizations/{id}")]
        Task<Response> UpdateOrganization(Guid id, [Body] OrganizationRequest request);

        [Patch("/api/v1/organizations/{id}/tier")]
        Task<Response> UpdateIgnoreTierChangeUntil(Guid id, [Body] UpdateIgnoreTierChangeUntilCommercialRequest request);

        [Post("/api/v1/organizations/current/validate")]
        Task<Response> ValidateCurrentOrganization();

        [Patch("/api/v1/organizations/accept-terms")]
        Task<Response> AcceptTerms([Body] AcceptTermRequest request);

        [Get("/api/v1/organizations/accept-terms")]
        Task<Response> GetAllTerms();

        [Patch("/api/v1/organizations/transfer")]
        Task<Response> TransferLinkedOrganizations([Body] TransferLinkedOrganizationRequest request);

        [Patch("/api/v1/organizations/{id}/decline")]
        Task<Response> DeclineOrganization(Guid id);

        [Patch("/api/v1/organizations/{id}/approve")]
        Task<Response> ApproveOrganization(Guid id);

        [Post("/api/v1/organizations/{id}/attachment/validate")]
        Task<Response> ValidateOrganizationDocumentFiles(Guid id);

        [Patch("/api/v1/organizations/{organizationId}/attachment/{keyName}")]
        Task<Response> UpdateOrganizationAttachment(Guid organizationId, string keyName, [Body] UpdateOrganizationAttachmentRequest request);

        [Delete("/api/v1/organizations/{organizationId}/attachment/{keyName}")]
        Task<Response> DeleteOrganizationDocument(Guid organizationId, string keyName, [Body] DeleteOrganizationAttachmentRequest request);

        [Patch("/api/v1/organizations/email/confirmation")]
        Task<Response> ConfirmationOrganizationEmail([Body] ConfirmationOrganizationEmailRequest request);

        [Patch("/api/v1/organizations/active-verification")]
        Task<Response> ActiveOrganizationAfterChangePassword(object request);

        [Patch("/api/v1/organizations/{id}/email/approve")]
        Task<Response> ResendApproveEmail(Guid id);

        [Patch("/api/v1/organizations/{id}/disable")]
        Task<Response> DisableOrganization(Guid id);

        [Patch("/api/v1/organizations/{id}/email")]
        Task<Response> ResendOrganizationConfirmationEmail(Guid id);

        [Patch("/api/v1/organizations/{id}/attachment")]
        Task<Response> UploadOrganizationDocument(Guid id, AddNewOrganizationAttachmentRequest response);

        [Patch("/api/v1/organizations/{id}/active")]
        Task<Response> ActivateOrganization(Guid id, [Body] object request);

        [Post("/api/v1/organizations/login/validate")]
        Task<Response> ValidateOrganization(object request);

        [Post("/api/v1/organizations/mention/validate")]
        Task<Response> ValidateOrganizationDistributionCenter([Body]object request);

        #endregion
    }
}