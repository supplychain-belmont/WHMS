using System.Globalization;

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
    private readonly CultureInfo _cultureInfo;

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
        _cultureInfo = new CultureInfo("es-BO");
    }

    [HttpGet("PdfGenerator/PurchaseOrderReport(PurchaseOrderId={purchaseOrderId})")]
    public async Task<IActionResult> PurchaseOrderReport([FromODataUri] int purchaseOrderId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return await ProcessOrder(purchaseOrderId);
    }


    [HttpGet("PdfGenerator/SalesOrderReport(SalesOrerId={salesOrderId})")]
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
                Costo_unitario = $" ${it.UnitCost!.Value.ToString("N2", _cultureInfo)}",
                Precio_unitario = $" ${it.UnitPrice!.Value.ToString("N2", _cultureInfo)}",
                Cantidad = $" {it.Quantity!.Value.ToString("N0", _cultureInfo)}",
                Descuento = $" ${it.UnitPriceDiscount!.Value.ToString("N2", _cultureInfo)}",
                Descuento_percent = $" {it.UnitPriceDiscountPercentage} %",
                Total = $" ${it.Total!.Value.ToString("N2", _cultureInfo)}",
                Comision = $" ${it.Commission!.Value.ToString("N2", _cultureInfo)}",
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
                Costo_unitario = $" ${it.UnitCost!.Value.ToString("N2", _cultureInfo)}",
                Cantidad = $" {it.Quantity!.Value.ToString("N0", _cultureInfo)}",
                M3 = $" {it.M3!.Value.ToString("N2", _cultureInfo)} m³",
                Porcentaje_peso_m3 = $" {it.WeightedPercentageM3!.Value.ToString("N2", _cultureInfo)} %",
                Costo_transporte = $" ${it.TotalShippingCost!.Value.ToString("N2", _cultureInfo)}",
                Costo_Bolivia = $" ${it.UnitCostBolivia!.Value.ToString("N2", _cultureInfo)}",
                Total = $" ${it.Total!.Value.ToString("N2", _cultureInfo)}",
            })
            .AsEnumerable();

        return _syncPdfService.GenerateOrderReport(order!, items);
    }
}
