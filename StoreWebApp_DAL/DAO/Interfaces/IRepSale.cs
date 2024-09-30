using StoreWebApp_Model.Models;

namespace StoreWebApp_DAL.DAO.Interfaces
{
    public interface IRepSale
    {
        public List<Sale> GetSales();
        public Sale? GetSaleById(int? id);
        public void CreateSale(Sale sale);
        public void UpdateSale(Sale sale);
        public void DeleteSale(int id);
        bool SaleExists(int? id);
    }
}
