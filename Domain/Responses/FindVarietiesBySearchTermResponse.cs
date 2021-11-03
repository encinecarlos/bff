namespace POC.Bff.Web.Domain.Responses
{
    public class FindVarietiesBySearchTermResponse : FindComponentsBySearchTermResponse
    {
        public string VarietyType { get; set; }
        public decimal Power { get; set; }
    }
}