using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Shared.Domain.Fixed;
using CreditCardDto = POC.Bff.Web.Domain.Dtos.CreditCardDto;
using InvoicingDto = POC.Bff.Web.Domain.Dtos.InvoicingDto;
using POC.Shared.Domain.ValueObjects;

namespace POC.Bff.Web.Domain.Profiles
{
    public class CommercialProfile : Profile
    {
        public CommercialProfile()
        {
            CreateMap<FindComponentDto, FindComponentRequest>()
                .ForMember(dest => dest.Manufacturers, opt => opt.MapFrom(x => x.ManufacturerList))
                .ForMember(dest => dest.ComponentTypes, opt => opt.MapFrom(x => x.ComponentTypeList))
                .ForMember(dest => dest.Powers, opt => opt.MapFrom(x => x.PowerList))
                .ForMember(dest => dest.Tiers, opt => opt.MapFrom(x => x.TierList))
                .ForMember(dest => dest.DistributionCenters, opt => opt.MapFrom(x => x.CdList))
                .ReverseMap();

            CreateMap<ComponentDto, ComponentResponse>()
                .ForMember(dest => dest.CdId, opt => opt.MapFrom(x => x.DistributionCenterId))
                .ForMember(dest => dest.PriceList, opt => opt.MapFrom(x => x.Prices))
                .ReverseMap();

            CreateMap<FindMemorialDto, GetMemorialRequest>()
                .ForMember(dest => dest.ComponentTypes, opt => opt.MapFrom(x => x.ComponentTypeList))
                .ForMember(dest => dest.Manufacturers, opt => opt.MapFrom(x => x.ManufacturerList))
                .ForMember(dest => dest.Powers, opt => opt.MapFrom(x => x.PowerList))
                .ForMember(dest => dest.Tiers, opt => opt.MapFrom(x => x.TierList))
                .ForMember(dest => dest.DistributionCenters, opt => opt.MapFrom(x => x.CdList))
                .ReverseMap();

            CreateMap<GroupedMemorialComponentsDto, GroupedMemorialComponentsResponse>()
                .ForMember(dest => dest.PowerList, opt => opt.MapFrom(x => x.Powers))
                .ForMember(dest => dest.TierList, opt => opt.MapFrom(x => x.Tiers))
                .ForMember(dest => dest.CdList, opt => opt.MapFrom(x => x.DistributionCenters))
                .ReverseMap();

            CreateMap<UpdateMemorialComponentCmvDto, UpdateMemorialComponentCmvRequest>()
                .ForMember(dest => dest.DistributionCenterId, opt => opt.MapFrom(x => x.CdId));

            CreateMap<BatchChangeMemorialComponentDto, BatchChangeMemorialComponentRequest>()
                .ForMember(dest => dest.CombinationGroups, opt => opt.MapFrom(x => x.CombinationGroupList))
                .ForMember(dest => dest.Combinations, opt => opt.MapFrom(x => x.CombinationList))
                .ReverseMap();

            CreateMap<UpdateMemorialComponentMarkupDto, UpdateMemorialComponentMarkupRequest>()
                .ForMember(dest => dest.DistributionCenterId, opt => opt.MapFrom(x => x.CdId))
                .ReverseMap();

            CreateMap<BatchChangeComponentMarkupDto, BatchChangeComponentMarkupRequest>()
                .ForMember(dest => dest.CombinationGroups, opt => opt.MapFrom(x => x.CombinationGroupList))
                .ForMember(dest => dest.Combinations, opt => opt.MapFrom(x => x.CombinationList))
                .ReverseMap();

            CreateMap<QuotationListDto, QuotationListResponse>()
                .ForMember(dest => dest.CustomerDetail, opt => opt.MapFrom(x => x.CustomerResumeDetail))
                .ReverseMap();

            CreateMap<ZipCodeDto, ZipCodeResponse>()
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();

            CreateMap<PvSystemListDto, PvSystemListResponse>()
                .ForMember(dest => dest.PvSystemPrice, opt => opt.MapFrom(x => x.Price))
                .ReverseMap();

            CreateMap<SystemComponentDto, SystemComponentResponse>()
                .ReverseMap();

            CreateMap<DiscountDto, DiscountResponse>()
             .ReverseMap();

            CreateMap<QuotationStatusDto, QuotationStatusResponse>()
             .ReverseMap();

            CreateMap<SystemDetailDto, PvSystemDetailResponse>()
                .ForMember(dest => dest.PvSystemPrice, opt => opt.MapFrom(x => x.Price))
                .ReverseMap();

            CreateMap<InvoicingDto, InvoicingResponse>()
                .ReverseMap();

            CreateMap<PaymentDto, PaymentResponse>()
                .ForMember(dest => dest.DiscountSices, opt => opt.MapFrom(x => x.Discount))
                .ForMember(dest => dest.CreditCardList, opt => opt.MapFrom(x => x.CreditCards))
                .ReverseMap();

            CreateMap<CreditCardDto, CreditCardResponse>()
                .ForMember(dest => dest.Parcels, opt => opt.MapFrom(x => x.Installments))
                .ForMember(dest => dest.ParcelValue, opt => opt.MapFrom(x => x.InstallmentValue))
                .ReverseMap();


            CreateMap<QuotationDto, QuotationResponse>()
                .ForMember(dest => dest.TagIdList, opt => opt.MapFrom(x => x.TagItems))
                .ReverseMap();

            CreateMap<ShippingDto, ShippingResponse>().ReverseMap();

            CreateMap<SystemQuotationDto, SystemQuotationRequest>()
                .ReverseMap();


            CreateMap<SystemDto, SystemResponse>()
                .ForMember(dest => dest.Insurances, opt => opt.MapFrom(x => x.InsuranceList))
                .ForMember(dest => dest.Components, opt => opt.MapFrom(x => x.ComponentList))
                .ReverseMap();

            CreateMap<PvSystemDto, PvSystemResponse>()
                .ForMember(dest => dest.PvSystemList, opt => opt.MapFrom(x => x.PvSystems))
                .ReverseMap();

            CreateMap<FindQuotationsDto, FindQuotationsRequest>()
                .ForMember(dest => dest.Tiers, opt => opt.MapFrom(x => x.TierList))
                .ForMember(dest => dest.OrderType, opt => opt.MapFrom(x => x.Field == null ? OrderType.Descending : OrderType.Ascending))
                .ForMember(dest => dest.ErpCodes, opt => opt.MapFrom(x => x.ComponentList))
                .ReverseMap();

            CreateMap<SystemDto, SystemRequest>()
                .ForMember(dest => dest.Components, opt => opt.MapFrom(x => x.ComponentList))
                .ForMember(dest => dest.Insurances, opt => opt.MapFrom(x => x.InsuranceList))
                .ReverseMap();

            CreateMap<ComponentCombinationDto, ComponentCombinationRequest>()
                .ForMember(dest => dest.Powers, opt => opt.MapFrom(x => x.PowerList))
                .ReverseMap();

            CreateMap<GroupComponentCombinationDto, GroupComponentCombinationRequest>()
                .ForMember(dest => dest.Powers, opt => opt.MapFrom(x => x.PowerList))
                .ForMember(dest => dest.Tiers, opt => opt.MapFrom(x => x.TierList))
                .ForMember(dest => dest.DistributionCenters, opt => opt.MapFrom(x => x.CdList))
                .ReverseMap();

            CreateMap<UpdateIgnoreTierChangeUntilRequest, UpdateIgnoreTierChangeUntilCommercialRequest>()
                .ReverseMap();

            CreateMap<SavePaymentDetailQuotationRequest, SavePaymentDetailQuotationCommercialRequest>()
                .ReverseMap();

            CreateMap<CalculateQuotationPriceRequest, CalculateQuotationPriceCommercialRequest>()
                .ReverseMap();

            CreateMap<FindInsuranceByRangesRequest, FindInsuranceByRangesCommercialRequest>()
                .ReverseMap();

            CreateMap<AvailablePromotionResponse, AvailablePromotionCommercialResponse>()
                .ReverseMap();

            CreateMap<AvailablePromotionCommercialResponse.FeaturePromotion, FeatureRequest>()
                .ForMember(dest => dest.LinkText, opt => opt.MapFrom(x => x.Link.FileName))
                .ForMember(dest => dest.LinkUrl, opt => opt.MapFrom(x => x.Link.KeyName))
                .ReverseMap();

            CreateMap<PromotionRequest, PromotionCommercialRequest>();

            CreateMap<ComponentConfigurationRequest, ComponentConfigurationCommercialRequest>()
                .ForMember(dest => dest.GroupedComponents, opt => opt.MapFrom(src => src.ComponentGroupList))
                .ReverseMap();

            CreateMap<ComponentGroupRequest, ComponentGroupCommercialRequest>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.ComponentOptionList))
                .ReverseMap();


