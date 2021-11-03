using POC.Bff.Web.Domain.Responses;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class AddNewProductRequest : ProductRequest
    {
    }
    public class UpdateProductRequest : ProductRequest
    {
    }

    public class ProductRequest
    {
        public string ErpCode { get; set; }
        public string Description { get; set; }
        public decimal Power { get; set; }
        public decimal Price { get; set; }
        public int ProductType { get; set; }
        public int Position { get; set; }
        public int InStorageQuantity { get; set; }
        public ImageDetailRequest ImageDetail { get; set; }
        public List<Guid> DistributionCenters { get; set; }
        public List<object> Components { get; set; }
        public int ProductStatusType { get; set; }
        public AccountResponse AccountResponse { get; set; }

    }

    public class ImageDetailRequest
    {
        public string KeyName { get; set; }
        public string FileName { get; set; }
        public string FileBase64 { get; set; }
        public int FileSize { get; set; }
    }
}