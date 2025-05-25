using ExcelReader.KamilKolanowski.Models;
using OfficeOpenXml;

namespace ExcelReader.KamilKolanowski.Services;

public class ExcelReaderService : IExcelReaderService
{
    public void ReadExcelFile(string excelFilePath, string sheetName)
    {
        using (var package = new ExcelPackage(excelFilePath))
        {
            var sheet = package.Workbook.Worksheets[sheetName];
            var excelData = package.GetAsByteArray();
            var sales = new List<Sales>();

            // sales.Add(new { });
        }
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
}
