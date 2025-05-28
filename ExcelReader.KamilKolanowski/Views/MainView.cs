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

    public async Task Start()
    {
        await AnsiConsole
            .Progress()
            .StartAsync(async ctx =>
            {
                var loadApp = ctx.AddTask("[green]Loading App [/]");
                var loadDb = ctx.AddTask("[green]Creating Database [/]");
                var loadSchema = ctx.AddTask("[green]Creating Schema [/]");
                var loadTable = ctx.AddTask("[green]Creating Table [/]");

                while (!ctx.IsFinished)
                {
                    await Task.Delay(250);

                    loadApp.Increment(10.3);
                    loadDb.Increment(8.1);
                    loadSchema.Increment(7.7);
                    loadTable.Increment(7.1);
                }
            });

        while (true)
        {
            Console.Clear();

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose what you want to do.")
                    .AddChoices(ViewOptions.MainMenuView.Values)
            );

            var choice = ViewOptions
                .MainMenuView.FirstOrDefault(o => o.Value == selectedOption)
                .Key;

            switch (choice)
            {
                case ViewOptions.MenuOptions.AddFile:
                    var filePath = PromptForFilePath();
                    var sheetName = PromptForSheetName();

                    var processedFile = _controller.ProcessFile(filePath, sheetName);
                    if (processedFile)
                    {
                        await LoadToDatabaseAnimation(sheetName);
                        var viewData = AnsiConsole.Confirm("Do you want to see the data?");
                        if (viewData)
                        {
                            _controller.PresentTableFromDatabase();
                        }
                    }
                    GoBackToMenu();
                    break;
                case ViewOptions.MenuOptions.ReadFile:
                    filePath = PromptForFilePath();
                    sheetName = PromptForSheetName();
                    var presentFile = _controller.PresentFile(filePath, sheetName);

                    if (presentFile)
                    {
                        var checkIfUserWantToSave = AskIfUserWantToSaveToDatabase();
                        if (checkIfUserWantToSave)
                        {
                            _controller.ProcessFile(filePath, sheetName);
                        }
                        await LoadToDatabaseAnimation(sheetName);
                    }
                    GoBackToMenu();
                    break;
                case ViewOptions.MenuOptions.ReadTable:
                    _controller.PresentTableFromDatabase();
                    GoBackToMenu();
                    break;
                case ViewOptions.MenuOptions.SaveToFile:
                    var fileName = PromptForSaveFilePath();
                    var fullPath = fileName.Item1.EndsWith("/")
                        ? $"{fileName.Item1}{fileName.Item2}.xlsx"
                        : $"{fileName.Item1}/{fileName.Item2}.xlsx";
                    _controller.SaveToFile(fileName.Item1, fullPath);
                    GoBackToMenu();
                    break;
                case ViewOptions.MenuOptions.Exit:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private FileInfo PromptForFilePath()
    {
        while (true)
        {
            var excelFilePath = AnsiConsole.Ask<string>(
                "Specify the path of your file [yellow](case sensitive)[/] with its format:"
            );
            FileInfo existingFile = new FileInfo(excelFilePath);
            if (!existingFile.Exists)
            {
                AnsiConsole.MarkupLine($"[red]The file '{excelFilePath}' does not exist.[/]");
                AnsiConsole.MarkupLine("[red]Try again![/]");
                continue;
            }

            return existingFile;
        }
    }

    private (string, string) PromptForSaveFilePath()
    {
        return (
            AnsiConsole.Ask<string>("Specify the directory for saving your file:"),
            AnsiConsole.Ask<string>("Specify the file name:")
        );
    }

    private string PromptForSheetName()
    {
        return AnsiConsole.Ask<string>("Specify the sheet name [yellow](case sensitive)[/]:");
    }

    private bool AskIfUserWantToSaveToDatabase()
    {
        return AnsiConsole.Confirm("Would you like to save your data to database?");
    }

    private void GoBackToMenu()
    {
        AnsiConsole.MarkupLine("Press any key to go back to the menu.");
        Console.ReadKey();
    }

    private async Task LoadToDatabaseAnimation(string sheetName)
    {
        await AnsiConsole
            .Progress()
            .StartAsync(async ctx =>
            {
                var loadData = ctx.AddTask("[green]Loading Data to Database [/]");

                while (!ctx.IsFinished)
                {
                    await Task.Delay(200);

                    loadData.Increment(8);
                }
            });

        AnsiConsole.MarkupLine($"[green]Your {sheetName} Data was added to the database![/]");
    }
}
