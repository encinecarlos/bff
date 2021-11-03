using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Reports
{
    public interface IReportService
    {
        Task<Response> CreateFinancialReport(CreateFinancialReportRequest command);
        Task<Response> CreateAfterSalesReport(CreateAfterSalesReportRequest command);
        Task<Response> CreateProductionReport(CreateProductionReportRequest command);
    }
}