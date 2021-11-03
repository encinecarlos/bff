namespace POC.Bff.Web.Domain.Responses
{
    public class FindStructuresBySearchTermResponse : FindComponentsBySearchTermResponse
    {
        public string SubType { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
    }
}