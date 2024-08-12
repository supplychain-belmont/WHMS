using AutoMapper;

using Indotalent.DTOs;
using Indotalent.Models.Entities;

namespace Indotalent.Infrastructures.Mapper;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        #region Purchase DTOs

        CreateMap<Vendor, VendorDto>()
            .ForMember(dest => dest.VendorGroup,
                opt =>
                    opt.MapFrom(src => src.VendorGroup!.Name))
            .ForMember(dest => dest.VendorCategory,
                opt =>
                    opt.MapFrom(src => src.VendorCategory!.Name));
        CreateMap<VendorDto, Vendor>()
            .ForMember(dest => dest.VendorGroup,
                opt => opt.Ignore())
            .ForMember(dest => dest.VendorCategory,
                opt => opt.Ignore())
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());
        CreateMap<VendorGroup, VendorGroupDto>();
        CreateMap<VendorGroupDto, VendorGroup>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());
        CreateMap<VendorCategory, VendorCategoryDto>();
        CreateMap<VendorCategoryDto, VendorCategory>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());
        CreateMap<VendorContact, VendorContactDto>()
            .ForMember(dest => dest.Vendor,
                opt =>
                    opt.MapFrom(src => src.Vendor!.Name));
        CreateMap<VendorContactDto, VendorContact>()
            .ForMember(dest => dest.Vendor,
                opt => opt.Ignore())
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());
        CreateMap<PurchaseOrder, PurchaseOrderDto>()
            .ForMember(dest => dest.Vendor,
                opt
                    => opt.MapFrom(src => src.Vendor!.Name))
            .ForMember(dest => dest.Tax,
                opt =>
                    opt.MapFrom(src => src.Tax!.Name))
            .ForMember(dest => dest.Status,
                opt =>
                    opt.MapFrom(src => src.OrderStatus));
        CreateMap<PurchaseOrderDto, PurchaseOrder>()
            .ForMember(dest => dest.Vendor,
                opt => opt.Ignore())
            .ForMember(dest => dest.Tax,
                opt => opt.Ignore())
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore())
            .ForMember(dest => dest.OrderStatus,
                opt =>
                    opt.MapFrom(src => src.Status));
        CreateMap<PurchaseOrderItem, PurchaseOrderItemDto>()
            .ForMember(dest => dest.PurchaseOrder,
                opt =>
                    opt.MapFrom(src => src.PurchaseOrder!.Number))
            .ForMember(dest => dest.OrderDate,
                opt =>
                    opt.MapFrom(src => src.PurchaseOrder!.OrderDate))
            .ForMember(dest => dest.Vendor,
                opt =>
                    opt.MapFrom(src => src.PurchaseOrder!.Vendor!.Name))
            .ForMember(dest => dest.Product,
                opt =>
                    opt.MapFrom(src => src.Product!.Name));
        CreateMap<PurchaseOrderItemDto, PurchaseOrderItem>()
            .ForMember(dest => dest.PurchaseOrder,
                opt => opt.Ignore())
            .ForMember(dest => dest.Product,
                opt => opt.Ignore())
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());
        CreateMap<PurchaseOrderItem, PurchaseOrderItemChildDto>();
        CreateMap<PurchaseOrderItemChildDto, PurchaseOrderItem>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());

        #endregion

        #region Settings DTOs

        CreateMap<Tax, TaxDto>();
        CreateMap<TaxDto, Tax>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());
        CreateMap<ApplicationUser, UserProfileDto>()
            .ForMember(dest => dest.SelectedCompany, opt
                => opt.MapFrom(src => src.SelectedCompany!.Name));
        CreateMap<UserProfileDto, ApplicationUser>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.SelectedCompany,
                opt => opt.Ignore());
        CreateMap<ApplicationUser, ApplicationUserDto>()
            .ForMember(dest => dest.SelectedCompany, opt
                => opt.MapFrom(src => src.SelectedCompany!.Name));
        CreateMap<ApplicationUserDto, ApplicationUser>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.SelectedCompany,
                opt => opt.Ignore());
        CreateMap<NumberSequence, NumberSequenceDto>();
        CreateMap<NumberSequenceDto, NumberSequence>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());

        #endregion
    }
}
