using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Organizations;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/organizations")]
    public class OrganizationController : BaseController
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(
            ILogger<OrganizationController> logger,
            IResponseService responseService,
            IOrganizationService organizationService) : base(logger, responseService)
        {
            _organizationService = organizationService;
        }

        /// <summary>
        ///     Get Organization by identifier
        /// </summary>
        /// <param name="id">Organization identification</param>
        /// <returns>Organization</returns>
        [HttpGet("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetOrganization(Guid id)
        {
            return await SafeExecuteAsync(async () => await _organizationService.GetOrganization(id), HttpMethod.Get);
        }


        /// <summary>
        ///     Find All Organization by filters
        /// </summary>
        /// <returns>List of Organization</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindOrganization()
        {
            return await SafeExecuteAsync(async () => await _organizationService.FindOrganizations(), HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new Organization
        /// </summary>
        /// <param name="command">Json containing the fields to create the Organization</param>
        /// <returns>Organization created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostOrganization(OrganizationRequest command)
        {
            return await SafeExecuteAsync(async () => await _organizationService.CreateOrganization(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Delete Organization by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusDetail Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteOrganization(Guid id)
        {
            return await SafeExecuteAsync(async () => await _organizationService.DeleteOrganization(id),
                HttpMethod.Delete);
        }

        /// <summary>
        ///     Find organizations by filters
        /// </summary>
        /// <returns>List of organizations</returns>
        [HttpPost("search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindOrganizationList(FindOrganizationsListRequest filters)
        {
            return await SafeExecuteAsync(async () => await _organizationService.FindOrganizationsList(filters),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Update Organization by identifier
        /// </summary>
        /// <param name="id">Organization identifier</param>
        /// <param name="command">Json containing the fields to update the Organization</param>
        /// <returns>Organization Updated</returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutOrganization(Guid id, OrganizationRequest command)
        {
            return await SafeExecuteAsync(async () => await _organizationService.UpdateOrganization(id, command),
                HttpMethod.Put);
        }

        /// <summary>
        ///     Update Tier and end date of the period we will use to change the level images (ignoreTierChangeUntil).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns>Organization updated</returns>
        [HttpPatch("{id}/tier-ignore-change-until")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> UpdateIgnoreTierChangeUntil(Guid id,
            UpdateIgnoreTierChangeUntilRequest command)
        {
            return await SafeExecuteAsync(
                async () => await _organizationService.UpdateIgnoreTierChangeUntilAsync(id, command), HttpMethod.Patch);
        }

        /// <summary>
        ///     Set accept terms on organization
        /// </summary>
        /// <param name="request">Json containing fields to acceptance term</param>
        [HttpPatch("accept-terms")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> AcceptTerms(AcceptTermRequest request)
        {
            return await SafeExecuteAsync(async () => await _organizationService.AcceptTerms(request),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Get all accept terms and what terms accept by the organization
        /// </summary>
        [HttpGet("accept-terms")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetAllTerms()
        {
            return await SafeExecuteAsync(async () => await _organizationService.GetAllTerms(), HttpMethod.Get);
        }

        /// <summary>
        ///     Decline Organization by identifier
        /// </summary>
        /// <param name="organizationId">Organization identifier</param>
        /// <returns></returns>
        [HttpPut("decline/{organizationId}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeclineOrganization(Guid organizationId)
        {
            return await SafeExecuteAsync(async () => await _organizationService.DeclineOrganization(organizationId),
                HttpMethod.Put);
        }


        /// <summary>
        ///     Transfer all organizations linked on Sices user to other Sices user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("transfer-linked")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> TransferLinkedOrganizations(TransferLinkedOrganizationRequest request)
        {
            return await SafeExecuteAsync(async () => await _organizationService.TransferLinkedOrganizations(request),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Update Organization Document
        /// </summary>
        /// <param name="id">organization identifier</param>
        /// <param name="request">Request to validation</param>
        /// <returns></returns>
        [HttpPatch("{id}/attachment/organization")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> AddNewOrganizationAttachment(Guid id, AttachmentRequest request)
        {
            return await SafeExecuteAsync(
                async () => await _organizationService.AddNewOrganizationAttachment(id, request), HttpMethod.Patch);
        }

        /// <summary>
        ///     Change organization status to approve organization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("approve")]
        public async Task<IActionResult> ApproveOrganization(ApproveOrganizationRequest request)
        {
            return await SafeExecuteAsync(async () => await _organizationService.ApproveOrganization(request),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Update organization document name
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="keyName"></param>
        /// <param name="request">Json containing fields to update organization document name</param>
        /// <returns></returns>
        [HttpPatch("{organizationId}/attachment/{keyName}/update")]
        public async Task<IActionResult> UpdateOrganizationDocument(Guid organizationId, string keyName,
            UpdateOrganizationAttachmentRequest request)
        {
            return await SafeExecuteAsync(
                async () => await _organizationService.UploadOrganizationAttachment(organizationId, keyName, request),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Resend organization confirmation e-mail
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("email/resend")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ResendEmail(ResendOrganizationConfirmationEmailRequest request)
        {
            return await SafeExecuteAsync(
                async () => await _organizationService.ResendOrganizationConfirmationEmail(request), HttpMethod.Patch);
        }

        /// <summary>
        ///     Delete organization attachment name
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="keyName"></param>
        /// <param name="request">Json containing fields to update organization document name</param>
        /// <returns></returns>
        [HttpDelete("{organizationId}/attachment/{keyName}/delete")]
        public async Task<IActionResult> DeleteOrganizationAttachment(Guid organizationId, string keyName,
            DeleteOrganizationAttachmentRequest request)
        {
            return await SafeExecuteAsync(
                async () => await _organizationService.DeleteOrganizationAttachment(organizationId, keyName, request),
                HttpMethod.Delete);
        }


        /// <summary>
        ///     Change status organization to active
        /// </summary>
        /// <param name="id">organization identifier</param>
        /// <returns></returns>
        [HttpPatch("{id}/activate/organization")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> ActivateOrganization(Guid id)
        {
            return await SafeExecuteAsync(async () => await _organizationService.ActivateOrganization(id),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Confirmation Organization Email after create organization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("email/confirmation")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> ConfirmationOrganizationEmail(ConfirmationOrganizationEmailRequest request)
        {
            return await SafeExecuteAsync(async () => await _organizationService.ConfirmationOrganizationEmail(request),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Resend approve organization email after approve organization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("email/resend-approve")]
        public async Task<IActionResult> ResendApproveOrganizationEmail(ResendOrganizationApproveEmailRequest request)
        {
            return await SafeExecuteAsync(
                async () => await _organizationService.ResendOrganizationApproveEmail(request), HttpMethod.Patch);
        }

        /// <summary>
        ///     Change organization status to disable
        /// </summary>
        /// <param name="id">Organization identifier</param>
        /// <returns></returns>
        [HttpPatch("{id}/disable")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> DisableOrganization(Guid id)
        {
            return await SafeExecuteAsync(async () => await _organizationService.DisableOrganization(id),
                HttpMethod.Patch);
        }
    }
}