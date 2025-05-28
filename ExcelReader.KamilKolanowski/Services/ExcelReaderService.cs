using ExcelReader.KamilKolanowski.Models;
using ExcelReader.KamilKolanowski.Repositories;
using OfficeOpenXml;
using Spectre.Console;

namespace ExcelReader.KamilKolanowski.Services;

public class ExcelReaderService : IExcelReaderService
{
    private readonly IDbRepository _dbRepository;

    public ExcelReaderService(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public IEnumerable<Sales> ReadExcelFile(FileInfo excelFile, string sheetName)
    {
        var sales = new List<Sales>();

        try
        {
            using (var package = new ExcelPackage(excelFile))
            {
                var worksheet = package.Workbook.Worksheets[sheetName];
                if (worksheet != null)
                {
                    int rowCount = worksheet.Dimension.End.Row;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var sale = new Sales
                        {
                            SalesDate = DateTime.Parse(worksheet.Cells[row, 1].Text),
                            Price = decimal.Parse(worksheet.Cells[row, 2].Text),
                            Tax = decimal.Parse(worksheet.Cells[row, 3].Text),
                            Discount = decimal.Parse(worksheet.Cells[row, 4].Text),
                            Total = decimal.Parse(worksheet.Cells[row, 5].Text),
                            Currency = worksheet.Cells[row, 6].Text,
                            Market = worksheet.Cells[row, 7].Text,
                            ProductName = worksheet.Cells[row, 8].Text,
                        };

                        sales.Add(sale);
                    }

                    return sales;
                }
                AnsiConsole.MarkupLine($"[red]Sheet '{sheetName}' does not exist.[/]");
                return Enumerable.Empty<Sales>();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            return Enumerable.Empty<Sales>();
        }
    }

    public void WriteExcelFile(IEnumerable<Sales> sales, string directory, string fileName)
    {
        using (var package = new ExcelPackage(new FileInfo(fileName)))
        {
            if (!Directory.Exists(directory))
            {
                AnsiConsole.MarkupLine("[red]Directory doesn't exist![/]");
                return;
            }

            if (File.Exists(fileName))
            {
                AnsiConsole.MarkupLine("[red]File already exist![/]");
                return;
            }
            var sheet = package.Workbook.Worksheets.Add("Sales");
            sheet.Cells[1, 1].LoadFromCollection(sales);
            package.Save();

            AnsiConsole.MarkupLine(
                $"[green]Data from Sales table has been saved to file[/]: [cyan1]{fileName}[/]"
            );
        }
    }

    public IEnumerable<Sales> ReadDataFromDatabase()
    {
        return _dbRepository.GetSales();
    }

    public void WriteExcelFileToDatabase(IEnumerable<Sales> sales)
    {
        _dbRepository.Insert(sales);
    }
}
