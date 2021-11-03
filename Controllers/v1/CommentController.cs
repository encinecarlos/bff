using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Comments;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/comments")]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(
            ILogger<ComponentController> logger,
            IResponseService responseService,
            ICommentService commentService) : base(logger, responseService)
        {
            _commentService = commentService;
        }

        /// <summary>
        ///     Create Memorial Message
        /// </summary>
        /// <returns>Message</returns>
        [HttpPost("memorial")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostComment(CommentRequest command)
        {
            return await SafeExecuteAsync(async () => await _commentService.CreateMemorialComment(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Find comments by memorial
        /// </summary>
        /// <returns>List of memorial</returns>
        [HttpGet("memorial/{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindComment(Guid id)
        {
            return await SafeExecuteAsync(async () => await _commentService.FindMemorialComments(id), HttpMethod.Get);
        }

        /// <summary>
        ///     Create Quotation Note
        /// </summary>
        /// <returns>Message</returns>
        [HttpPost("quotation/note")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CreateQuotationNote(CommentRequest command)
        {
            return await SafeExecuteAsync(async () => await _commentService.CreateQuotationNote(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Create Quotation Message
        /// </summary>
        /// <returns>Message</returns>
        [HttpPost("quotation/message")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CreateQuotationMessage(CommentRequest command)
        {
            return await SafeExecuteAsync(async () => await _commentService.CreateQuotationMessage(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Create Quotation Message Reply
        /// </summary>
        /// <returns>Message</returns>
        [HttpPost("quotation/message/{id}/reply")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CreateQuotationReply(Guid id, CommentRequest command)
        {
            return await SafeExecuteAsync(async () => await _commentService.CreateQuotationReply(id, command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Find notes by quotation
        /// </summary>
        /// <returns>List of memorial</returns>
        [HttpGet("quotation/{id}/notes")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindQuotationNotes(Guid id)
        {
            return await SafeExecuteAsync(async () => await _commentService.FindQuotationNotes(id), HttpMethod.Get);
        }

        /// <summary>
        ///     Find notes by quotation
        /// </summary>
        /// <returns>List of memorial</returns>
        [HttpGet("quotation/{id}/messages")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindQuotationMessages(Guid id)
        {
            return await SafeExecuteAsync(async () => await _commentService.FindQuotationMessages(id), HttpMethod.Get);
        }

        /// <summary>
        ///     Delete Comment
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            return await SafeExecuteAsync(async () => await _commentService.DeleteComment(id), HttpMethod.Delete);
        }

        /// <summary>
        ///     Call To mark Notification as Read
        /// </summary>
        [HttpPatch("notification/{id}/as-read")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> ChangeNotificationAsRead(Guid id)
        {

            return await SafeExecuteAsync(async () => await _commentService.ChangeNotificationAsReadAsync(id), HttpMethod.Patch);
        }

        /// <summary>
        ///     Call To mark Notification as Unread
        /// </summary>
        [HttpPatch("notification/{id}/as-unread")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> ChangeNotificationAsUnRead(Guid id)
        {
            return await SafeExecuteAsync(async () => await _commentService.ChangeNotificationAsUnReadAsync(id), HttpMethod.Patch);
        }

        /// <summary>
        ///    List of Notifications by user logged
        /// </summary>
        [HttpPost("notification")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindNotifications(FindNotificationsRequest query)
        {
            return await SafeExecuteAsync(async () => await _commentService.FindNotificationsAsync(query), HttpMethod.Post);
        }


        /// <summary>
        ///     Call To mark Messages as Read
        /// </summary>
        [HttpPatch("message/read")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> ChangeMessageAsRead(ChangeMessageAsReadRequest command)
        {
            return await SafeExecuteAsync(async () => await _commentService.ChangeMessageAsReadAsync(command), HttpMethod.Patch);
        }

        /// <summary>
        ///     Call To mark Notes as Read
        /// </summary>
        [HttpPatch("notes/read")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> ChangeNotesAsRead(ChangeNotesAsReadRequest command)
        {
            return await SafeExecuteAsync(async () => await _commentService.ChangeNotesAsReadAsync(command), HttpMethod.Patch);
        }

        /// <summary>
        ///     Find all internal users accounts, distribution centers and roles
        /// </summary>
        /// <returns>List of internal users account</returns>
        [HttpGet("accounts-roles-dc")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindInternalUsersAccounts()
        {
            return await SafeExecuteAsync(async () => await _commentService.FindAccountsRolesDCsAsync(), HttpMethod.Get);
        }
    }
}