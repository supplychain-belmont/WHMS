using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;

using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace Indotalent.Infrastructures.Pdfs;

public class SyncPdfService
{
    public FileStreamResult GenerateOrderReport(PurchaseOrderDto orderDto, IEnumerable<object> items)
    {
        PdfDocument document = new();
        document.PageSettings.Size = PdfPageSize.Letter;

        PdfPage page = document.Pages.Add();

        PdfGraphics graphics = page.Graphics;

        FileStream imageStream = new("Utils/Images/belmont.png", FileMode.Open, FileAccess.Read);
        PdfBitmap image = new(imageStream);
        graphics.DrawImage(image, document.PageSettings.Width - 200, -20, 100, 100);

        PdfStringFormat format = new();
        format.LineAlignment = PdfVerticalAlignment.Middle;
        format.EnableBaseline = true;
        var title = $"Reporte de compra";
        PdfFont fontTitle = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
        PdfFont fontText = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
        PdfFont fontBoldText = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold);
        // headers

        #region headers

        graphics.DrawString(title, fontTitle, PdfBrushes.Black, new PointF(175, 75), format);
        graphics.DrawString("Id de Orden: ", fontBoldText, PdfBrushes.Black, new PointF(25, 125), format);
        graphics.DrawString("Estado: ", fontBoldText, PdfBrushes.Black, new PointF(225, 125), format);
        graphics.DrawString("Fecha de la orden: ", fontBoldText, PdfBrushes.Black, new PointF(350, 125), format);
        graphics.DrawString("Descripción: ", fontBoldText, PdfBrushes.Black, new PointF(25, 150), format);
        graphics.DrawString("Metros cubicos contenedor: ", fontBoldText, PdfBrushes.Black, new PointF(25, 200),
            format);
        graphics.DrawString("Gasto de Transporte: ", fontBoldText, PdfBrushes.Black, new PointF(350, 200), format);
        graphics.DrawString("Proveedor: ", fontBoldText, PdfBrushes.Black, new PointF(25, 225), format);
        graphics.DrawString("Impuesto: ", fontBoldText, PdfBrushes.Black, new PointF(350, 225), format);

        #endregion

        #region Text

        graphics.DrawString(orderDto.Number ?? "Non-Code", fontText, PdfBrushes.Black, new PointF(100, 125), format);
        graphics.DrawString($"{orderDto.Status}", fontText, PdfBrushes.Black, new PointF(275, 125), format);
        graphics.DrawString($"{orderDto.OrderDate}", fontText, PdfBrushes.Black, new PointF(460, 125), format);
        graphics.DrawString($"{orderDto.ContainerM3} m3", fontText, PdfBrushes.Black, new PointF(200, 200), format);
        graphics.DrawString($"$ {orderDto.TotalTransportContainerCost}", fontText, PdfBrushes.Black,
            new PointF(475, 200),
            format);
        graphics.DrawString($"{orderDto.Vendor}", fontText, PdfBrushes.Black, new PointF(100, 225), format);
        graphics.DrawString($"{orderDto.Tax} %", fontText, PdfBrushes.Black, new PointF(425, 225), format);

        #endregion

        #region Table

        PdfGrid grid = new();
        grid.DataSource = items;
        var pdfGridLayoutResult = grid.Draw(page, new PointF(25, 250));

        #endregion

        #region Total

        RectangleF rectangleF = pdfGridLayoutResult.Bounds;
        var totalHeight = rectangleF.Y + rectangleF.Height + 20;
        var totalWidth = rectangleF.X + rectangleF.Width + 20;

        graphics.DrawString("Total sin Impuestos", fontBoldText, PdfBrushes.Black, new PointF(350, totalHeight),
            format);
        graphics.DrawString("Impuestos", fontBoldText, PdfBrushes.Black, new PointF(350, totalHeight + 25), format);
        graphics.DrawString("Total con Impuestos", fontBoldText, PdfBrushes.Black, new PointF(350, totalHeight + 50),
            format);

        graphics.DrawString($"${orderDto.BeforeTaxAmount}", fontText, PdfBrushes.Black, new PointF(475, totalHeight),
            format);
        graphics.DrawString($"${orderDto.TaxAmount}", fontText, PdfBrushes.Black, new PointF(475, totalHeight + 25),
            format);
        graphics.DrawString($"${orderDto.AfterTaxAmount}", fontText, PdfBrushes.Black,
            new PointF(475, totalHeight + 50),
            format);

