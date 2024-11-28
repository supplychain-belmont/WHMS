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
                it.Product,
                it.Summary,
                it.UnitCostDiscounted,
                it.Quantity,
                it.M3,
                it.WeightedPercentageM3,
                it.TotalShippingCost,
                it.UnitCost,
                it.Total
            })
            .AsEnumerable();

        return _syncPdfService.GenerateOrderReport(order!, items);
    }
}
