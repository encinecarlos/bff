using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Accounts
{
    public interface IAccountService
    {
        Task<Response> GetAccount(Guid id);
        Task<Response> FindAccounts();
        Task<Response> CreateAccount(AccountRequest request);
        Task<Response> CreateExternalAccount(AddNewExternalAccountRequest request);
        Task<Response> DeleteAccount(Guid id);
        Task<Response> DeleteExternalAccount(Guid id);
        Task<Response> UpdateAccount(Guid id, AccountUpdateRequest request);
        Task<Response> AddNewRolesToAccount(Guid id, RolesAccountRequest request);
        Task<Response> ChangePassword(Guid id, ChangePasswordAccountRequest request);
        Task<Response> FindExternalAccounts(FindExternalAccountsRequest request);
        Task<Response> RegisterNewLogin(LoginRequest request);
        Task<Response> RegisterNewLogout();
        Task<Response> FindRoles();
        Task<Response> GetRole(Guid id);
        Task<Response> FindRolesByUser(Guid id);
        Task<Response> CreateRole(RoleRequest request);
        Task<Response> UpdateRole(Guid id, RoleRequest request);
        Task<Response> DeleteRole(Guid id);
        Task<Response> ActiveUser(Guid id);
        Task<Response> DisableUser(Guid id);
        Task<Response> UpdateExternalAccount(Guid id, UpdateExternalAccountRequest request);
        Task<Response> ResendEmailExternalAccount(Guid id, ResendEmailExternalAccountRequest request);
        Task<Response> VerifyInternalAccountPermission(PermissionInternalAccountRequest request);
        Task<Response> RecoverPassword(RecoverPasswordRequest request);
        Task<Response> ChangePasswordExternalAccount(ChangePasswordExternalAccountRequest request);
        Task<Response> ChangePasswordWithVerificationCode(ChangePasswordWithVerificationCodeRequest request);
        Task<Response> EmailConfirmation(EmailConfirmationRequest request);
    }
}