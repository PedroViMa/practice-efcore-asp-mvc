using Microsoft.EntityFrameworkCore;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.Data;
using StoreWebApp_Model.Models;
using System.Data;

namespace StoreWebApp_DAL.DAO.EFCRep
{
    public class EFCSaleRep : IRepSale
    {
        private readonly StoreDbContext _dbContext;

        public EFCSaleRep(StoreDbContext db)
        {
            _dbContext = db;
        }

        public List<Sale> GetSales()
        {
            return _dbContext.Sales.Include(s => s.Product).ToList();
        }

        public void CreateSale(Sale sale)
        {
            _dbContext.Add(sale);
            _dbContext.SaveChanges();
        }

        public void DeleteSale(int id)
        {
            var sale = _dbContext.Sales.Find(id);
            if (sale != null)
            {
                _dbContext.Sales.Remove(sale);
            }

            _dbContext.SaveChanges();
        }

        public Sale? GetSaleById(int? id)
        {
            return _dbContext.Sales
                .Include(s => s.Product)
                .FirstOrDefault(m => m.Id == id);
        }

        public void UpdateSale(Sale sale)
        {
            try
            {
                _dbContext.Update(sale);
                _dbContext.SaveChanges();
            }
            catch (DBConcurrencyException)
            {
                throw;
            }

        }

        public bool SaleExists(int? id)
        {
            return _dbContext.Sales.Any(p => p.Id == id);
        }
    }
}
