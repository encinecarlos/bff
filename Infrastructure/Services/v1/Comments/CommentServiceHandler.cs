using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Fixed;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Comments
{
    public class CommentServiceHandler : ICommentService
    {
        private readonly ICommercialClient _commercialClient;
        private readonly IIdentityClient _identityClient;
        private readonly IComponentClient _componentClient;
        private readonly IMapper _mapper;
        private readonly IResponseService _responseService;

        public CommentServiceHandler(
            ICommercialClient commercialClient,
            IIdentityClient identityClient,
            IComponentClient componentClient,
            IMapper mapper,
            IResponseService responseService)
        {
            _commercialClient = commercialClient;
            _identityClient = identityClient;
            _componentClient = componentClient;
            _mapper = mapper;
            _responseService = responseService;
        }

        public async Task<Response> CreateMemorialComment(CommentRequest command)
        {
            return await _commercialClient.CreateMemorialComment(command);
        }

        public async Task<Response> FindMemorialComments(Guid id)
        {
            return await _commercialClient.FindMemorialComments(id);
        }

        public async Task<Response> CreateQuotationNote(CommentRequest command)
        {

            var request = _mapper.Map<CommentDto>(command);
            var users = await _identityClient.FindInternalAccountWithMention(new { sicesUsers = command.SicesUsersList, role = command.RolesList });
            var responseIdentity = users.Parse<List<InternalAccountMentionResponse>>();
            request.Accounts = responseIdentity;

            if (command.RolesList != null && command.RolesList.Any())
            {
                var accounts = await _commercialClient.ValidateOrganizationDistributionCenter(new { accounts = responseIdentity, distributionCenter = command.CdList });
                var responseCommercial = accounts.Parse<List<InternalAccountMentionResponse>>();
                request.Accounts = responseCommercial;
            }

            var responseComment = await _commercialClient.CreateQuotationNote(request);

            if (request.Accounts.Any())
                await _commercialClient.AddNewNoteNotification(new { request.Accounts, request.ReferenceId });

            return responseComment;
        }

        public async Task<Response> CreateQuotationMessage(CommentRequest command)
        {
            var request = _mapper.Map<CommentDto>(command);
            request.AccountsNotification = new Dictionary<Guid, bool>();
            var response = await _commercialClient.ValidateCommentMessage(command);
            var validateCommentMessageResponse = response.Parse<ValidateCommentMessageResponse>();
            var isAccountManager = validateCommentMessageResponse.AccountManager != Guid.Empty;
            if (validateCommentMessageResponse.InternalUser)
            {
                if (validateCommentMessageResponse.CreatedBy != Guid.Empty)
                    request.AccountsNotification.TryAdd(validateCommentMessageResponse.CreatedBy, isAccountManager);

                var ownerResponse = await _identityClient.GetOwnerByOrganizationId(validateCommentMessageResponse.Organization);
                var owner = ownerResponse.Parse<Guid>();

                if (owner != Guid.Empty)
                    request.AccountsNotification.TryAdd(owner, isAccountManager);
            }

            if (isAccountManager)
                request.AccountsNotification.TryAdd(validateCommentMessageResponse.AccountManager, isAccountManager);

            var responseComment = await _commercialClient.CreateQuotationMessage(request);

            if (request.AccountsNotification.Any())
                await _commercialClient.AddNewMessageNotification(new { accounts = request.AccountsNotification, request.ReferenceId });

            return responseComment;
        }

        public async Task<Response> CreateQuotationReply(Guid id, CommentRequest command)
        {
            var request = _mapper.Map<CommentDto>(command);
            request.ParentId = id;

            var commentTypeResponse = await _commercialClient.GetCommentType(request.ParentId);
            var commentType = commentTypeResponse.Parse<CommentType>();

            if (commentType == CommentType.Note)
            {
                var users = await _identityClient.FindInternalAccountWithMention(new { sicesUsers = command.SicesUsersList, role = command.RolesList });
                var responseIdentity = users.Parse<List<InternalAccountMentionResponse>>();
                request.Accounts = responseIdentity;

                if (command.RolesList != null && command.RolesList.Any())
                {
                    var accounts = await _commercialClient.ValidateOrganizationDistributionCenter(new { accounts = responseIdentity, distributionCenter = command.CdList });
                    var responseCommercial = accounts.Parse<List<InternalAccountMentionResponse>>();
                    request.Accounts = responseCommercial;
                }

                var commentResponse = await _commercialClient.CreateQuotationReply(id, request);

                if (request.Accounts.Any())
                    await _commercialClient.AddNewNoteNotification(new { request.Accounts, request.ReferenceId });

                return commentResponse;
            }
            else
            {
                request.AccountsNotification = new Dictionary<Guid, bool>();
                var response = await _commercialClient.ValidateCommentMessage(request);
                var validateCommentMessageResponse = response.Parse<ValidateCommentMessageResponse>();
                var isAccountManager = validateCommentMessageResponse.AccountManager != Guid.Empty;
                if (validateCommentMessageResponse.InternalUser)
                {
                    if (validateCommentMessageResponse.CreatedBy != Guid.Empty)
                        request.AccountsNotification.TryAdd(validateCommentMessageResponse.AccountManager, isAccountManager);

                    var ownerResponse = await _identityClient.GetOwnerByOrganizationId(validateCommentMessageResponse.Organization);
                    var owner = ownerResponse.Parse<Guid>();

                    if (owner != Guid.Empty)
                        request.AccountsNotification.TryAdd(validateCommentMessageResponse.AccountManager, isAccountManager);
                }

                if (isAccountManager)
                    request.AccountsNotification.TryAdd(validateCommentMessageResponse.AccountManager, isAccountManager);

                var commentResponse = await _commercialClient.CreateQuotationReply(id, request);

                if (request.AccountsNotification.Any())
                    await _commercialClient.AddNewMessageNotification(new { accounts = request.AccountsNotification, request.ReferenceId });

                return commentResponse;
            }
        }

        public async Task<Response> FindQuotationNotes(Guid id)
        {
            return await _commercialClient.GetQuotationNotes(id);
        }

        public async Task<Response> FindQuotationMessages(Guid id)
        {
            return await _commercialClient.GetQuotationMessages(id);
        }

        public async Task<Response> DeleteComment(Guid id)
        {
            return await _commercialClient.DeleteComment(id);
        }

        public async Task<Response> ChangeNotificationAsReadAsync(Guid id)
        {
            var request = new ChangeNotificationAsReadRequest { Id = id, AsRead = true };
            return await _commercialClient.ChangeNotificationAsRead(id, request);
        }
        public async Task<Response> ChangeNotificationAsUnReadAsync(Guid id)
        {
            var request = new ChangeNotificationAsReadRequest { Id = id, AsRead = false };
            return await _commercialClient.ChangeNotificationAsRead(id, request);
        }
        public async Task<Response> FindNotificationsAsync(FindNotificationsRequest request)
        {
            var notifications = await _commercialClient.FindNotifications(request);

            if (notifications.Data is null)
            {
                return _responseService.CreateSuccessResponse(notifications.Data).AddNotifications(notifications.Notifications);
            }

            var notificationsPagedResponse = notifications.Parse<PagedResponse<NotificationsPagedResponse>>();
            var notificationsList = _mapper.Map<List<PagedNotificationsDto>>(notificationsPagedResponse.List);

            var response = new { UnreadNotificationCount = notificationsPagedResponse.Total, List = notificationsList };
            return _responseService.CreateSuccessResponse(response).AddNotifications(notifications.Notifications);
        }

        public async Task<Response> ChangeMessageAsReadAsync(ChangeMessageAsReadRequest command)
        {
            var request = _mapper.Map<ChangeMessageAsReadDto>(command);

            return await _commercialClient.ChangeMessageAsRead(request);
        }

        public async Task<Response> ChangeNotesAsReadAsync(ChangeNotesAsReadRequest command)
        {
            var request = _mapper.Map<ChangeNotesAsReadDto>(command);

            return await _commercialClient.ChangeNotesAsRead(request);
        }

        public async Task<Response> FindAccountsRolesDCsAsync()
        {
            var internalUsersResponse = _identityClient.FindInternalUsersAccounts();

            var roles = _identityClient.FindRoles();

            var distributionCentersResponse = _componentClient.FindDistributionCenterByCurrentUser();

            await Task.WhenAll(internalUsersResponse, roles, distributionCentersResponse);

            var internalUsers = internalUsersResponse.Result.Parse<List<AccountResponse>>();
            var internalUsersList = internalUsers.Select(x => _mapper.Map<GenericResponseDto>(x));

            var cd = distributionCentersResponse.Result.Parse<List<DistributionCenterDto>>();
            var distributionCenterList = cd.Select(x => _mapper.Map<GenericResponseDto>(x));

            return _responseService.CreateSuccessResponse(new
            {
                sicesUsersList = internalUsersList,
                roleList = roles.Result.Parse<List<RolesResponse>>(),
                cdList = distributionCenterList
            });
        }
    }
}