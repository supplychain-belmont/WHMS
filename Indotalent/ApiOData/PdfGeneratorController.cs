using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.PurchaseOrderItems;
using Indotalent.Applications.PurchaseOrders;
using Indotalent.Applications.SalesOrderItems;
using Indotalent.Applications.SalesOrders;
using Indotalent.DTOs;
using Indotalent.Infrastructures.Pdfs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData;

public class PdfGeneratorController : ODataController
{
    private readonly SyncPdfService _syncPdfService;
    private readonly PurchaseOrderService _purchaseOrderService;
    private readonly SalesOrderService _salesOrderService;
    private readonly PurchaseOrderItemService _purchaseOrderItemService;
    private readonly SalesOrderItemService _salesOrderItemService;
    private readonly IMapper _mapper;

    public PdfGeneratorController(SyncPdfService syncPdfService,
        PurchaseOrderService purchaseOrderService,
        SalesOrderService salesOrderService,
        PurchaseOrderItemService purchaseOrderItemService,
        SalesOrderItemService salesOrderItemService,
        IMapper mapper)
    {
        _syncPdfService = syncPdfService;
        _purchaseOrderService = purchaseOrderService;
        _salesOrderService = salesOrderService;
        _purchaseOrderItemService = purchaseOrderItemService;
        _salesOrderItemService = salesOrderItemService;
        _mapper = mapper;
    }

    [HttpGet("/odata/PdfGenerator/PurchaseOrderReport(PurchaseOrderId={purchaseOrderId})")]
    public async Task<IActionResult> PurchaseOrderReport([FromODataUri] int purchaseOrderId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return await ProcessOrder(purchaseOrderId);
    }


    [HttpGet("/odata/PdfGenerator/SalesOrderReport(SalesOrerId={salesOrderId})")]
    public async Task<IActionResult> SalesOrderReport([FromODataUri] int salesOrderId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return await ProcessSalesOrder(salesOrderId);
    }

    [HttpPost]
    public async Task<IActionResult> GeneratePdf(ODataActionParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var purchaseOrderId = (int)parameters["purchaseOrderId"];
        var salesOrderId = (int)parameters["salesOrderId"];
        return await ProcessOrder(purchaseOrderId);
    }

    private async Task<FileStreamResult> ProcessSalesOrder(int orderId)
    {
        var order = await _salesOrderService
            .GetByIdAsync(orderId, po => po.Customer, po => po.Tax)
            .ProjectTo<SalesOrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        var items = _salesOrderItemService
            .GetAll()
            .Where(x => x.SalesOrderId == orderId)
            .Include(x => x.SalesOrder)
            .Include(x => x.Product)
            .ProjectTo<SalesOrderItemChildDto>(_mapper.ConfigurationProvider)
            .Select(it => new
            {
                Producto = it.Product,
                Codigo = it.Summary,
                Costo_unitario = it.UnitCost,
                Precio_unitario = it.UnitPrice,
                Cantidad = it.Quantity,
                Precio_unitario_descuento = it.UnitPriceDiscount,
                Precio_unitario_descuento_percen = it.UnitPriceDiscountPercentage,
                it.Total,
                Comision = it.Commission
            })
            .AsEnumerable();

        return _syncPdfService.GenerateSalesOrderReport(order!, items);
    }

    private async Task<FileStreamResult> ProcessOrder(int orderId)
    {
        var order = await _purchaseOrderService
            .GetByIdAsync(orderId, po => po.Vendor, po => po.Tax)
            .ProjectTo<PurchaseOrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        var items = _purchaseOrderItemService
            .GetAll()
            .Where(x => x.PurchaseOrderId == orderId)
            .Include(x => x.PurchaseOrder)
            .Include(x => x.Product)
            .ProjectTo<PurchaseOrderItemChildDto>(_mapper.ConfigurationProvider)
            .Select(it => new
            {
                Producto = it.Product,
                Codigo = it.Summary,
                Costo_unitario_descuento = it.UnitCostDiscounted,
                Cantidad = it.Quantity,
                it.M3,
                Porcentaje_peso_m3 = it.WeightedPercentageM3,
                Costo_transporte = it.TotalShippingCost,
                Costo_unitario = it.UnitCost,
                it.Total
            })
            .AsEnumerable();

        return _syncPdfService.GenerateOrderReport(order!, items);
    }
}
