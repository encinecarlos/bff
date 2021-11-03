using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Reports
{
    public class ReportServiceHandler : IReportService
    {
        private readonly IReportClient _reportClient;
        private readonly IResponseService _responseService;

        public ReportServiceHandler(IReportClient reportClient, IResponseService responseService)
        {
            _reportClient = reportClient;
            _responseService = responseService;
        }

        public async Task<Response> CreateAfterSalesReport(CreateAfterSalesReportRequest command)
        {
            var response = await _reportClient.CreateAfterSalesReport(command);
            return _responseService.CreateSuccessResponse(response.Data).AddNotifications(response.Notifications);
        }

        public async Task<Response> CreateFinancialReport(CreateFinancialReportRequest command)
        {
            var response = await _reportClient.CreateFinancialReport(command);
            return _responseService.CreateSuccessResponse(response.Data).AddNotifications(response.Notifications);
        }

        public async Task<Response> CreateProductionReport(CreateProductionReportRequest command)
        {
            var response = await _reportClient.CreateProductionReport(command);
            return _responseService.CreateSuccessResponse(response.Data).AddNotifications(response.Notifications);
        }
    }
}