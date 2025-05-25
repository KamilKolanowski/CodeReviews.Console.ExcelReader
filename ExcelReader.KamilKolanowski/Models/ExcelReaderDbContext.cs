using Microsoft.EntityFrameworkCore;

namespace ExcelReader.KamilKolanowski.Models;

public class ExcelReaderDbContext : DbContext
{
    public ExcelReaderDbContext(DbContextOptions<ExcelReaderDbContext> options)
        : base(options) { }

    DbSet<Sales> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TCSA");
        base.OnModelCreating(modelBuilder);
    }
}
