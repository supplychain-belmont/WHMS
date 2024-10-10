using AutoMapper;

using Indotalent.DTOs;
using Indotalent.Infrastructures.Images;
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
        CreateMap<VendorContact, VendorContactChildDto>();
        CreateMap<VendorContactChildDto, VendorContact>()
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
        CreateMap<PurchaseOrderItem, PurchaseOrderItemChildDto>()
            .ForMember(dest => dest.M3,
                opt
                    => opt.MapFrom(src => src.Product!.M3));
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

        #region Logs DTOs

        CreateMap<LogError, LogErrorDto>();
        CreateMap<LogErrorDto, LogError>();
        CreateMap<LogAnalytic, LogAnalyticDto>();
        CreateMap<LogAnalyticDto, LogAnalytic>();
        CreateMap<LogSession, LogSessionDto>();
        CreateMap<LogSessionDto, LogSession>();

        #endregion

        #region Sales DTOs

        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.CustomerGroup,
                opt =>
                    opt.MapFrom(src => src.CustomerGroup!.Name))
            .ForMember(dest => dest.CustomerCategory,
                opt =>
                    opt.MapFrom(src => src.CustomerCategory!.Name));
        CreateMap<CustomerDto, Customer>()
            .ForMember(dest => dest.CustomerCategory,
                opt => opt.Ignore())
            .ForMember(dest => dest.CustomerGroup,
                opt => opt.Ignore())
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());
        CreateMap<CustomerContact, CustomerContactDto>()
            .ForMember(dest => dest.Customer,
                opt =>
                    opt.MapFrom(src => src.Customer!.Name));
        CreateMap<CustomerContactDto, CustomerContact>()
            .ForMember(dest => dest.Customer,
                opt => opt.Ignore())
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());
        CreateMap<CustomerGroup, CustomerGroupDto>();
        CreateMap<CustomerGroupDto, CustomerGroup>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());
        CreateMap<CustomerCategory, CustomerCategoryDto>();
        CreateMap<CustomerCategoryDto, CustomerCategory>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());
        CreateMap<SalesReturn, SalesReturnDto>()
            .ForMember(dest => dest.DeliveryOrder, opt => opt.MapFrom(src => src.DeliveryOrder!.Number))
            .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryOrder!.DeliveryDate))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.DeliveryOrder!.SalesOrder!.Customer!.Name));
        CreateMap<SalesReturnDto, SalesReturn>();
        CreateMap<SalesOrder, SalesOrderDto>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer!.Name))
            .ForMember(dest => dest.Tax, opt => opt.MapFrom(src => src.Tax!.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OrderStatus));
        CreateMap<SalesOrderDto, SalesOrder>()
            .ForMember(dest => dest.Customer,
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
        CreateMap<SalesOrderItem, SalesOrderItemDto>()
            .ForMember(dest => dest.SalesOrder, opt => opt.MapFrom(src => src.SalesOrder!.Number))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.SalesOrder!.Customer!.Name))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product!.Name));
        CreateMap<SalesOrderItemDto, SalesOrderItem>()
            .ForMember(dest => dest.SalesOrder, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid, opt => opt.Ignore());
        CreateMap<SalesOrderItem, SalesOrderItemChildDto>();
        CreateMap<SalesOrderItemChildDto, SalesOrderItem>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());

        #endregion

        #region File Image DTOs

        CreateMap<FileImage, FileImageDto>()
            .ForMember(dest => dest.CreatedAtUtc, opt => opt.MapFrom(src => src.CreatedAtUtc))
            .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => src.ImageData))
            .ForMember(dest => dest.OriginalFileName, opt => opt.MapFrom(src => src.OriginalFileName));

        CreateMap<FileImageDto, FileImage>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedByUserId, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedByUserId, opt => opt.Ignore())
            .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => src.ImageData))
            .ForMember(dest => dest.OriginalFileName, opt => opt.MapFrom(src => src.OriginalFileName));

        #endregion

        #region ProductDetail DTOs

        CreateMap<ProductDetails, ProductDetailsDto>()
            .ForMember(dest => dest.ProductId,
                opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.NationalProductOrderId,
                opt => opt.MapFrom(src => src.NationalProductOrderId))
            .ForMember(dest => dest.Dimensions,
                opt => opt.MapFrom(src => src.Dimensions))
            .ForMember(dest => dest.Brand,
                opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Service,
                opt => opt.MapFrom(src => src.Service));

        CreateMap<ProductDetailsDto, ProductDetails>()
            .ForMember(dest => dest.ProductId,
                opt => opt.Ignore())
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());

        #endregion

        #region TrasnfersDTOs

        CreateMap<TransferIn, TransferInDto>()
            .ForMember(dest => dest.TransferOut, opt =>
                opt.MapFrom(src => src.TransferOut!.Number))
            .ForMember(dest => dest.WarehouseFrom, opt =>
                opt.MapFrom(src => src.TransferOut!.WarehouseFrom!.Name))
            .ForMember(dest => dest.WarehouseTo, opt =>
                opt.MapFrom(src => src.TransferOut!.WarehouseTo!.Name))
            .ForMember(dest => dest.ReleaseDate, opt =>
                opt.MapFrom(src => src.TransferOut!.TransferReleaseDate))
            .ForMember(dest => dest.ReceiveDate, opt =>
                opt.MapFrom(src => src.TransferReceiveDate));
        CreateMap<TransferInDto, TransferIn>();
        CreateMap<TransferOut, TransferOutDto>()
            .ForMember(dest => dest.WarehouseFrom,
                opt =>
                    opt.MapFrom(src => src.WarehouseFrom!.Name))
            .ForMember(dest => dest.WarehouseTo,
                opt =>
                    opt.MapFrom(src => src.WarehouseTo!.Name));

        CreateMap<TransferOutDto, TransferOut>();
        CreateMap<InventoryTransaction, TransferOutItemChildDto>();
        CreateMap<TransferOutItemChildDto, InventoryTransaction>();

        #endregion

        #region productDTOs

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ProductGroup, opt => opt.MapFrom(src => src.ProductGroup!.Name))
            .ForMember(dest => dest.UnitMeasure, opt => opt.MapFrom(src => src.UnitMeasure!.Name));

        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.ProductGroup, opt => opt.Ignore())
            .ForMember(dest => dest.UnitMeasure, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid, opt => opt.Ignore());
        CreateMap<ProductGroup, ProductGroupDto>().ReverseMap();

        #endregion

        #region InventoryTransactionDTOs

        CreateMap<InventoryTransaction, InvenTransDto>()
            .ForMember(dest => dest.ModuleName, opt => opt.MapFrom(src => src.ModuleName))
            .ForMember(dest => dest.ModuleCode, opt => opt.MapFrom(src => src.ModuleCode))
            .ForMember(dest => dest.ModuleNumber, opt => opt.MapFrom(src => src.ModuleNumber))
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse!.Name))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product!.Name))
            .ForMember(dest => dest.WarehouseFrom, opt => opt.MapFrom(src => src.WarehouseFrom!.Name))
            .ForMember(dest => dest.WarehouseTo, opt => opt.MapFrom(src => src.WarehouseTo!.Name));

        CreateMap<InvenTransDto, InventoryTransaction>()
            .ForMember(dest => dest.ModuleName, opt => opt.MapFrom(src => src.ModuleName))
            .ForMember(dest => dest.ModuleCode, opt => opt.MapFrom(src => src.ModuleCode))
            .ForMember(dest => dest.ModuleNumber, opt => opt.MapFrom(src => src.ModuleNumber))
            .ForMember(dest => dest.WarehouseId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.WarehouseFromId, opt => opt.Ignore())
            .ForMember(dest => dest.WarehouseToId, opt => opt.Ignore());

        #endregion

        #region InventoryStockDTOs

        CreateMap<InventoryTransaction, InvenStockDto>()
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse!.Name))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product!.Name))
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock));

        CreateMap<InvenStockDto, InventoryTransaction>()
            .ForMember(dest => dest.WarehouseId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductId, opt => opt.Ignore());

        #endregion

        #region AdjusmentDTOs

        CreateMap<AdjustmentMinus, NegativeAdjustmentDto>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status));
        CreateMap<NegativeAdjustmentDto, AdjustmentMinus>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());
        CreateMap<AdjustmentPlus, PositiveAdjustmentDto>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status));
        CreateMap<PositiveAdjustmentDto, AdjustmentPlus>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());

        #endregion

        #region WarehouseDTOs

        CreateMap<Warehouse, WarehouseDto>().ReverseMap();

        #endregion

        #region StockDTOs

        CreateMap<StockCount, StockCountDto>()
            .ForMember(dest => dest.Warehouse,
                opt => opt.MapFrom(src => src.Warehouse!.Name));

        CreateMap<StockCountDto, StockCount>()
            .ForMember(dest => dest.Warehouse,
                opt => opt.Ignore())
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid,
                opt => opt.Ignore());

        #endregion

        #region ScrappingDTOs

        CreateMap<Scrapping, ScrappingDto>()
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse!.Name));
        CreateMap<ScrappingDto, Scrapping>();

        #endregion

        #region UnitMesureDTOs

        CreateMap<UnitMeasure, UnitMeasureDto>().ReverseMap();

        #endregion

        #region GoodsReceiveDTOs

        CreateMap<GoodsReceive, GoodsReceiveDto>()
            .ForMember(dest => dest.PurchaseOrder, opt => opt.MapFrom(src => src.PurchaseOrder!.Number))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.PurchaseOrder!.OrderDate))
            .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.PurchaseOrder!.Vendor!.Name));
        CreateMap<GoodsReceiveDto, GoodsReceive>()
            .ForMember(dest => dest.PurchaseOrder, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid, opt => opt.Ignore());
        CreateMap<InventoryTransaction, GoodsReceiveItemChildDto>()
            .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.RequestedMovement, opt => opt.MapFrom(src => src.RequestedMovement))
            .ForMember(dest => dest.Movement, opt => opt.MapFrom(src => src.Movement));
        CreateMap<GoodsReceiveItemChildDto, InventoryTransaction>();

        #endregion

        #region DeliveryOrders

        CreateMap<DeliveryOrder, DeliveryOrderDto>()
            .ForMember(dest => dest.SalesOrder, opt => opt.MapFrom(src => src.SalesOrder!.Number))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.SalesOrder!.OrderDate))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.SalesOrder!.Customer!.Name));
        CreateMap<DeliveryOrderDto, DeliveryOrder>()
            .ForMember(dest => dest.SalesOrder, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.RowGuid, opt => opt.Ignore());
        CreateMap<InventoryTransaction, DeliveryOrderItemChildDto>();
        CreateMap<DeliveryOrderItemChildDto, InventoryTransaction>();

        #endregion
    }
}
