using Newtonsoft.Json;
using POC.Bff.Web.Infrastructure;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class SectionResponse
    {
        public int Type { get; set; }
        public List<ModuleResponse> Modules { get; set; }

        [JsonConverter(typeof(AttributeConverter))]
        public AttributeResponse Attributes { get; set; }
    }

    public class ModuleResponse
    {
        public int Type { get; set; }
        public List<int> Permissions { get; set; }
        public List<StatusPermissionResponse> StatusPermissions { get; set; }

        [JsonConverter(typeof(AttributeConverter))]
        public AttributeResponse Attributes { get; set; }
    }

    public class StatusPermissionResponse
    {
        public int Type { get; set; }
        public List<int> Permissions { get; set; }

        [JsonConverter(typeof(AttributeConverter))]
        public AttributeResponse Attributes { get; set; }
    }

    public class AttributeResponse : Dictionary<string, string>
    {
    }
}