using ExcelReader.KamilKolanowski.Models;
using ExcelReader.KamilKolanowski.Repositories;
using OfficeOpenXml;

namespace ExcelReader.KamilKolanowski.Services;

public class ExcelReaderService : IExcelReaderService
{
    private readonly IDbRepository _dbRepository;

    public ExcelReaderService(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }
    public IEnumerable<Sales> ReadExcelFile(string excelFilePath, string sheetName)
    {
        var sales = new List<Sales>();

        try
        {
            FileInfo existingFile = new FileInfo(excelFilePath);
            using (var package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

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
                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return sales;
    }
    
    public void WriteExcelFile(string excelFilePath)
    {
        using (var package = new ExcelPackage(excelFilePath))
        {
            var sheet = package.Workbook.Worksheets.Add("Sales");
            package.Save();
            Console.WriteLine("test");
        }
    }

    public void WriteExcelFileToDatabase(IEnumerable<Sales> sales)
    {
        _dbRepository.Insert(sales);
    }
}
