namespace POC.Bff.Web.Domain.Responses
{
    public class ValidateSystemResponse
    {
        public decimal? SystemPrice { get; set; }
        public decimal PvSystemMarkup { get; set; }
        public decimal PvSystemCMV { get; set; }
        public decimal PvSystemTax { get; set; }
    }
}
