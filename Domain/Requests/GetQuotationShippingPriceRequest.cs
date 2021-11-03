using System;

namespace POC.Bff.Web.Domain.Requests
{
    public sealed class GetQuotationShippingPriceRequest
    {
        public GetQuotationShippingPriceRequest(decimal? price, Guid? regionId, bool isManualPrice, AddressRequest selectedAddress)
        {
            Price = price;
            RegionId = regionId;
            IsManualPrice = isManualPrice;
            SelectedAddressDetail = selectedAddress;
        }

        public decimal? Price { get; set; }

        public Guid? RegionId { get; set; }

        public bool IsManualPrice { get; set; }

        public AddressRequest SelectedAddressDetail { get; set; }


    }
}