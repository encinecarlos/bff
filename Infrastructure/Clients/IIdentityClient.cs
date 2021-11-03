using Refit;
using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Clients
{
    public interface IIdentityClient
    {
        [Get("/api/v1/roles")]
        Task<Response> FindRoles();

        [Post("/api/v1/roles")]
        Task<Response> CreateRole([Body] RoleRequest request);

        [Get("/api/v1/roles/{id}")]
        Task<Response> GetRole(Guid id);

        [Get("/api/v1/roles/{id}/accounts")]
        Task<Response> FindRolesByUser(Guid id);

        [Put("/api/v1/roles/{id}")]
        Task<Response> UpdateRole(Guid id, [Body] RoleRequest request);

        [Delete("/api/v1/roles/{id}")]
        Task<Response> DeleteRole(Guid id);

        [Get("/api/v1/internal/accounts")]
        Task<Response> FindAccounts([Body] object request);

        [Get("/api/v1/internal/accounts/{id}")]
        Task<Response> GetAccount(Guid id);

        [Post("/api/v1/internal/accounts")]
        Task<Response> CreateAccount([Body] AccountRequest request);

        [Post("/api/v1/external/accounts")]
        Task<Response> CreateExternalAccount([Body] AddNewExternalAccountRequest request);

        [Delete("/api/v1/internal/accounts/{id}")]
        Task<Response> DeleteAccount(Guid id);

        [Delete("/api/v1/external/accounts/{id}")]
        Task<Response> DeleteExternalAccount(Guid id);

        [Post("/api/v1/external/accounts/search")]
        Task<Response> FindExternalAccounts([Body] FindExternalAccountsRequest request);

        [Put("/api/v1/internal/accounts/{id}")]
        Task<Response> UpdateAccount(Guid id, [Body] AccountUpdateRequest request);

        [Patch("/api/v1/accounts/{id}/roles")]
        Task<Response> AddNewRolesToAccount(Guid id, [Body] RolesAccountRequest request);

        [Get("/api/v1/organizations/{id}/accounts")]
        Task<Response> GetAccountByOrganizationId(Guid id);

        [Post("/api/v1/internal/accounts/permission")]
        Task<Response> VerifyInternalAccountPermission([Body] PermissionInternalAccountRequest request);

        [Patch("/api/v1/accounts/{id}/password")]
        Task<Response> ChangePassword(Guid id, [Body] ChangePasswordAccountRequest request);

        [Post("/api/v1/login")]
        Task<Response> RegisterNewLogin([Body] LoginRequest request);

        [Post("/api/v1/logout")]
        Task<Response> RegisterNewLogout();

        [Get("/api/v1/authorization")]
        Task<Response> GetAuthorizationDetail();

        [Patch("/api/v1/external/accounts/{id}/active")]
        Task<Response> ActiveUser(Guid id);

        [Patch("/api/v1/external/accounts/{id}/disable")]
        Task<Response> DisableUser(Guid id);

        [Put("/api/v1/external/accounts/{id}")]
        Task<Response> UpdateExternalAccount(Guid id, [Body] UpdateExternalAccountRequest request);

        [Patch("/api/v1/external/accounts/{id}/resend-email")]
        Task<Response> ResendEmailExternalAccount(Guid id, [Body] ResendEmailExternalAccountRequest request);

        [Patch("/api/v1/external/accounts/recover-password")]
        Task<Response> RecoverPassword([Body] RecoverPasswordRequest request);

        [Patch("/api/v1/external/accounts/password")]
        Task<Response> ChangePasswordWithVerificationCode([Body] ChangePasswordWithVerificationCodeRequest request);

        [Post("/api/v1/internal/accounts/validate")]
        Task<Response> ValidateInternalAccount(ValidateInternalAccountRequest request);

        [Post("/api/v1/external/account/organization")]
        Task<Response> CreateOwnerExternalAccount([Body] AddNewOwnerExternalAccountRequest externalAccount);

        [Patch("/api/v1/external/account/organization")]
        Task<Response> UpdateOwnerExternalAccount([Body] UpdateExternalAccountByOrganizationRequest externalAccount);

        [Patch("/api/v1/external/accounts/password-logged")]
        Task<Response> ChangePasswordExternalAccount([Body] ChangePasswordExternalAccountRequest request);

        [Patch("/api/v1/external/accounts/confirmation")]
        Task<Response> EmailConfirmation([Body] EmailConfirmationRequest request);

        [Post("/api/v1/external/accounts/wait")]
        Task<Response> WaitApproveAccount([Body] object request);

        [Patch("/api/v1/external/accounts/organizations/email/confirmation-owner")]
        Task<Response> ConfirmationEmailOwner([Body] object request);

        [Patch("/api/v1/external/accounts/disable/organization/{id}")]
        Task<Response> DisableOrganizationUsers(Guid id);

        [Get("/api/v1/accounts/validate/logged")]
        Task<Response> ValidateUserIsLogged();

        [Get("/api/v1/external/accounts/{email}/flags")]
        Task<Response> GetExternalAccountFlagsByEmail(string email);

        [Get("/api/v1/internal/accounts-user")]
        Task<Response> FindInternalUsersAccounts();

        [Post("/api/v1/internal/accounts/mention")]
        Task<Response> FindInternalAccountWithMention([Body] object request);

        [Get("/api/v1/external/organization/{id}/owner")]
        Task<Response> GetOwnerByOrganizationId(Guid id);
    }
}