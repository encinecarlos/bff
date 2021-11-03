using Newtonsoft.Json;
using POC.Bff.Web.Domain.Fixed;
using POC.Bff.Web.Domain.Responses;
using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class SaveShippingQuotationRequest
    {
        public int Type { get; set; }

        public int AddressType { get; set; }

        public object ShippingCompany { get; set; }

        public object OtherAddressDetail { get; set; }

        public decimal? Price { get; set; }

        public string PickupAvailabilityAfterPayment { get; set; }

        public string DeliveryTimeAfterPickUp { get; set; }

        public AddressResponse SelectedAddressDetail { get; set; }

        public decimal PvSystemWeight { get; set; }

        public decimal PvSystemVolume { get; set; }

        public DateTime? PickupAvailabilityAt { get; set; }

        public LoyaltyPointsConfigurationRequest LoyaltyPointsConfiguration { get; set; }

        public Guid? RegionId { get; set; }

        public bool IsManualPrice { get; set; }

        [JsonIgnore]
        public bool IsValid =>
            SelectedAddressDetail != null
            && Enum.IsDefined(typeof(ShippingType), Type)
            && Enum.IsDefined(typeof(ShippingAddressType), AddressType);
    }
}