using StoreWebApp_Model.Models;

namespace StoreWebApp_DAL.DAO.Interfaces
{
    public interface IRepInventory
    {
        public List<Inventory> GetInventories();
        public Inventory? GetInventoryById(int? id);
        public void CreateInventory(Inventory inventory);
        public void UpdateInventory(Inventory inventory);
        public void DeleteInventory(int id);
    }
}
