using ExcelReader.KamilKolanowski.Models;

namespace ExcelReader.KamilKolanowski.Services;

public interface IExcelReaderService
{
    public void ReadExcelFile(string excelFilePath, string sheetName);
    public void WriteExcelFile(string excelFilePath);
}
