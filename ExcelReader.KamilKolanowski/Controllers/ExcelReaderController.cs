using ExcelReader.KamilKolanowski.Services;

namespace ExcelReader.KamilKolanowski.Controllers;

internal class ExcelReaderController
{
    private readonly ExcelReaderService _excelReaderService = new();

    internal void ProcessFile(string excelFilePath)
    {
        _excelReaderService.WriteExcelFile("sales.xlsx");
    }
}
