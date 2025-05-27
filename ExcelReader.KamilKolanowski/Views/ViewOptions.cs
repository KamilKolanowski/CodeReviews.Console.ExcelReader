namespace ExcelReader.KamilKolanowski.Views;

public class ViewOptions
{
    public enum MenuOptions
    {
        AddFile,
        ReadFile,
        ReadTable,
        SaveToFile,
        Exit,
    }

    public static Dictionary<MenuOptions, string> MainMenuView { get; } =
        new()
        {
            { MenuOptions.AddFile, "Add File" },
            { MenuOptions.ReadFile, "Read File" },
            { MenuOptions.ReadTable, "Read Table" },
            { MenuOptions.SaveToFile, "Save to File" },
            { MenuOptions.Exit, "Exit" },
        };
}
