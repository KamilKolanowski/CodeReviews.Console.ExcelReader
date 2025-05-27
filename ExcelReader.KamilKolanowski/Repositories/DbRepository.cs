using ExcelReader.KamilKolanowski.Models;

namespace ExcelReader.KamilKolanowski.Repositories;

public class DbRepository : IDbRepository
{
    private readonly ExcelReaderDbContext _excelReaderDbContext;

    public DbRepository(ExcelReaderDbContext excelReaderDbContext)
    {
        _excelReaderDbContext = excelReaderDbContext;
    }

    public IEnumerable<Sales?> GetSales()
    {
        var allSales = _excelReaderDbContext.Sales;
        return allSales;
    }

    public Sales? GetSale(int id)
    {
        var sale = _excelReaderDbContext.Sales.Find(id);
        return sale;
    }

    public void Insert(IEnumerable<Sales> sales)
    {
        try
        {
            _excelReaderDbContext.AddRange(sales);
            _excelReaderDbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Update(Sales sales)
    {
        try
        {
            _excelReaderDbContext.Update(sales);
            _excelReaderDbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Delete(int id)
    {
        try
        {
            _excelReaderDbContext.Remove(id);
            _excelReaderDbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
