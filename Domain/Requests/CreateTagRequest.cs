using POC.Shared.Domain.Models;

namespace POC.Bff.Web.Domain.Requests
{
    public class CreateTagRequest : Request
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string TextColor { get; set; }
        public string BackgroundColor { get; set; }
    }
}