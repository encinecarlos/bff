namespace POC.Bff.Web.Domain.Requests
{
    public class UpdateTagRequest
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string TextColor { get; set; }
        public string BackgroundColor { get; set; }
    }
}