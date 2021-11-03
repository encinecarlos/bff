using System.Collections.Generic;
using System.Linq;

namespace POC.Bff.Web.Domain.Responses
{
    public abstract class FindComponentsBySearchTermResponse
    {
        public string ErpCode { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string ImageKeyName { get; set; }
        public string ImageUrl { get; set; }
    }

    public static class FindComponentsBySearchTermResponseExtensions
    {
        public static void PopulateImageUrl<T>(this IList<T> obj, List<AttachmentResponse> attachments) where T : FindComponentsBySearchTermResponse
        {
            if (attachments is null || !attachments.Any()) return;

            obj.ToList().ForEach(item =>
            {
                item.ImageUrl = attachments.FirstOrDefault(x => x.KeyName.Equals(item.ImageKeyName))?.Url;
            });
        }
    }
}