using Refit;
using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Clients
{
    public interface IExpressClient
    {
        #region Products
        [Get("/api/v1/products/{id}")]
        Task<Response> GetProduct(string id);

        [Post("/api/v1/products/search")]
        Task<Response> FindProducts([Body] FindProductsRequest query);

        [Post("/api/v1/products")]
        Task<Response> PostProduct([Body] AddNewProductRequest command);

        [Delete("/api/v1/products/{id}")]
        Task<Response> DeleteProduct(string id);

        [Put("/api/v1/products/{id}")]
        Task<Response> PutProduct(string id, [Body] UpdateProductRequest command);
        #endregion

        #region Order
        [Get("/api/v1/orders/{orderId}")]
        Task<Response> GetOrder(Guid orderId);

        [Post("/api/v1/orders/search")]
        Task<Response> FindOrders(FindOrdersRequest request);

        [Post("/api/v1/orders")]
        Task<Response> AddNewOrder([Body] AddNewOrderRequest command);

        [Get("/api/v1/orders/orderNumber/{orderNumber}")]
        Task<Response> GetOrder(string orderNumber);

        [Put("/api/v1/orders/{orderId}")]
        Task<Response> CancelOrder(Guid orderId, [Body] UpdateOrderRequest command);
        #endregion

        #region CalculateInstallments
        [Post("/api/v1/calculate-installments")]
        Task<Response> CalculateInstallments(CalculateInstallmentsRequest request);
        #endregion

        #region ShoppingCart
        [Post("/api/v1/shopping-cart/products/price")]
        Task<Response> SumProductsPrice(ProductsPriceRequest command);

        [Post("/api/v1/shopping-cart/coupon/apply")]
        Task<Response> ApplyCouponCart(CouponProductsPriceRequest couponCart);
        #endregion
    }
}