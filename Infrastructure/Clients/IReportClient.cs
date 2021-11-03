using Refit;
using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Clients
{
    public interface IReportClient
    {
        [Post("/api/v1/reports/financial/csv")]
        Task<Response> CreateFinancialReport(CreateFinancialReportRequest command);

        [Post("/api/v1/reports/after-sales/csv")]
        Task<Response> CreateAfterSalesReport(CreateAfterSalesReportRequest command);

        [Post("/api/v1/reports/production/csv")]
        Task<Response> CreateProductionReport(CreateProductionReportRequest command);
    }
}