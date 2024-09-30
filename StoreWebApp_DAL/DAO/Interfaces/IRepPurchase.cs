using StoreWebApp_Model.Models;

namespace StoreWebApp_DAL.DAO.Interfaces
{
    public interface IRepPurchase
    {
        public List<Purchase> GetPurchases();
        public Purchase? GetPurchaseById(int? id);
        public void CreatePurchase(Purchase purchase);
        public void UpdatePurchase(Purchase purchase);
        public void DeletePurchase(int id);
        bool PurchaseExists(int? id);
    }
}
