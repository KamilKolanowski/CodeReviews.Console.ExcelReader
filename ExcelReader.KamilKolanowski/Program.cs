using ExcelReader.KamilKolanowski.Controllers;
using ExcelReader.KamilKolanowski.Models;
using ExcelReader.KamilKolanowski.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExcelReader.KamilKolanowski;

class Program
{
    static void Main()
    {
        var builder = Host.CreateApplicationBuilder();
        var modelBuilder = new ModelBuilder();
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ExcelReaderDbContext>(opt =>
            opt.UseSqlServer(connectionString)
        );

        builder.Services.AddTransient<ExcelReaderService>();
        builder.Services.AddTransient<ExcelReaderController>();

        modelBuilder.Entity<Sales>().Property(s => s.Currency).HasMaxLength(3);
        modelBuilder.Entity<Sales>().Property(s => s.Market).HasMaxLength(50);
        modelBuilder.Entity<Sales>().ToTable("Sales", schema: "TCSA");

        var app = builder.Build();
        var controller = app.Services.GetService<ExcelReaderController>();

        controller.ProcessFile("sales.xlsx");

        app.Run();
    }
}
