using Refit;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Clients
{
    public interface IComponentClient
    {
        [Post("/api/v1/components/keys/search")]
        Task<Response> FindKeysByErpCode([Body] SystemComponentRequest request);

        [Get("/api/v1/datasheet/{erpCode}")]
        Task<Response> GetDataSheet(string erpCode);

        [Get("/api/v1/distribution-centers")]
        Task<Response> FindDistributionCenters();

        [Post("/api/v1/distribution-centers")]
        Task<Response> CreateDistributionCenter([Body] DistributionCenterRequest request);

        [Get("/api/v1/distribution-centers/{id}")]
        Task<Response> GetDistributionCenter(Guid id);

        [Put("/api/v1/distribution-centers/{id}")]
        Task<Response> UpdateDistributionCenter(Guid id, [Body] DistributionCenterRequest request);

        [Post("/api/v1/distribution-centers/search")]
        Task<Response> FindPagedDistributionCenters(FindPagedDistributionCentersDto request);

        [Delete("/api/v1/distribution-centers/{id}")]
        Task<Response> DeleteDistributionCenter(Guid id);

        [Get("/api/v1/inverters")]
        Task<Response> FindInverters();

        [Post("/api/v1/inverters")]
        Task<Response> CreateInverter([Body] InverterRequest request);

        [Get("/api/v1/inverters/{erpCode}")]
        Task<Response> GetInverter(string erpCode);

        [Put("/api/v1/inverters/{id}")]
        Task<Response> UpdateInverter(Guid id, [Body] InverterRequest request);

        [Delete("/api/v1/inverters/{id}")]
        Task<Response> DeleteInverter(Guid id);

        [Get("/api/v1/manufacturers")]
        Task<Response> FindManufacturers([Query] FindManufacturersRequest filters = null);

        [Get("/api/v1/components/erpcodes/search")]
        Task<Response> FindErpCodeComponents(Guid distributionCenterId, int tierId, bool automaticSystem);

        [Post("/api/v1/manufacturers/component-type/{componentType}")]
        Task<Response> FindManufacturerUsedOnComponents(int componentType, [Body] SystemComponentRequest request);

        [Get("/api/v1/manufacturers/group")]
        Task<Response> FindGroupedBuiltComponents();

        [Post("/api/v1/manufacturers")]
        Task<Response> CreateManufacturer([Body] ManufacturerRequest request);

        [Get("/api/v1/manufacturers/{id}")]
        Task<Response> GetManufacturer(Guid id);

        [Put("/api/v1/manufacturers/{id}")]
        Task<Response> UpdateManufacturer(Guid id, [Body] ManufacturerRequest request);

        [Delete("/api/v1/manufacturers/{id}")]
        Task<Response> DeleteManufacturer(Guid id);

        [Get("/api/v1/mlpes")]
        Task<Response> FindMlpes();

        [Post("/api/v1/mlpes")]
        Task<Response> CreateMlpe([Body] MlpeRequest request);

        [Get("/api/v1/mlpes/{erpCode}")]
        Task<Response> GetMlpe(string erpCode);

        [Put("/api/v1/mlpes/{id}")]
        Task<Response> UpdateMlpe(Guid id, [Body] MlpeRequest request);

        [Delete("/api/v1/mlpes/{id}")]
        Task<Response> DeleteMlpe(Guid id);

        [Get("/api/v1/modules")]
        Task<Response> FindModules();

        [Post("/api/v1/modules")]
        Task<Response> CreateModule([Body] ModuleRequest request);

        [Get("/api/v1/modules/{erpCode}")]
        Task<Response> GetModule(string erpCode);

        [Put("/api/v1/modules/{id}")]
        Task<Response> UpdateModule(Guid id, [Body] ModuleRequest request);

        [Delete("/api/v1/modules/{id}")]
        Task<Response> DeleteModule(Guid id);

        [Get("/api/v1/string-box")]
        Task<Response> FindStringBoxes();

        [Post("/api/v1/string-box")]
        Task<Response> CreateStringBox([Body] StringBoxRequest request);

        [Get("/api/v1/string-box/{erpCode}")]
        Task<Response> GetStringBox(string erpCode);

        [Put("/api/v1/string-box/{id}")]
        Task<Response> UpdateStringBox(Guid id, [Body] StringBoxRequest request);

        [Delete("/api/v1/string-box/{id}")]
        Task<Response> DeleteStringBox(Guid id);

        [Get("/api/v1/structures")]
        Task<Response> FindStructures();

        [Post("/api/v1/structures")]
        Task<Response> CreateStructure([Body] StructureRequest request);

        [Get("/api/v1/structures/{erpCode}")]
        Task<Response> GetStructure(string erpCode);

        [Put("/api/v1/structures/{id}")]
        Task<Response> UpdateStructure(Guid id, [Body] StructureRequest request);

        [Delete("/api/v1/structures/{id}")]
        Task<Response> DeleteStructure(Guid id);

        [Get("/api/v1/varieties")]
        Task<Response> FindVarieties();

        [Post("/api/v1/varieties")]
        Task<Response> CreateVariety([Body] VarietyRequest request);

        [Get("/api/v1/varieties/{erpCode}")]
        Task<Response> GetVariety(string erpCode);

        [Put("/api/v1/varieties/{id}")]
        Task<Response> UpdateVariety(Guid id, [Body] VarietyRequest request);

        [Delete("/api/v1/varieties/{id}")]
        Task<Response> DeleteVariety(Guid id);

        [Post("/api/v1/system-power/validate")]
        Task<Response> ValidateSystemPower([Body] ValidateComponentRequest request);

        [Get("/api/v1/components/tiers/{tierId}")]
        Task<Response> FindComponentsByTier(int tierId);

        [Get("/api/v1/components")]
        Task<Response> FindComponents();

        [Post("/api/v1/components/search")]
        Task<Response> FindComponentsBySearchTerm(ComponentListBySearchTermComponentRequest request);

        [Patch("/api/v1/components/{erpCode}")]
        Task<Response> UpdateComponentByErpCode(string erpCode, [Body] ComponentUpdateByErpCodeRequest request);

        [Get("/api/v1/components/{erpCode}")]
        Task<Response> FindComponentDetailByErpCode(string erpCode);

        [Get("/api/v1/distribution-centers/components")]
        Task<Response> FindDistributionCentersComponent();

        [Get("/api/v1/distribution-centers/validate")]
        Task<Response> GetDistributionCenterByStatusCode(string countryCode, string stateCode);

        [Get("/api/v1/distribution-centers/current-user")]
        Task<Response> FindDistributionCenterByCurrentUser();
    }
}