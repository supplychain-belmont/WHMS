﻿using Indotalent.ApiOData;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.OData.ModelBuilder;

namespace Indotalent.Infrastructures.ODatas
{
    public static class ODataServiceExtensions
    {
        public static void AddCustomOData(this IServiceCollection services)
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<LogAnalyticDto>("LogAnalytic");
            builder.EntitySet<LogErrorDto>("LogError");
            builder.EntitySet<LogSessionDto>("LogSession");
            builder.EntitySet<NumberSequenceDto>("NumberSequence");
            builder.EntitySet<ApplicationUserDto>("ApplicationUser");
            builder.EntitySet<UserProfileDto>("UserProfile");
            builder.EntitySet<CompanyDto>("Company");
            builder.EntitySet<CustomerGroupDto>("CustomerGroup");
            builder.EntitySet<CustomerCategoryDto>("CustomerCategory");
            builder.EntitySet<VendorGroupDto>("VendorGroup");
            builder.EntitySet<VendorCategoryDto>("VendorCategory");
            builder.EntitySet<WarehouseDto>("Warehouse");
            builder.EntitySet<CustomerDto>("Customer");
            builder.EntitySet<VendorDto>("Vendor");
            builder.EntitySet<UnitMeasureDto>("UnitMeasure");
            builder.EntitySet<ProductGroupDto>("ProductGroup");
            builder.EntitySet<ProductDto>("Product");
            builder.EntitySet<CustomerContactDto>("CustomerContact");
            builder.EntitySet<CustomerContactChildDto>("CustomerContactChild");
            builder.EntitySet<VendorContactDto>("VendorContact");
            builder.EntitySet<VendorContactChildDto>("VendorContactChild");
            builder.EntitySet<TaxDto>("Tax");
            // builder.EntitySet<SalesOrderDto>("SalesOrder");
            builder.EntitySet<SalesOrderItemChildDto>("SalesOrderItemChild");
            builder.EntitySet<SalesOrderItemDto>("SalesOrderItem");
            builder.EntitySet<PurchaseOrderDto>("PurchaseOrder");
            builder.EntitySet<PurchaseOrderItemChildDto>("PurchaseOrderItemChild");
            builder.EntitySet<PurchaseOrderItemDto>("PurchaseOrderItem");
            builder.EntitySet<InvenTransDto>("InvenTrans");
            builder.EntitySet<InvenStockDto>("InvenStock");
            builder.EntitySet<DeliveryOrderDto>("DeliveryOrder");
            builder.EntitySet<SalesReturnDto>("SalesReturn");
            builder.EntitySet<GoodsReceiveDto>("GoodsReceive");
            builder.EntitySet<PurchaseReturnDto>("PurchaseReturn");
            builder.EntitySet<TransferOutDto>("TransferOut");
            builder.EntitySet<TransferInDto>("TransferIn");
            builder.EntitySet<PositiveAdjustmentDto>("PositiveAdjustment");
            builder.EntitySet<NegativeAdjustmentDto>("NegativeAdjustment");
            builder.EntitySet<ScrappingDto>("Scrapping");
            builder.EntitySet<StockCountDto>("StockCount");
            builder.EntitySet<DeliveryOrderItemChildDto>("DeliveryOrderItemChild");
            builder.EntitySet<GoodsReceiveItemChildDto>("GoodsReceiveItemChild");
            builder.EntitySet<SalesReturnItemChildDto>("SalesReturnItemChild");
            builder.EntitySet<PurchaseReturnItemChildDto>("PurchaseReturnItemChild");
            builder.EntitySet<TransferOutItemChildDto>("TransferOutItemChild");
            builder.EntitySet<TransferInItemChildDto>("TransferInItemChild");
            builder.EntitySet<AdjustmentPlusItemChildDto>("AdjustmentPlusItemChild");
            builder.EntitySet<AdjustmentMinusItemChildDto>("AdjustmentMinusItemChild");
            builder.EntitySet<ScrappingItemChildDto>("ScrappingItemChild");
            builder.EntitySet<StockCountItemChildDto>("StockCountItemChild");
            builder.EntitySet<FileImageDto>("FileImage");
            // builder.EntitySet<AssemblyDto>("Assembly");
            builder.EntitySet<AssemblyChildDto>("AssemblyChild");
            builder.EntitySet<LotDto>("Lot");
            builder.EntitySet<LotItemDto>("LotItem");
            // builder.EntitySet<PdfResource>("PdfGenerator");
            var pdfAction = builder.EntityType<PdfResource>().Collection.Action("GeneratePdf");
            pdfAction.Returns<Stream>();
            pdfAction.Parameter<int>("Id");
            pdfAction.Parameter<int>("productId");
            pdfAction.Parameter<int>("purchaseOrderId");
            pdfAction.Parameter<int>("salesOrderId");
            pdfAction.Parameter<int>("quantity");
            builder.EntitySet<PdfResource>("PdfGenerator");
            var pdfFunction = builder.EntityType<PdfResource>().Collection.Function("PurchaseOrderReport");
            pdfFunction.Returns<Stream>();
            pdfFunction.Parameter<int>("purchaseOrderId");
            var pdfSalesFunction = builder.EntityType<PdfResource>().Collection.Function("SalesOrderReport");
            pdfSalesFunction.Returns<Stream>();
            pdfSalesFunction.Parameter<int>("salesOrderId");

            var assemblyAction = builder.EntityType<SalesOrderDto>().Collection.Action("OrderFromAssembly");
            assemblyAction.Parameter<int>("assemblyId");
            assemblyAction.Parameter<int>("quantity");
            assemblyAction.ReturnsFromEntitySet<SalesOrderDto>("SalesOrder");

            var assemblyAction2 = builder.EntityType<AssemblyDto>().Collection.Action("BuildAssembly");
            assemblyAction2.Parameter<int>("productId");
            assemblyAction2.Parameter<int>("warehouseId");
            assemblyAction2.Parameter<int>("quantity");
            assemblyAction2.ReturnsFromEntitySet<AssemblyDto>("Assembly");


            services.AddControllers()
                .AddNewtonsoftJson()
                .AddOData(options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null)
                    .AddRouteComponents("odata", builder.GetEdmModel(), odataServices =>
                    {
                        odataServices.AddSingleton<IODataQueryValidator, SFODataQueryValidator>();
                        odataServices.AddSingleton<ODataBatchHandler, DefaultODataBatchHandler>();
                    }));
        }
    }
}
