using ExcelReader.KamilKolanowski.Models;

namespace ExcelReader.KamilKolanowski.Repositories;

public interface IDbRepository
{
    IEnumerable<Sales?> GetSales();
    Sales? GetSale(int id);
    void Insert(IEnumerable<Sales> sales);
    void Update(Sales sales);
    void Delete(int id);
}
