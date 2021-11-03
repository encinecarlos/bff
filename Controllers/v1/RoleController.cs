//using System.Net;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
//using Microsoft.Extensions.Logging;
//using Sices.Bff.Web.Infrastructure.Clients;
//using Sices.Bff.Web.Models;
//using Sices.Bff.Web.Services.v1.Accounts;
//using Sices.Bff.Web.Swagger;
//using Swashbuckle.AspNetCore.Examples;

//namespace Sices.Bff.Web.Controllers.v1
//{
//    [Route("api/v1/roles")]
//    public class RoleController : BaseController
//    {
//        private readonly IAccountService _accountService;

//        public RoleController(ILogger<RoleController> logger, IAccountService accountService) : base(logger)
//        {
//            _accountService = accountService;
//        }

//        /// <summary>
//        ///     Get all roles
//        /// </summary>
//        /// <returns>Roles</returns>
//        [HttpGet]
//        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
//        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
//        public async Task<IActionResult> FindRoles()
//        {
//            return await SafeExecuteAsync(async () => await _accountService.FindRoles(), HttpMethod.Get);
//        }

//        /// <summary>
//        ///     Get Roles by identifier
//        /// </summary>
//        /// <param name="id">Role identifier ID</param>
//        /// <returns>Role model</returns>
//        [HttpGet("{id}")]
//        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
//        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
//        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
//        public async Task<IActionResult> GetRole(string id)
//        {
//            return await SafeExecuteAsync(async () => await _accountService.GetRole(id), HttpMethod.Get);
//        }

//        /// <summary>
//        ///     Get Roles by identifier account
//        /// </summary>
//        /// <param name="id">Account Id</param>
//        /// <returns>Roles Account</returns>
//        [HttpGet("{id}/account")]
//        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
//        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
//        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
//        public async Task<IActionResult> FindRolesByUser(string id)
//        {
//            return await SafeExecuteAsync(async () => await _accountService.FindRolesByUser(id), HttpMethod.Get);
//        }

//        /// <summary>
//        ///     Create a new role from system.
//        /// </summary>
//        /// <param name="request">Json containing new role</param>
//        /// <returns></returns>
//        [HttpPost]
//        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
//        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
//        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
//        public async Task<IActionResult> PostRole(RoleRequest request)
//        {
//            return await SafeExecuteAsync(async () => await _accountService.CreateRole(request), HttpMethod.Post);
//        }

//        /// <summary>
//        ///     Update role identified by id
//        /// </summary>
//        /// <param name="id">Role identifier</param>
//        /// <param name="request">Json containing the fields to create the Role</param>
//        /// <returns>Role updated</returns>
//        [HttpPut("{id}")]
//        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
//        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
//        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
//        public async Task<IActionResult> PutRole(string id, RoleRequest request)
//        {
//            return await SafeExecuteAsync(async () => await _accountService.UpdateRole(id, request), HttpMethod.Put);
//        }


//        /// <summary>
//        ///     Delete Role by identifier
//        /// </summary>
//        /// <param name="id">Role Identifier</param>
//        /// <returns>Status Code 200 if successfully deleted</returns>
//        [HttpDelete("{id}")]
//        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
//        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
//        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
//        public async Task<IActionResult> DeleteRole(string id)
//        {
//            return await SafeExecuteAsync(async () => await _accountService.DeleteRole(id), HttpMethod.Delete);
//        }
//    }
//}