using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Manufacturers
{
    public interface IManufacturerService
    {
        Task<Response> GetManufacturer(Guid id);
        Task<Response> FindManufacturers(FindManufacturersRequest query);
        Task<Response> CreateManufacturer(ManufacturerRequest command);
        Task<Response> DeleteManufacturer(Guid id);
        Task<Response> UpdateManufacturer(Guid id, ManufacturerRequest command);
        Task<Response> FindGroupedBuiltComponents();
    }
}