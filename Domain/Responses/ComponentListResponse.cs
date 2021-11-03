using POC.Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class ComponentListResponse
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
            public decimal Power { get; set; }
            public decimal Price { get; set; }
            public List<ComponentPrice> Prices { get; set; }

            public class ComponentPrice
            {
                private decimal _value;
                public Guid PowerId { get; set; }
                public Power Power { get; set; }
                public decimal Markup { get; set; }

                public decimal Value
                {
                    get => _value;
                    set => _value = Math.Round(value, 2);
                }
            }
        }
    }
}