        #endregion

        MemoryStream stream = new();
        document.Save(stream);
        stream.Position = 0;
        FileStreamResult fileStreamResult = new(stream, "application/pdf");
        fileStreamResult.FileDownloadName = $"Orden de compra {orderDto.Number}.pdf";
        return fileStreamResult;
    }

    public FileStreamResult GenerateSalesOrderReport(SalesOrderDto orderDto, IEnumerable<object> items)
    {
        PdfDocument document = new();
        document.PageSettings.Size = PdfPageSize.Letter;

        PdfPage page = document.Pages.Add();

        PdfGraphics graphics = page.Graphics;

        FileStream imageStream = new("Utils/Images/belmont.png", FileMode.Open, FileAccess.Read);
        PdfBitmap image = new(imageStream);
        graphics.DrawImage(image, document.PageSettings.Width - 200, -20, 100, 100);

        PdfStringFormat format = new();
        format.LineAlignment = PdfVerticalAlignment.Middle;
        format.EnableBaseline = true;
        var title = $"Reporte de venta";
        PdfFont fontTitle = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
        PdfFont fontText = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
        PdfFont fontBoldText = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold);
        // headers

        #region headers

        graphics.DrawString(title, fontTitle, PdfBrushes.Black, new PointF(175, 75), format);
        graphics.DrawString("Id de Orden: ", fontBoldText, PdfBrushes.Black, new PointF(25, 125), format);
        graphics.DrawString("Estado: ", fontBoldText, PdfBrushes.Black, new PointF(225, 125), format);
        graphics.DrawString("Fecha de la orden: ", fontBoldText, PdfBrushes.Black, new PointF(350, 125), format);
        graphics.DrawString("Descripción: ", fontBoldText, PdfBrushes.Black, new PointF(25, 150), format);
        graphics.DrawString("Cliente: ", fontBoldText, PdfBrushes.Black, new PointF(225, 150), format);
        graphics.DrawString("Impuesto: ", fontBoldText, PdfBrushes.Black, new PointF(25, 200), format);

        #endregion

        #region Text

        graphics.DrawString(orderDto.Number ?? "Non-Code", fontText, PdfBrushes.Black, new PointF(100, 125), format);
        graphics.DrawString($"{orderDto.Status}", fontText, PdfBrushes.Black, new PointF(275, 125), format);
        graphics.DrawString($"{orderDto.OrderDate}", fontText, PdfBrushes.Black, new PointF(460, 125), format);
        graphics.DrawString($"{orderDto.Customer}", fontText, PdfBrushes.Black, new PointF(275, 150), format);
        graphics.DrawString($"{orderDto.Tax} %", fontText, PdfBrushes.Black, new PointF(100, 200), format);

        #endregion

        #region Table

        PdfGrid grid = new();
        grid.DataSource = items;
        var pdfGridLayoutResult = grid.Draw(page, new PointF(25, 250));

        #endregion

        #region Total

        RectangleF rectangleF = pdfGridLayoutResult.Bounds;
        var totalHeight = rectangleF.Y + rectangleF.Height + 20;
        var totalWidth = rectangleF.X + rectangleF.Width + 20;

        graphics.DrawString("Total sin Impuestos", fontBoldText, PdfBrushes.Black, new PointF(350, totalHeight),
            format);
        graphics.DrawString("Impuestos", fontBoldText, PdfBrushes.Black, new PointF(350, totalHeight + 25), format);
        graphics.DrawString("Total con Impuestos", fontBoldText, PdfBrushes.Black, new PointF(350, totalHeight + 50),
            format);

        graphics.DrawString($"${orderDto.BeforeTaxAmount}", fontText, PdfBrushes.Black, new PointF(475, totalHeight),
            format);
        graphics.DrawString($"${orderDto.TaxAmount}", fontText, PdfBrushes.Black, new PointF(475, totalHeight + 25),
            format);
        graphics.DrawString($"${orderDto.AfterTaxAmount}", fontText, PdfBrushes.Black,
            new PointF(475, totalHeight + 50),
            format);

        #endregion

        MemoryStream stream = new();
        document.Save(stream);
        stream.Position = 0;
        FileStreamResult fileStreamResult = new(stream, "application/pdf");
        fileStreamResult.FileDownloadName = $"Orden de venta {orderDto.Number}.pdf";
        return fileStreamResult;
    }
}
