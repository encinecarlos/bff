using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class PaymentConditionRequest
    {
        public string Name { get; set; }
        public int Status { get; set; }
        public bool IsFinancingCondition { get; set; }
        public bool IsFinancingActive { get; set; }
        public bool IsConsortiumActive { get; set; }
        public List<object> Installments { get; set; }
    }
}