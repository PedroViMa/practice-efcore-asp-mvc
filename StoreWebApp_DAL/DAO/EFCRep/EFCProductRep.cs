using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.Data;
using StoreWebApp_Model.Models;
using System.Data;

namespace StoreWebApp_DAL.DAO.EFCRep
{
    public class EFCProductRep : IRepProduct
    {
        private readonly StoreDbContext _dbContext;

        public EFCProductRep(StoreDbContext db)
        {
            _dbContext = db;
        }

        public List<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public void CreateProduct(Product product)
        {
            _dbContext.Add(product);
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
            }

            _dbContext.SaveChanges();
        }

        public Product? GetProductById(int? id)
        {
            return _dbContext.Products
                .FirstOrDefault(m => m.Id == id);
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                _dbContext.Update(product);
                _dbContext.SaveChanges();
            }
            catch (DBConcurrencyException)
            {
                throw;
            }
        }

        public bool ProductExists(int? id)
        {
            return _dbContext.Products.Any(m => m.Id == id);
        }
    }
}
