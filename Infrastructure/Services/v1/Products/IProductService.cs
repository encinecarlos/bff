using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Products
{
    public interface IProductService
    {
        Task<Response> GetProduct(string id);
        Task<Response> FindProducts(FindProductsRequest query);
        Task<Response> CreateProduct(AddNewProductRequest command);
        Task<Response> DeleteProduct(string id);
        Task<Response> UpdateProduct(string id, UpdateProductRequest command);
        Task<Response> GetProductDetailConfig();
    }
}