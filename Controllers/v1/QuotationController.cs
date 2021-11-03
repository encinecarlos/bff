using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Fixed;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Quotations;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/quotations")]
    public class QuotationController : BaseController
    {
        private readonly IQuotationService _quotationService;

        public QuotationController(
            ILogger<QuotationController> logger,
            IResponseService responseService,
            IQuotationService quotationService) :
            base(logger, responseService)
        {
            _quotationService = quotationService;
        }

        /// <summary>
        ///     Find All quotation by filters
        /// </summary>
        /// <returns>List of quotation</returns>
        [HttpPost("search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindQuotationList([FromBody] FindQuotationsDto filters)
        {
            return await SafeExecuteAsync(async () => await _quotationService.FindQuotationsList(filters),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Get quotation by identifier
        /// </summary>
        /// <param name="id">quotation identification</param>
        /// <returns>Quotation</returns>
        [HttpGet("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetQuotationDetail(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.GetQuotationDetail(id), HttpMethod.Get);
        }


        /// <summary>
        ///     Delete quotations from one or N identifiers in command string
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> BatchDeleteQuotation(BatchDeleteQuotationsRequest command)
        {
            return await SafeExecuteAsync(async () => await _quotationService.BatchDeleteQuotations(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Delete quotation from identifier
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> DeleteQuotation(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.DeleteQuotation(id), HttpMethod.Delete);
        }


        /// <summary>
        ///     Returns Goals current month quotations
        /// </summary>
        /// <returns>List of quotations, total power and total price</returns>
        [HttpGet("goals")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindGoalsQuotation()
        {
            return await SafeExecuteAsync(async () => await _quotationService.FindGoalsQuotation(), HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new quotation
        /// </summary>
        /// <returns>Quotation created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostQuotation(CreateQuotationRequest request)
        {
            return await SafeExecuteAsync(async () => await _quotationService.CreateQuotation(request), HttpMethod.Post);
        }


        [HttpGet("page/configuration")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindMyQuotationsPageConfig()
        {
            return await SafeExecuteAsync(async () => await _quotationService.FindMyQuotationsPageConfig(),
                HttpMethod.Get);
        }


        /// <summary>
        ///     Change importance from quotation
        /// </summary>
        /// <param name="id"> quotation identifier </param>
        /// <returns></returns>
        [HttpPatch("{id}/importance")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> ChangeImportance(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.ChangeQuotationImportance(id),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Cancel quotations from one or any identifiers in command string
        /// </summary>
        /// <param name="command"> Request to cancel </param>
        /// <returns></returns>
        [HttpPost("cancel")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> BatchCancelQuotation(CancelQuotationRequest command)
        {
            return await SafeExecuteAsync(async () => await _quotationService.BatchCancelQuotation(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Cancel quotation from identifier
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{id}/cancel")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> BatchCancelQuotationId(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.CancelQuotation(id), HttpMethod.Patch);
        }

        /// <summary>
        ///     Approve quotation
        /// </summary>
        /// <param name="id">quotation identifier</param>
        /// <returns></returns>
        [HttpPost("{id}/approve")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> ApproveQuotation(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.ApproveQuotation(id), HttpMethod.Post);
        }

        /// <summary>
        ///     Clone quotation
        /// </summary>
        /// <param name="id">Request to clone</param>
        /// <returns></returns>
        [HttpPost("{id}/clone")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CloneQuotation(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.CloneQuotation(id), HttpMethod.Post);
        }

        /// <summary>
        ///     Add a new customer to Quotation
        /// </summary>
        /// <param name="id">Quotation id that will receive customer</param>
        /// <param name="command">Json containing the fields to add customer to quotation</param>
        /// <returns>Customer added</returns>
        [HttpPatch("{id}/customer")]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> AddCustomerToQuotation(Guid id, CreateCustomerQuotationRequest command)
        {
            return await SafeExecuteAsync(async () => await _quotationService.AddCustomerToQuotation(id, command),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Submit quotation for validation
        /// </summary>
        /// <param name="id">Quotation identifier</param>
        /// <returns></returns>
        [HttpPost("{id}/validate")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> SubmitForValidation(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.SubmitQuotationForValidation(id),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Find All Quotation History By Quotation Identifier
        /// </summary>
        /// <param name="id">Quotation identifier</param>
        /// <returns>List of Quotation History</returns>
        [HttpGet("{id}/history")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetQuotationHistory(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.GetQuotationHistory(id), HttpMethod.Get);
        }

        /// <summary>
        ///     update status
        /// </summary>
        /// <param name="id">Change quotation status to waiting approval</param>
        /// <returns></returns>
        [HttpPatch("{id}/wait-approval")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ValidateWaitingApproval(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.ValidateQuotationWaitingApproval(id),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Add a new invoicing to Quotation
        /// </summary>
        /// <param name="id">Quotation id that will receive invoicing</param>
        /// <param name="command">Json containing the fields to add invoicing to quotation</param>
        /// <returns>Invoicing added</returns>
        [HttpPatch("{id}/invoicing")]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public virtual async Task<IActionResult> AddInvoicingInQuotation(Guid id,
            CreateInvoicingQuotationRequest command)
        {
            return await SafeExecuteAsync(async () => await _quotationService.AddInvoicingInQuotation(id, command),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Add New System in quotation
        /// </summary>
        /// <param name="id">quotation identifier</param>
        /// <param name="command">Request to add pvSystemDetail</param>
        /// <returns></returns>
        [HttpPatch("{id}/systems")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> AddNewSystemInQuotation(Guid id, SystemQuotationDto command)
        {
            return await SafeExecuteAsync(async () => await _quotationService.AddNewSystemInQuotation(id, command),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Delete pvSystem in quotation
        /// </summary>
        /// <param name="id">quotation identifier</param>
        /// <param name="systemId">system identifier</param>
        /// <returns></returns>
        [HttpDelete("{id}/systems/{systemId}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> DeletePvSystemQuotation(Guid id, Guid systemId)
        {
            return await SafeExecuteAsync(async () => await _quotationService.DeleteSystemInQuotation(id, systemId),
                HttpMethod.Delete);
        }

        /// <summary>
        ///     Save pvSystem in quotation
        /// </summary>
        /// <param name="id">quotation identifier</param>
        /// <returns></returns>
        [HttpPatch("{id}/systems/save")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> SaveSystemInQuotation(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.SaveSystemInQuotation(id),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Save payment detail in quotation
        /// </summary>
        /// <param name="id">quotation identifier</param>
        /// <param name="command">object contains fields to save payment</param>
        /// <returns></returns>
        [HttpPatch("{id}/payment/save")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> SavePaymentDetail(Guid id, SavePaymentDetailQuotationRequest command)
        {
            return await SafeExecuteAsync(async () => await _quotationService.SavePaymentDetail(id, command),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Call will return the budget amount considering the shipping type.
        /// </summary>
        /// <param name="id">Quotation identification</param>
        /// <param name="shippingType">Shipping type</param>
        /// <param name="command">Additional info</param>
        /// <returns>Quotation</returns>
        [HttpPost("{id}/shipping/{shippingType}/price")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetShippingValue(Guid id, ShippingType shippingType,
            [FromBody] GetQuotationShippingPriceRequest command)
        {
            return await SafeExecuteAsync(
                async () => await _quotationService.GetQuotationShippingPrice(id, shippingType, command),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Save Shipping step
        /// </summary>
        /// <param name="id">quotation identifier</param>
        /// <param name="command">Request to save shipping step</param>
        /// <returns></returns>
        [HttpPatch("{id}/shipping/save")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> SaveShippingInQuotation(Guid id, SaveShippingQuotationRequest command)
        {
            return await SafeExecuteAsync(async () => await _quotationService.SaveShippingInQuotation(id, command),
                HttpMethod.Patch);
        }


        /// <summary>
        ///     Delete payment receipt from file name
        /// </summary>
        /// <param name="id">Quotation identifier</param>
        /// <param name="filename">File name</param>
        /// <returns></returns>
        [HttpDelete("{id}/attachment/payment/{filename}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> UnlinkPaymentReceipt(Guid id, string filename)
        {
            return await SafeExecuteAsync(async () => await _quotationService.UnlinkPaymentReceipt(id, filename),
                HttpMethod.Delete);
        }

        /// <summary>
        ///     Delete LiabilityTermFiles from file name
        /// </summary>
        /// <param name="id">Quotation identifier</param>
        /// <param name="filename">File name</param>
        /// <returns></returns>
        [HttpDelete("{id}/attachment/liability-term/{filename}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> UnlinkLiabilityTerms(Guid id, string filename)
        {
            return await SafeExecuteAsync(async () => await _quotationService.UnlinkLiabilityTerms(id, filename),
                HttpMethod.Delete);
        }

        /// <summary>
        ///     CalculateSystem new quotation price based on discount types or additions informed by user
        /// </summary>
        /// <param name="id">Quotation identifier</param>
        /// <param name="command">Request to recalculate quotation price</param>
        /// <returns></returns>
        [HttpPost("{id}/price/calculate")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> CalculateNewQuotationPrice(Guid id, CalculateQuotationPriceRequest command)
        {
            return await SafeExecuteAsync(async () => await _quotationService.CalculateNewQuotationPrice(id, command),
                HttpMethod.Post);
        }


        /// <summary>
        ///     Add New Sales Fiscal Detail in quotation
        /// </summary>
        /// <param name="id">quotation identifier</param>
        /// <param name="command">Request to Sales Fiscal Detail</param>
        /// <returns></returns>
        [HttpPatch("{id}/sale")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> AddNewSaleFiscalDetail(Guid id, CreateSalesQuotationRequest command)
        {
            return await SafeExecuteAsync(async () => await _quotationService.AddNewSaleFiscalDetail(id, command),
                HttpMethod.Patch);
        }


        [HttpGet("page/detail/configuration")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FetchQuotationDetailPageConfig()
        {
            return await SafeExecuteAsync(async () => await _quotationService.FindQuotationDetailPageConfig(), HttpMethod.Get);
        }

        /// <summary>
        ///     Link payment receipt to quotation and change the quotation status
        /// </summary>
        /// <param name="id">Quotation identifier</param>
        /// <param name="attachmentDto">Request to validation</param>
        /// <returns></returns>
        [HttpPatch("{id}/attachment/payment")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> LinkPaymentReceiptQuotation(Guid id, AttachmentRequest attachmentDto)
        {
            return await SafeExecuteAsync(async () =>
                await _quotationService.AddPaymentReceiptQuotation(id, attachmentDto), HttpMethod.Patch);
        }

        /// <summary>
        ///     Link liability term to quotation
        /// </summary>
        /// <param name="id">Quotation identifier</param>
        /// <param name="attachmentDto">Request to validation</param>
        /// <returns></returns>
        [HttpPatch("{id}/attachment/liability-term")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> LinkLiabilityTermQuotation(Guid id, AttachmentRequest attachmentDto)
        {
            return await SafeExecuteAsync(async () =>
                await _quotationService.AddLiabilityTermQuotation(id, attachmentDto), HttpMethod.Patch);
        }

        /// <summary>
        ///     Validate invoicing
        /// </summary>
        /// <param name="id">Quotation id that will receive invoicing</param>
        /// <param name="validateInvoicingQuotationDto">Json containing the fields to validate invoicing</param>
        /// <returns></returns>
        [HttpPost("{id}/invoicing/validate")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ValidateInvoicing(Guid id,
            ValidateInvoicingQuotationRequest validateInvoicingQuotationDto)
        {
            return await SafeExecuteAsync(
                async () => await _quotationService.ValidateInvoicingQuotation(id, validateInvoicingQuotationDto),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Validate receipt
        /// </summary>
        /// <param name="id">Quotation id to validate receipt</param>
        /// <param name="command">Request to validate receipt</param>
        /// <returns></returns>
        [HttpPatch("{id}/payment/receipt/validate")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ValidateReceipt(Guid id, QuotationStatusTypeRequest command)
        {
            return await SafeExecuteAsync(async () => await _quotationService.ValidateReceipt(id, command),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Init production quotation
        /// </summary>
        /// <param name="id">Quotation id to init production</param>
        /// <param name="command">Request to to init production</param>
        /// <returns></returns>
        [HttpPatch("{id}/initialize-production")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> InitProduction(Guid id, QuotationStatusTypeRequest command)
        {
            return await SafeExecuteAsync(async () => await _quotationService.InitProduction(id, command),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Change Status Quotation in Production
        /// </summary>
        /// <param name="id">Quotation id to change quotation status to production</param>
        /// <param name="command">Request to change quotation status to production</param>
        /// <returns></returns>
        [HttpPatch("{id}/produce")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ChangeToInProduction(Guid id, QuotationStatusTypeRequest command)
        {
            return await SafeExecuteAsync(async () => await _quotationService.ChangeToInProduction(id, command),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Change Status Quotation Pickup Available
        /// </summary>
        /// <param name="id">Quotation id to change quotation status to pickup available</param>
        /// <returns></returns>
        [HttpPatch("{id}/pickup-available")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ChangeToPickupAvailable(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.ChangeToPickupAvailable(id),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Change Status Quotation PickedUp
        /// </summary>
        /// <param name="id">Quotation id to change quotation status to picked up</param>
        /// <returns></returns>
        [HttpPatch("{id}/pickedup")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ChangeQuotationToPickUp(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.ChangeQuotationToPickUp(id),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Remove invoice on quotation from file name
        /// </summary>
        /// <param name="id">Quotation identifier</param>
        /// <param name="filename">File name</param>
        /// <returns></returns>
        [HttpDelete("{id}/attachment/invoice/{filename}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> RemoveInvoice(Guid id, string filename)
        {
            return await SafeExecuteAsync(async () => await _quotationService.RemoveInvoice(id, filename),
                HttpMethod.Delete);
        }

        /// <summary>
        ///     Create danfe csv
        /// </summary>
        /// <param name="id">Quotation identifier</param>
        /// <returns></returns>
        [HttpPatch("{id}/danfe/csv")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> CreateDanfeCsv(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.CreateDanfeCsv(id), HttpMethod.Patch);
        }

        /// <summary>
        ///     Find loyalty points from quotation
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Loyalty points from quotation</returns>
        [HttpGet("{id}/loyalty-points")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindLoyaltyPoints(Guid id)
        {
            return await SafeExecuteAsync(async () => await _quotationService.FindLoyaltyPoints(id), HttpMethod.Get);
        }

        /// <summary>
        ///     Attach tag to quotation
        /// </summary>
        /// <param name="id">Quotation Id</param>
        /// <param name="tagId">Tag Id</param>
        /// <returns></returns>
        [HttpPatch("{id}/tag/{tagId}/attach")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> AttachTagQuotation(Guid id, Guid tagId)
        {
            return await SafeExecuteAsync(async () => await _quotationService.AttachTagQuotation(id, tagId),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Remove tag from specific quotation
        /// </summary>
        /// <param name="id">Quotation id to remove a tag</param>
        /// <param name="tagId">Tag Id</param>
        /// <returns></returns>
        [HttpPatch("{id}/tag/{tagId}/remove")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> RemoveTagQuotation(Guid id, Guid tagId)
        {
            return await SafeExecuteAsync(async () => await _quotationService.RemoveTagQuotation(id, tagId),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Attach new observation on quotation
        /// </summary>
        /// <param name="id">Quotation identifier</param>
        /// <param name="request">Json containing observation</param>
        /// <returns></returns>
        [HttpPatch("{id}/observation")]
        public async Task<IActionResult> AttachObservations(Guid id, AttachObservationRequest request)
        {
            return await SafeExecuteAsync(async () => await _quotationService.AttachObservation(id, request), HttpMethod.Patch);
        }
    }
}