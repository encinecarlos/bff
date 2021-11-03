namespace POC.Bff.Web.Domain.Responses
{
    public class FindStringBoxesBySearchTermResponse : FindComponentsBySearchTermResponse
    {
        public int InputNumber { get; set; }
        public int OutputNumber { get; set; }
        public int FuseNumber { get; set; }
    }
}