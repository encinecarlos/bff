using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Manufacturers
{
    public class ManufacturerServiceHandler : IManufacturerService
    {
        private readonly IComponentClient _componentClient;

        public ManufacturerServiceHandler(IComponentClient componentClient)
        {
            _componentClient = componentClient;
        }

        public async Task<Response> GetManufacturer(Guid id)
        {
            return await _componentClient.GetManufacturer(id);
        }

        public async Task<Response> FindManufacturers(FindManufacturersRequest query)
        {
            return await _componentClient.FindManufacturers(query);
        }

        public async Task<Response> CreateManufacturer(ManufacturerRequest command)
        {
            return await _componentClient.CreateManufacturer(command);
        }

        public async Task<Response> DeleteManufacturer(Guid id)
        {
            return await _componentClient.DeleteManufacturer(id);
        }

        public async Task<Response> UpdateManufacturer(Guid id, ManufacturerRequest command)
        {
            return await _componentClient.UpdateManufacturer(id, command);
        }

        public async Task<Response> FindGroupedBuiltComponents()
        {
            return await _componentClient.FindGroupedBuiltComponents();
        }
    }
}