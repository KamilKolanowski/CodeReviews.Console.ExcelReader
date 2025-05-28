# Excel Reader

**Excel Reader** is a C# console application designed for efficient Excel file processing and database management. The app offers an interactive command-line UI using [Spectre.Console](https://spectreconsole.net/), leverages [EPPlus](https://github.com/EPPlusSoftware/EPPlus) for Excel operations, and utilizes [Entity Framework Core (EFCore)](https://docs.microsoft.com/en-us/ef/core/) to communicate with a database.

---

## Features

- **Add Sales Files**: Import Excel sales files (e.g., provided in the `Assets` directory) and persist it to the database.
- **Read Excel Files**: Parse and display Excel file contents in the console.
- **Read from Database**: Query and display database tables in the console.
- **Export to Excel**: Export data already saved in the database to a new `.xlsx` file.

---

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version compatible with your project)
- Access to a supported database (e.g., SQL Server, SQLite, etc.)

### Restore Packages

To restore all the required NuGet packages, run:
```sh
dotnet restore
```

### Database Migrations

To apply any pending Entity Framework Core migrations and ensure your database schema is up-to-date:
```sh
dotnet ef database update
```
> **Tip:** If you modify your data models, add new migrations using:
> ```sh
> dotnet ef migrations add <MigrationName>
> dotnet ef database update
> ```

---

## Usage

1. **Place your Excel file** (e.g., `Sales.xlsx`) in the `Assets` directory.
2. **Run the application**:
    ```sh
    dotnet run
    ```
3. **Follow the interactive UI** to:
    - Add and read sales files from Excel.
    - Save imported data to the database.
    - Read and display data from the database.
    - Export database data to a new Excel file.

---

## Project Structure

```
ExcelReader/
├── Assets/
│   └── Sales.xlsx           # Example sales file to import
├── Migrations/              # EFCore migration files
├── Program.cs               # Main entry point
├── ... (other source files)
└── README.md
```

---

## Key Technologies

- **Spectre.Console**: Beautiful and interactive console UI.
- **EPPlus**: Fast and reliable Excel file reading and writing.
- **EFCore**: Modern database access and migrations.

---

## Notes

- Ensure your database connection string is correctly configured in `appsettings.json` or equivalent.
- The app supports importing and exporting `.xlsx` files only.
- After importing an Excel file, you can immediately save it to the database via the UI.
- Once data is in the database, you can export it to a new Excel file at any time.

---

## Contributing

Pull requests and issues are welcome!

---
