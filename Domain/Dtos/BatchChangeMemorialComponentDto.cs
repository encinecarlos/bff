namespace POC.Bff.Web.Domain.Dtos
{
    public class BatchChangeMemorialComponentDto : BatchBaseActionOfMemorialComponentDto
    {
        public decimal? Cmv { get; set; }

        public decimal? PercentCmv { get; set; }
    }
}