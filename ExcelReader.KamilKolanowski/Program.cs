using ExcelReader.KamilKolanowski.Controllers;
using ExcelReader.KamilKolanowski.Models;
using ExcelReader.KamilKolanowski.Repositories;
using ExcelReader.KamilKolanowski.Services;
using ExcelReader.KamilKolanowski.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExcelReader.KamilKolanowski;

class Program
{
    static async Task Main()
    {
        var builder = Host.CreateApplicationBuilder();
        var modelBuilder = new ModelBuilder();
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ExcelReaderDbContext>(opt =>
            opt.UseSqlServer(connectionString)
        );

        builder.Services.AddTransient<ViewOptions>();
        builder.Services.AddTransient<MainView>();
        builder.Services.AddTransient<ExcelReaderService>();
        builder.Services.AddTransient<ExcelReaderController>();
        builder.Services.AddTransient<IDbRepository, DbRepository>();

        modelBuilder.Entity<Sales>().Property(s => s.Currency).HasMaxLength(3);
        modelBuilder.Entity<Sales>().Property(s => s.Market).HasMaxLength(50);
        modelBuilder.Entity<Sales>().Property(s => s.ProductName).HasMaxLength(100);
        modelBuilder.Entity<Sales>().Property(s => s.SalesDate).HasColumnType("datetime2");
        modelBuilder.Entity<Sales>().Property(s => s.Total).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Sales>().Property(s => s.Price).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Sales>().Property(s => s.Discount).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Sales>().Property(s => s.Tax).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Sales>().ToTable("Sales", schema: "TCSA");

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.SetMinimumLevel(LogLevel.Warning);

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ExcelReaderDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        var mainView = app.Services.GetRequiredService<MainView>();
        await mainView.Start();
    }
}
