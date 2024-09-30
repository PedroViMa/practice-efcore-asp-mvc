using StoreWebApp_Model.Models;

namespace StoreWebApp_DAL.DAO.Interfaces
{
    public interface IRepProduct
    {
        public List<Product> GetProducts();
        public Product? GetProductById(int? id);
        public void CreateProduct(Product product);
        public void UpdateProduct(Product product);
        public void DeleteProduct(int id);
        public bool ProductExists(int? id);
    }
}
