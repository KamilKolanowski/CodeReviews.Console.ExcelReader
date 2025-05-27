using ExcelReader.KamilKolanowski.Models;

namespace ExcelReader.KamilKolanowski.Services;

public interface IExcelReaderService
{
    public IEnumerable<Sales> ReadExcelFile(FileInfo excelFile, string sheetName);
    public void WriteExcelFile(IEnumerable<Sales> sales, string fileName);
}
