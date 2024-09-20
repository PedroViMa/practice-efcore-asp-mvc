namespace StoreWebApp_Model.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public required int Quantity { get; set; }
        public required DateTime Date { get; set; }
        public required decimal Price { get; set; }
        public required int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
