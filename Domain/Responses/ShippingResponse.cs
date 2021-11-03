using POC.Bff.Web.Domain.Fixed;
using System;

namespace POC.Bff.Web.Domain.Responses
{
    public class ShippingResponse
    {
        public ShippingType Type { get; set; }
        public object AddressType { get; set; }
        public object ShippingCompany { get; set; }
        public object OtherAddressDetail { get; set; }
        public AddressResponse SelectedAddressDetail { get; set; }
        public decimal? Price { get; set; }
        public string PickupAvailabilityAfterPayment { get; set; }
        public DateTime? PickupAvailabilityAt { get; set; }
        public DateTime? PickedUpAt { get; set; }
        public string DeliveryTimeAfterPickUp { get; set; }
        public DateTime? EstimateDeliveryAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public decimal PvSystemWeight { get; set; }
        public decimal PvSystemVolume { get; set; }
        public bool IsCompleted { get; set; }
        public Guid? RegionId { get; set; }
        public bool IsManualPrice { get; set; }
        public bool CanEdit { get; set; }
    }
}