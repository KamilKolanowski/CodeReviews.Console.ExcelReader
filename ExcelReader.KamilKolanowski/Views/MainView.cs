using ExcelReader.KamilKolanowski.Controllers;
using Spectre.Console;

namespace ExcelReader.KamilKolanowski.Views;

public class MainView
{
    private readonly ViewOptions _options;
    private readonly ExcelReaderController _controller;

    public MainView(ViewOptions options, ExcelReaderController controller)
    {
        _options = options;
        _controller = controller;
    }

    public void Start()
    {
        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose what you want to do")
                .AddChoices(ViewOptions.MainMenuView.Values));
        
        var choice = ViewOptions
            .MainMenuView.FirstOrDefault(o => o.Value == selectedOption)
            .Key;

        var filePath = PromptForFilePath();
        var sheetName = PromptForSheetName();
        
        switch (choice)
        {
            case ViewOptions.MenuOptions.AddFile:
                _controller.ProcessFile(filePath, sheetName);
                break;
            case ViewOptions.MenuOptions.ReadFile:
                _controller.PresentFile(filePath, sheetName);
                break;
        }
    }

    private string PromptForFilePath()
    {
        return AnsiConsole.Ask<string>("Specify the path of your file:");
    }
    private string PromptForSheetName()
    {
        return AnsiConsole.Ask<string>("Specify the sheet name:");
    }
}