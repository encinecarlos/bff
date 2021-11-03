using POC.Shared.Configuration.Extensions;
using POC.Shared.Domain.Fixed;
using POC.Shared.Domain.ValueObjects;
using POC.Shared.Resources.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace POC.Bff.Web.Domain.Dtos
{
    public sealed class TierComponentsDto
    {
        public ComponentType ComponentType { get; set; }

        public string ImageKeyName { get; set; }

        public ErpCode ErpCode { get; set; }

        public Text Description { get; set; }

        public static object Create(List<TierComponentsDto> dto)
        {
            return dto
                .GroupBy(x => x.ComponentType)
                .Select(g =>
                {
                    return new
                    {
                        Id = g.Key,
                        Name = g.Key.GetDescription(),
                        ComponentOptionList = g.Select(c => new
                        {
                            Type = g.Key,
                            ImageUrl = c.ImageKeyName,
                            ErpCode = c.ErpCode.Value,
                            Model = ((Dictionary<string, string>)c.Description).GetByCultureLanguage(CultureExtensions
                                .DefaultCultureCodePortuguese)
                        })
                    };
                })
                .OrderBy(x => x.Id);
        }
    }
}