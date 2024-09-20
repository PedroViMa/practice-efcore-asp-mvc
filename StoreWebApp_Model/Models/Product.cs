namespace StoreWebApp_Model.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public required int QuantityInStock { get; set; }
        public List<Sale>? Sales { get; set; }
        public List<Purchase>? Purchases { get; set; }
        public Inventory? Inventory { get; set; }
    }
}
