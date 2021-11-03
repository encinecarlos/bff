namespace POC.Bff.Web.Domain.Requests
{
    public sealed class FeatureRequest
    {
        public string Description { get; set; }
        public bool LinkEnabled { get; set; }
        public string LinkText { get; set; }
        public string LinkUrl { get; set; }
    }
}