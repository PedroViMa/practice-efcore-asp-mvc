using Microsoft.EntityFrameworkCore;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.Data;
using StoreWebApp_Model.Models;
using System.Data;

namespace StoreWebApp_DAL.DAO.EFCRep
{
    public class EFCPurchaseRep : IRepPurchase
    {
        private readonly StoreDbContext _dbContext;

        public EFCPurchaseRep(StoreDbContext db)
        {
            _dbContext = db;
        }

        public void CreatePurchase(Purchase purchase)
        {
            _dbContext.Add(purchase);
            _dbContext.SaveChanges();
        }

        public void DeletePurchase(int id)
        {
            var purchase = _dbContext.Purchases.Find(id);
            if (purchase != null)
            {
                _dbContext.Purchases.Remove(purchase);
            }

            _dbContext.SaveChanges();
        }

        public Purchase? GetPurchaseById(int? id)
        {
            return _dbContext.Purchases
                .Include(p => p.Product)
                .FirstOrDefault(m => m.Id == id);
        }

        public List<Purchase> GetPurchases()
        {
            return _dbContext.Purchases.Include(p => p.Product)
                .ToList();
        }

        public void UpdatePurchase(Purchase purchase)
        {
            try
            {
                _dbContext.Update(purchase);
                _dbContext.SaveChanges();
            }
            catch (DBConcurrencyException)
            {
                throw;
            }
        }

        public bool PurchaseExists(int? id)
        {
            return _dbContext.Purchases.Any(p => p.Id == id);
        }
    }
}
