using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class ValidateComponentRequest
    {
        public Guid QuotationId { get; set; }
        public FdiRequest Fdi { get; set; }
        public int Quantity { get; set; }
        public decimal Power { get; set; }
        public List<ComponentValuesRequest> ComponentList { get; set; }
        public Guid? PromotionId { get; set; }
        public Guid? VoltageId { get; set; }
        public VoltageRequest Voltage { get; set; }

    }
}