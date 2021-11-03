using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Order
{
    public interface IOrderService
    {
        Task<Response> GetOrder(Guid orderId);
        Task<Response> FindOrders(FindOrdersRequest query);
        Task<Response> AddNewOrder(AddNewOrderRequest command);
        Task<Response> GetOrder(string orderNumber);
        Task<Response> CancelOrder(Guid orderId);
    }
}