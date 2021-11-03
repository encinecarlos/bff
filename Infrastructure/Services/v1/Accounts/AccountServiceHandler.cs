using Microsoft.AspNetCore.Http;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Accounts
{
    public class AccountServiceHandler : IAccountService
    {
        private readonly ICommercialClient _commercialClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityClient _identityClient;
        private readonly IResponseService _responseService;

        public AccountServiceHandler(
            IIdentityClient identityClient,
            ICommercialClient commercialClient,
            IHttpContextAccessor httpContextAccessor,
            IResponseService responseService)
        {
            _identityClient = identityClient;
            _commercialClient = commercialClient;
            _httpContextAccessor = httpContextAccessor;
            _responseService = responseService;
        }


        public async Task<Response> GetAccount(Guid id)
        {
            return await _identityClient.GetAccount(id);
        }

        public async Task<Response> FindAccounts()
        {
            return await _identityClient.FindAccounts(new object());
        }

        public async Task<Response> CreateAccount(AccountRequest request)
        {
            return await _identityClient.CreateAccount(request);
        }

        public async Task<Response> CreateExternalAccount(AddNewExternalAccountRequest request)
        {
            var organization = await _commercialClient.GetLoggedUserOrganization();

            var organizationResponse = organization.Parse<OrganizationDto>();

            if (organization.Data == null) return _responseService.CreateFailResponse(request);

            request.Tier = organizationResponse?.Tier.Id;
            request.OrganizationId = organizationResponse?.Id;

            return await _identityClient.CreateExternalAccount(request);
        }

        public async Task<Response> DeleteAccount(Guid id)
        {
            return await _identityClient.DeleteAccount(id);
        }

        public async Task<Response> DeleteExternalAccount(Guid id)
        {
            return await _identityClient.DeleteExternalAccount(id);
        }

        public async Task<Response> UpdateAccount(Guid id, AccountUpdateRequest request)
        {
            return await _identityClient.UpdateAccount(id, request);
        }

        public async Task<Response> FindExternalAccounts(FindExternalAccountsRequest request)
        {
            return await _identityClient.FindExternalAccounts(request);
        }

        public async Task<Response> AddNewRolesToAccount(Guid id, RolesAccountRequest request)
        {
            return await _identityClient.AddNewRolesToAccount(id, request);
        }

        public async Task<Response> ChangePassword(Guid id, ChangePasswordAccountRequest request)
        {
            return await _identityClient.ChangePassword(id, request);
        }

        public async Task<Response> RegisterNewLogin(LoginRequest request)
        {
            var response = await _identityClient.RegisterNewLogin(request);

            var login = response.Parse<LoginResponse>();

            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out _))
                _httpContextAccessor.HttpContext.Request.Headers.Remove("Authorization");

            _httpContextAccessor.HttpContext.Request.Headers.Add("Authorization", "bearer " + login.Token);

            await _commercialClient.ValidateOrganization(new { login.IsOwner });

            return _responseService.CreateSuccessResponse(new
            {
                login.Email,
                login.IsInternalUser,
                login.Token,
                login.NotBefore,
                login.Expires
            });
        }

        public async Task<Response> RegisterNewLogout()
        {
            return await _identityClient.RegisterNewLogout();
        }

        public async Task<Response> FindRoles()
        {
            return await _identityClient.FindRoles();
        }
        public async Task<Response> GetRole(Guid id)
        {
            return await _identityClient.GetAccount(id);
        }

        public async Task<Response> FindRolesByUser(Guid id)
        {
            return await _identityClient.FindRolesByUser(id);
        }

        public async Task<Response> CreateRole(RoleRequest request)
        {
            return await _identityClient.CreateRole(request);
        }

        public async Task<Response> UpdateRole(Guid id, RoleRequest request)
        {
            return await _identityClient.UpdateRole(id, request);
        }

        public async Task<Response> DeleteRole(Guid id)
        {
            return await _identityClient.DeleteRole(id);
        }

        public async Task<Response> ActiveUser(Guid id)
        {
            return await _identityClient.ActiveUser(id);
        }

        public async Task<Response> DisableUser(Guid id)
        {
            return await _identityClient.DisableUser(id);
        }

        public async Task<Response> UpdateExternalAccount(Guid id, UpdateExternalAccountRequest request)
        {
            return await _identityClient.UpdateExternalAccount(id, request);
        }

        public async Task<Response> ResendEmailExternalAccount(Guid id, ResendEmailExternalAccountRequest request)
        {
            return await _identityClient.ResendEmailExternalAccount(id, request);
        }

        public async Task<Response> VerifyInternalAccountPermission(PermissionInternalAccountRequest request)
        {
            return await _identityClient.VerifyInternalAccountPermission(request);
        }

        public async Task<Response> RecoverPassword(RecoverPasswordRequest request)
        {
            return await _identityClient.RecoverPassword(request);
        }

        public async Task<Response> ChangePasswordExternalAccount(ChangePasswordExternalAccountRequest request)
        {
            return await _identityClient.ChangePasswordExternalAccount(request);
        }

        public async Task<Response> ChangePasswordWithVerificationCode(ChangePasswordWithVerificationCodeRequest request)
        {
            var response = await _identityClient.ChangePasswordWithVerificationCode(request);
            var loginResponse = response.Parse<LoginResponse>();
            if (loginResponse.IsOwner)
                await _commercialClient.ActiveOrganizationAfterChangePassword(new { request.Email });

            return _responseService.CreateSuccessResponse(loginResponse);
        }

        public async Task<Response> EmailConfirmation(EmailConfirmationRequest request)
        {
            return await _identityClient.EmailConfirmation(request);
        }
    }
}