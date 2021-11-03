using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Fixed;
using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Quotations
{
    public interface IQuotationService
    {
        Task<Response> FindMyQuotationsPageConfig();
        Task<Response> FindQuotationDetailPageConfig();
        Task<Response> GetQuotationDetail(Guid quotationId);
        Task<Response> AddPaymentReceiptQuotation(Guid quotationId, AttachmentRequest attachmentDto);
        Task<Response> AddLiabilityTermQuotation(Guid quotationId, AttachmentRequest attachmentDto);
        Task<Response> AddNewSystemInQuotation(Guid quotationId, SystemQuotationDto command);
        Task<Response> ValidateInvoicingQuotation(Guid quotationId, ValidateInvoicingQuotationRequest validateInvoicingQuotationDto);
        Task<Response> FindQuotationsList(FindQuotationsDto request);
        Task<Response> BatchDeleteQuotations(BatchDeleteQuotationsRequest command);
        Task<Response> DeleteQuotation(Guid id);
        Task<Response> FindGoalsQuotation();
        Task<Response> CreateQuotation(CreateQuotationRequest request);
        Task<Response> ChangeQuotationImportance(Guid id);
        Task<Response> BatchCancelQuotation(CancelQuotationRequest command);
        Task<Response> CancelQuotation(Guid id);
        Task<Response> ApproveQuotation(Guid id);
        Task<Response> CloneQuotation(Guid id);
        Task<Response> AddCustomerToQuotation(Guid id, CreateCustomerQuotationRequest command);
        Task<Response> SubmitQuotationForValidation(Guid id);
        Task<Response> GetQuotationHistory(Guid id);
        Task<Response> ValidateQuotationWaitingApproval(Guid id);
        Task<Response> AddInvoicingInQuotation(Guid id, CreateInvoicingQuotationRequest command);
        Task<Response> DeleteSystemInQuotation(Guid id, Guid systemId);
        Task<Response> SaveSystemInQuotation(Guid id);
        Task<Response> SavePaymentDetail(Guid id, SavePaymentDetailQuotationRequest command);
        Task<Response> GetQuotationShippingPrice(Guid id, ShippingType shippingType, GetQuotationShippingPriceRequest command);
        Task<Response> SaveShippingInQuotation(Guid id, SaveShippingQuotationRequest command);
        Task<Response> UnlinkPaymentReceipt(Guid id, string filename);
        Task<Response> UnlinkLiabilityTerms(Guid id, string filename);
        Task<Response> CalculateNewQuotationPrice(Guid id, CalculateQuotationPriceRequest command);
        Task<Response> AddNewSaleFiscalDetail(Guid id, CreateSalesQuotationRequest command);
        Task<Response> ValidateReceipt(Guid id, QuotationStatusTypeRequest command);
        Task<Response> InitProduction(Guid id, QuotationStatusTypeRequest command);
        Task<Response> ChangeToInProduction(Guid id, QuotationStatusTypeRequest command);
        Task<Response> ChangeToPickupAvailable(Guid id);
        Task<Response> ChangeQuotationToPickUp(Guid id);
        Task<Response> RemoveInvoice(Guid id, string filename);
        Task<Response> CreateDanfeCsv(Guid id);
        Task<Response> FindLoyaltyPoints(Guid quotationId);
        Task<Response> FindQuotationIdsFromTag(Guid tagId);
        Task<Response> BatchRemoveTagQuotation(Guid tagId, RemoveTagQuotationRequest request);
        Task<Response> RemoveTagQuotation(Guid id, Guid tagId);
        Task<Response> AttachTagQuotation(Guid id, Guid tagId);
        Task<Response> AttachObservation(Guid id, AttachObservationRequest request);
    }
}