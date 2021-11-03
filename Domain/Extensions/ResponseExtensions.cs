using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Responses;
using POC.Shared.Configuration.Extensions;
using POC.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Bff.Web.Domain.Extensions
{
    public static class ResponseExtensions
    {
        public static T Parse<T>(this Response obj)
        {
            return obj?.Data is null ? default : obj.Data.ParseRefitResponseJson<T>();
        }

        public static IEnumerable<object> Parse<T>(this Task<Response> response, Func<T, object> selector)
        {
            return response.Result.Parse<List<T>>().Select(selector);
        }

        public static IEnumerable<object> ParseInsurances(this Task<Response> insurances)
        {
            return insurances.Parse<InsuranceDto>(x => new
            {
                x.Id,
                x.Title
            });
        }

        public static IEnumerable<object> ParseDistributionCenters(this Task<Response> distributionCenters)
        {
            return distributionCenters.Parse<DistributionCenterDto>(x => new
            {
                x.Id,
                Name = x.Description
            });
        }

        public static List<string> GetKeyNames(this IList<FindTiersResponse> obj)
        {
            return obj
                .Where(x => x.Image != null)
                .Select(x => x.Image?.KeyName)
                .ToList();
        }

        public static List<FindTiersResponse> GetWithoutImage(this IList<FindTiersResponse> obj)
        {
            return obj
                .Where(x => x.Image is null)
                .ToList();
        }

        public static List<TierDto> MergeAttachment(this IList<FindTiersResponse> obj, IList<AttachmentResponse> attachments)
        {
            var tiers = new List<TierDto>(
                attachments
                    .Join(
                        obj, a => a.KeyName, t => t.Image?.KeyName,
                        (attachmentResponse, tierResponse) => new TierDto
                        {
                            Id = tierResponse.Id,
                            Name = tierResponse.Description,
                            FileName = tierResponse.Image.FileName,
                            KeyName = tierResponse.Image.KeyName,
                            ImageUrl = attachmentResponse.Url,
                            Color = tierResponse.Color
                        })
                    .Concat(obj
                        .GetWithoutImage()
                        .Select(x => new TierDto
                        {
                            Id = x.Id,
                            Name = x.Description,
                            Color = x.Color
                        }))
            );

            return tiers;
        }
    }
}