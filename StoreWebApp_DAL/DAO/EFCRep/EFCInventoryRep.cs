using Microsoft.EntityFrameworkCore;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.Data;
using StoreWebApp_Model.Models;
using System.Data;

namespace StoreWebApp_DAL.DAO.EFCRep
{
    public class EFCInventoryRep : IRepInventory
    {
        private readonly StoreDbContext _dbContext;

        public EFCInventoryRep(StoreDbContext db)
        {
            _dbContext = db;
        }

        public void CreateInventory(Inventory inventory)
        {
            _dbContext.Add(inventory);
            _dbContext.SaveChanges();
        }

        public void DeleteInventory(int id)
        {
            var inventory = _dbContext.Inventory.Find(id);
            if (inventory != null)
            {
                _dbContext.Inventory.Remove(inventory);
            }

            _dbContext.SaveChanges();
        }

        public List<Inventory> GetInventories()
        {
            return _dbContext.Inventory.Include(i => i.Product). ToList();
        }

        public Inventory? GetInventoryById(int? id)
        {
            return _dbContext.Inventory
                .Include(i => i.Product)
                .FirstOrDefault(m => m.Id == id);
        }

        public void UpdateInventory(Inventory inventory)
        {
            try
            {
                _dbContext.Update(inventory);
                _dbContext.SaveChanges();
            }
            catch (DBConcurrencyException)
            {
                throw;
            }
        }

        public bool InventoryExists(int? id)
        {
            return _dbContext.Inventory.Any(p => p.Id == id);
        }
    }
}
