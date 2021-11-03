using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Accounts;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/accounts")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IResponseService responseService,
            IAccountService accountService) : base(logger, responseService)
        {
            _accountService = accountService;
        }

        /// <summary>
        ///     Get account by identifier
        /// </summary>
        /// <param name="id">account Identifier</param>
        /// <returns> account </returns>
        [HttpGet("/api/v1/internal/accounts/{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            return await SafeExecuteAsync(async () => await _accountService.GetAccount(id), HttpMethod.Get);
        }

        /// <summary>
        ///     Find All accounts by filters
        /// </summary>
        /// <returns>List of accounts</returns>
        [HttpGet("/api/v1/internal/accounts")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindAccount()
        {
            return await SafeExecuteAsync(async () => await _accountService.FindAccounts(), HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new account
        /// </summary>
        /// <param name="request">Json containing fields to create a new account</param>
        /// <returns>Token for authentication</returns>
        [HttpPost("/api/v1/internal/accounts")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostAccount(AccountRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.CreateAccount(request), HttpMethod.Post);
        }

        /// <summary>
        ///     Delete account by identifier
        /// </summary>
        /// <param name="id">account Identifier</param>
        /// <returns>Status Code 200 if successfully deleted</returns>
        [HttpDelete("/api/v1/internal/accounts/{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            return await SafeExecuteAsync(async () => await _accountService.DeleteAccount(id), HttpMethod.Delete);
        }

        /// <summary>
        ///     Update account by identifier
        /// </summary>
        /// <param name="id">account Identifier</param>
        /// <param name="request">Json containing the fields to create the account</param>
        /// <returns>account Updated</returns>
        [HttpPut("/api/v1/internal/accounts/{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutAccount(Guid id, AccountUpdateRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.UpdateAccount(id, request), HttpMethod.Put);
        }

        /// <summary>
        ///     Add new roles from account
        /// </summary>
        /// <param name="id">Account Identifier</param>
        /// <param name="request">Json containing new roles to be added </param>
        /// <returns></returns>
        [HttpPatch("{id}/roles")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> AddNewRolesToAccount(Guid id, RolesAccountRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.AddNewRolesToAccount(id, request),
                HttpMethod.Patch);
        }


        /// <summary>
        ///     Change password from account
        /// </summary>
        /// <param name="id">Account Identifier</param>
        /// <param name="request">Json containing new password</param>
        /// <returns></returns>
        [HttpPatch("{id}/password")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ChangePassword(Guid id, ChangePasswordAccountRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.ChangePassword(id, request),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Create a account
        /// </summary>
        /// <param name="request">Json containing fields to create a new account</param>
        [HttpPost("/api/v1/external/accounts")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostUser(AddNewExternalAccountRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.CreateExternalAccount(request),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Find all accounts by filters
        /// </summary>
        /// <returns>List of external accounts</returns>
        [HttpPost("/api/v1/external/accounts/search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> Find(FindExternalAccountsRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.FindExternalAccounts(request),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Delete a account
        /// </summary>
        /// <returns></returns>
        [HttpDelete("/api/v1/external/accounts/{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        public async Task<IActionResult> BatchDelete(Guid id)
        {
            return await SafeExecuteAsync(async () => await _accountService.DeleteExternalAccount(id),
                HttpMethod.Delete);
        }

        /// <summary>
        ///     Change status account to active
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("/api/v1/external/accounts/{id}/active")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ActiveUser(Guid id)
        {
            return await SafeExecuteAsync(async () => await _accountService.ActiveUser(id), HttpMethod.Patch);
        }

        /// <summary>
        ///     Change status account to disable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("/api/v1/external/accounts/{id}/disable")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DisableUser(Guid id)
        {
            return await SafeExecuteAsync(async () => await _accountService.DisableUser(id), HttpMethod.Patch);
        }

        /// <summary>
        ///     Update a external account
        /// </summary>
        /// <param name="id">user Identifier</param>
        /// <param name="request">Json containing fields to update a account</param>
        [HttpPut("/api/v1/external/accounts/{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateExternalAccountRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.UpdateExternalAccount(id, request),
                HttpMethod.Put);
        }

        /// <summary>
        ///     Resend confirmation email
        /// </summary>
        /// <param name="id">user identifier</param>
        /// <param name="request">Json containing fields to resend email</param>
        /// <returns></returns>
        [HttpPatch("/api/v1/external/accounts/{id}/email/resend")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ResendEmail(Guid id, ResendEmailExternalAccountRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.ResendEmailExternalAccount(id, request),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Checks if internal account has permission
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/v1/internal/accounts/permission")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> VerifyInternalAccountPermission(PermissionInternalAccountRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.VerifyInternalAccountPermission(request),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Recover password through e-mail
        /// </summary>
        /// <param name="request">Json containing e-mail</param>
        /// <returns></returns>
        [HttpPatch("/api/v1/external/accounts/recover-password")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.RecoverPassword(request), HttpMethod.Patch);
        }

        /// <summary>
        ///     Change password with verification code
        /// </summary>
        /// <param name="request">Json containing fields to change password</param>
        /// <returns></returns>
        [HttpPatch("/api/v1/external/accounts/password")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ChangePasswordWithVerificationCode(
            ChangePasswordWithVerificationCodeRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.ChangePasswordWithVerificationCode(request),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Change password of logged account
        /// </summary>
        /// <param name="request">Json containing fields to change password</param>
        /// <returns></returns>
        [HttpPatch("/api/v1/external/accounts/password-logged")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ChangePasswordExternalAccount(ChangePasswordExternalAccountRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.ChangePasswordExternalAccount(request),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Email confirmation account
        /// </summary>
        /// <param name="request">Json containing fields to confirm email</param>
        /// <returns></returns>
        [HttpPatch("/api/v1/external/accounts/confirmation")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> EmailConfirmatio(EmailConfirmationRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.EmailConfirmation(request),
                HttpMethod.Patch);
        }
    }
}