namespace POC.Bff.Web.Domain.Requests
{
    public class AddPercentRangeToRuleRequest
    {
        public decimal Percent { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}