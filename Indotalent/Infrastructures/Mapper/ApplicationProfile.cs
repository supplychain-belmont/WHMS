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

        #endregion
    }
}
