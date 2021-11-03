namespace POC.Bff.Web.Domain.Dtos
{
    public class BatchChangeComponentMarkupDto : BatchBaseActionOfMemorialComponentDto
    {
        public decimal? Markup { get; set; }

        public decimal? PercentMarkup { get; set; }

    }
}