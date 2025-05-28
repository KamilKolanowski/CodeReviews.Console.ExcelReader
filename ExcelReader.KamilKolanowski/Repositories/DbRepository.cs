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
        try
        {
            var allSales = _excelReaderDbContext.Sales;
            return allSales;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Sales? GetSale(int id)
    {
        try
        {
            var sale = _excelReaderDbContext.Sales.Find(id);
            return sale;
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
