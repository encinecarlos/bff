using POC.Shared.Domain.Fixed;
using POC.Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class ProductResponse
    {
        public bool Enabled { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ErpCode { get; set; }
        public string Description { get; set; }
        public int ProductType { get; set; }
        public decimal Power { get; set; }
        public decimal Price { get; set; }
        public int InStorageQuantity { get; set; }
        public int Reserved { get; set; }
        public ImageDetailResponse ImageDetail { get; set; }
        public int Position { get; set; }
        public List<Component> Components { get; set; }
        public List<Guid> DistributionCenters { get; set; }
        public int ProductStatusType { get; set; }
        public string Currency { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
    }

    public class ImageDetailResponse
    {
        public string KeyName { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string Url { get; set; }
        public DateTime UploadedAt { get; set; }
    }

    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ComponentOptionList> ComponentOptionList { get; set; }
    }

    public class ComponentOptionList
    {
        public ErpCode ErpCode { get; set; }
        public string Model { get; set; }
        public ComponentType Type { get; set; }
        public int Quantity { get; set; }
        public int Position { get; set; }
        public string ManufacturerName { get; set; }
    }
}