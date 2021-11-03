namespace POC.Bff.Web.Domain.Requests
{
    public class VarietyRequest : ComponentBaseRequest
    {
        public int Type { get; set; }

        public decimal Power { get; set; }
    }
}