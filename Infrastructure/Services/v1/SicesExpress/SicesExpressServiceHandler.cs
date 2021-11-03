using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.SicesExpress
{
    public sealed class SicesExpressServiceHandler : ISicesExpressService
    {
        private readonly IConfigurationClient _configurationClient;
        private readonly IResponseService _responseService;
        public SicesExpressServiceHandler(IConfigurationClient configurationClient, IResponseService responseService)
        {
            _configurationClient = configurationClient;
            _responseService = responseService;
        }

        public async Task<Response> GetAllParametersSicesExpress()
        {
            return _responseService.CreateSuccessResponse(new
            {
                SicesExpressParameters = (await _configurationClient.GetAllParametersSicesExpress()).Data
            });
        }

        public async Task<Response> UpdateSicesExpressParameter(Guid id, UpdateSicesExpressParameterRequest command)
        {
            return await _configurationClient.PutSicesExpressParameters(id, command);
        }
    }
}