namespace ExcelReader.KamilKolanowski.Views;

public class ViewOptions
{
    public enum MenuOptions
    {
        AddFile,
        ReadFile
    }
    
    public static Dictionary<MenuOptions, string> MainMenuView { get; } =
        new()
        {
            { MenuOptions.AddFile, "Add File" },
            { MenuOptions.ReadFile, "Read File" },
        };
}


