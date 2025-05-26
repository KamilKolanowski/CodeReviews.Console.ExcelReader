using ExcelReader.KamilKolanowski.Services;
using Spectre.Console;

namespace ExcelReader.KamilKolanowski.Controllers;

internal class ExcelReaderController
{
    private readonly ExcelReaderService _excelReaderService;

    public ExcelReaderController(ExcelReaderService excelReaderService)
    {
        _excelReaderService = excelReaderService;
    }

    internal void ProcessFile(string excelFilePath, string sheetName)
    {
        var sales = _excelReaderService.ReadExcelFile(excelFilePath, sheetName);
        _excelReaderService.WriteExcelFileToDatabase(sales);
        
        AnsiConsole.MarkupLine($"[green]Your {sheetName} data was added to the database![/]");
    }


    internal void PresentFile(string excelFilePath, string sheetName)
    {
        var table = new Table();

        table.AddColumn("SalesDate");
        table.AddColumn("Price");
        table.AddColumn("Tax");
        table.AddColumn("Discount");
        table.AddColumn("Total");
        table.AddColumn("Currency");
        table.AddColumn("Market");
        table.AddColumn("ProductName");
        
        var sales = _excelReaderService.ReadExcelFile(excelFilePath, sheetName);
        foreach (var sale in sales)
        {
            table.AddRow(
                sale.SalesDate.ToString("dd/MM/yyyy hh:mm:ss"),
                sale.Price.ToString("C2"),
                sale.Tax.ToString("C2"),
                sale.Discount.ToString("C2"),
                sale.Total.ToString("C2"),
                sale.Currency,
                sale.Market,
                sale.ProductName
            );
        }
        
        AnsiConsole.Render(table);
    }
}

