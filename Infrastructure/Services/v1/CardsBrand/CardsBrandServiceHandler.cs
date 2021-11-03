using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.CardsBrand
{
    public class CardsBrandServiceHandler : ICardsBrandService
    {
        private readonly IConfigurationClient _configurationClient;
        private readonly IResponseService _responseService;

        public CardsBrandServiceHandler(IConfigurationClient configurationClient, IResponseService responseService)
        {
            _configurationClient = configurationClient;
            _responseService = responseService;
        }

        public async Task<Response> FindAllCardsBrand()
        {
            return _responseService.CreateSuccessResponse(new
            {
                CardsBrandList = (await _configurationClient.FindAllCardsBrand()).Data
            });
        }

        public async Task<Response> GetCardsBrandById(Guid id) =>
            await _configurationClient.GetCardsBrandById(id);

        public async Task<Response> AddCardsBrand(CardsBrandRequest command) =>
            await _configurationClient.AddCardsBrand(command);

        public async Task<Response> DeleteCardsBrand(Guid id)
        {
            return await _configurationClient.DeleteCardsBrand(id);
        }

        public async Task<Response> UpdateCardsBrand(Guid id, CardsBrandRequest command) =>
            await _configurationClient.UpdateCardsBrand(id, command);


    }
}