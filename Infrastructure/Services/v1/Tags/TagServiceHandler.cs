using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Bff.Web.Infrastructure.Services.v1.Quotations;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Tags
{
    public class TagServiceHandler : ITagService
    {
        private readonly IConfigurationClient _configurationClient;
        private readonly IQuotationService _quotationService;
        private readonly IResponseService _responseService;

        public TagServiceHandler(
            IConfigurationClient configurationClient,
            IQuotationService quotationService,
            IResponseService responseService)
        {
            _configurationClient = configurationClient;
            _quotationService = quotationService;
            _responseService = responseService;
        }

        public async Task<Response> DeleteTag(Guid id)
        {
            var quotationsResult = await _quotationService.FindQuotationIdsFromTag(id);
            var quotationIds = quotationsResult.Parse<List<Guid>>();

            await _quotationService.BatchRemoveTagQuotation(id, new RemoveTagQuotationRequest(quotationIds));

            var response = await _configurationClient.DeleteTag(id);

            return _responseService.CreateSuccessResponse(response.Data).AddNotifications(response.Notifications);
        }
    }
}