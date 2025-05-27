using ExcelReader.KamilKolanowski.Models;
using ExcelReader.KamilKolanowski.Services;
using Spectre.Console;

namespace ExcelReader.KamilKolanowski.Controllers;

public class ExcelReaderController
{
    private readonly ExcelReaderService _excelReaderService;

    public ExcelReaderController(ExcelReaderService excelReaderService)
    {
        _excelReaderService = excelReaderService;
    }

    internal void ProcessFile(FileInfo excelFile, string sheetName)
    {
        var sales = _excelReaderService.ReadExcelFile(excelFile, sheetName);
        _excelReaderService.WriteExcelFileToDatabase(sales);
    }

    internal void SaveToFile(string fileName)
    {
        _excelReaderService.WriteExcelFile(_excelReaderService.ReadDataFromDatabase(), fileName);
    }

    internal void PresentFile(FileInfo excelFile, string sheetName)
    {
        var table = new Table { Title = new TableTitle($"[lime]{excelFile.Name}[/]") };

        table.AddColumn("SalesDate");
        table.AddColumn("Price");
        table.AddColumn("Tax");
        table.AddColumn("Discount");
        table.AddColumn("Total");
        table.AddColumn("Currency");
        table.AddColumn("Market");
        table.AddColumn("ProductName");

        var sales = _excelReaderService.ReadExcelFile(excelFile, sheetName);
        foreach (var sale in sales)
        {
            table.AddRow(
                sale.SalesDate.ToString("dd/MM/yyyy hh:mm:ss"),
                sale.Price.ToString("G"),
                sale.Tax.ToString("G"),
                sale.Discount.ToString("G"),
                sale.Total.ToString("G"),
                sale.Currency,
                sale.Market,
                sale.ProductName
            );
        }

        AnsiConsole.Render(table);
    }

    internal void PresentTableFromDatabase()
    {
        var table = new Table { Title = new TableTitle("[lime]Sales[/]") };

        table.AddColumn("SalesDate");
        table.AddColumn("Price");
        table.AddColumn("Tax");
        table.AddColumn("Discount");
        table.AddColumn("Total");
        table.AddColumn("Currency");
        table.AddColumn("Market");
        table.AddColumn("ProductName");

        var sales = _excelReaderService.ReadDataFromDatabase();
        foreach (var sale in sales)
        {
            table.AddRow(
                sale.SalesDate.ToString("dd/MM/yyyy hh:mm:ss"),
                sale.Price.ToString(),
                sale.Tax.ToString("G"),
                sale.Discount.ToString("G"),
                sale.Total.ToString("G"),
                sale.Currency,
                sale.Market,
                sale.ProductName
            );
        }

        AnsiConsole.Render(table);
    }
}
