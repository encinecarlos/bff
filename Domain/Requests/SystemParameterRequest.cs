using POC.Shared.Domain.Fixed;
using POC.Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class SystemParameterRequest
    {

        public decimal SystemPower { get; set; }
        public int OrganizationTier { get; set; }
        public Guid OrganizationDistributionCenterId { get; set; }
        public ErpCode ModuleErpCode { get; set; }
        public Guid InverterManufacturerId { get; set; }
        public Guid StringBoxManufacturerId { get; set; }
        public Guid StructureManufacturerId { get; set; }
        public Guid StructureVarietyId { get; set; }
        public Guid VoltageId { get; set; }
        public PhaseType Phase { get; set; }
        public bool AddTransformer { get; set; }
        public bool AddMlpe { get; set; }
        public ErpCode MlpeErpCode { get; set; }
        public List<object> StructureParameters { get; set; }
        public Guid? PromotionId { get; set; }
        public decimal? MaxTemperature { get; set; }
        public decimal? MinTemperature { get; set; }
    }
}