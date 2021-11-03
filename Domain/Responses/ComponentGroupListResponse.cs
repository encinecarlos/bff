using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class ComponentGroupListResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<ComponentOption> ComponentOptionList { get; set; }

        public class ComponentOption
        {
            public string ErpCode { get; set; }
            public long Type { get; set; }
            public string Model { get; set; }
            public string KeyName { get; set; }
            public string ImageKeyName { get; set; }
        }
    }
}