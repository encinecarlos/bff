using System.ComponentModel;

namespace POC.Bff.Web.Domain.Fixed
{
    public enum QuotationStatusType
    {
        [Description("CREATED")]
        Created = 0,

        [Description("EDITION")]
        Edition = 1,

        [Description("IN_VALIDATION")]
        InValidation = 2,

        [Description("WAITING_APPROVAL")]
        WaitingApproval = 3,

        [Description("WAITING_PAYMENT")]
        WaitingPayment = 4,

        [Description("CANCELED")]
        Canceled = 5,

        [Description("WAITING_CONFIRMATION")]
        WaitingConfirmation = 6,

        [Description("CONFIRMED")]
        Confirmed = 7,

        [Description("CONFIRMED_RESERVED")]
        ConfirmedRsvd = 8,

        [Description("IN_PRODUCTION")]
        InProduction = 9,

        [Description("IN_PRODUCTION_RESERVED")]
        InProdRsvd = 10,

        [Description("IN_PRODUCTION_CONFIRMED")]
        InProductionConfirmed = 11,

        [Description("IN_PRODUCTION_WAITING_MATERIAL")]
        InProductionWaitingMaterial = 12,

        [Description("IN_PRODUCTION_WAITING_INVOICING")]
        InProductionWaitingInvoicing = 13,

        [Description("IN_PRODUCTION_INVOICED")]
        InProductionInvoiced = 14,

        [Description("PICK_UP_AVAILABLE")]
        PickUpAvailable = 15,

        [Description("PICKED_UP")]
        PickedUp = 16,

        [Description("IN_TRANSIT")]
        InTransit = 17,

        [Description("DELIVERED")]
        Delivered = 18,

        [Description("IN_APPROVAL")]
        InApproval = 19,

        [Description("Expired")]
        Expired = 20
    }
}