            CreateMap<AccountResponse, GenericResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();

            CreateMap<DistributionCenterDto, GenericResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();

            CreateMap<ProductsPriceDto, ProductsPriceRequest>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(x => x.ProductsList))
                .ReverseMap();

            CreateMap<GetPromotionByIdCommercialResponse, PromotionResponse>();

            CreateMap<GetPromotionByIdCommercialResponse, PromotionRequest>();

            CreateMap<ProductsPriceDto, ProductsPriceRequest>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(x => x.ProductsList))
                .ReverseMap();
            CreateMap<ProductsPriceDto, ProductsPriceRequest>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(x => x.ProductsList))
                .ReverseMap();
            CreateMap<DistributionCenterDto, GenericResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();
            CreateMap<AccountResponse, GenericResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();

            CreateMap<AddressResponse, AddressRequest>();

            CreateMap<ShippingResponse, SaveShippingQuotationRequest>();

            CreateMap<NotificationsPagedResponse, PagedNotificationsDto>()
                 .ForMember(dest => dest.LinkList, opt => opt.MapFrom(x => x.Links));

            CreateMap<ChangeMessageAsReadRequest, ChangeMessageAsReadDto>()
                .ForMember(dest => dest.MessagesIdentifiers, opt => opt.MapFrom(x => x.MessageIdList));

            CreateMap<ChangeNotesAsReadRequest, ChangeNotesAsReadDto>()
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(x => x.NoteIdList));

            CreateMap<CommentRequest, CommentDto>()
                .ReverseMap();

            CreateMap<CreateInvoicingQuotationRequest, CreateInvoicingQuotationCommercialRequest>()
               .ReverseMap();

            CreateMap<StatusDetailDto, QuotationStatusResponse>()
                .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<OrganizationRequest, UpdateOrganizationDto>()
                .ForMember(src => src.ErpCustomerCode, opt => opt.Ignore());
        }
    }
}