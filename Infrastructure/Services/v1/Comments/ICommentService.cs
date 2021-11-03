using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Comments
{
    public interface ICommentService
    {
        Task<Response> CreateMemorialComment(CommentRequest command);

        Task<Response> FindMemorialComments(Guid id);

        Task<Response> CreateQuotationNote(CommentRequest command);

        Task<Response> CreateQuotationMessage(CommentRequest command);

        Task<Response> CreateQuotationReply(Guid id, CommentRequest command);

        Task<Response> FindQuotationNotes(Guid id);

        Task<Response> FindQuotationMessages(Guid id);

        Task<Response> DeleteComment(Guid id);
        Task<Response> ChangeNotificationAsUnReadAsync(Guid id);
        Task<Response> ChangeNotificationAsReadAsync(Guid id);
        Task<Response> FindNotificationsAsync(FindNotificationsRequest query);
        Task<Response> ChangeMessageAsReadAsync(ChangeMessageAsReadRequest command);
        Task<Response> ChangeNotesAsReadAsync(ChangeNotesAsReadRequest command);

        Task<Response> FindAccountsRolesDCsAsync();
    }
}