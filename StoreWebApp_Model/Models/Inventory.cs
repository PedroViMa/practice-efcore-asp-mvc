namespace StoreWebApp_Model.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public required int Quantity { get; set; }
        public required DateTime Date { get; set; }
        public required int ProductId { get; set; }
        public required Product Product { get; set; }
    }
}
