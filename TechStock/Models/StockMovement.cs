namespace TechStock.Models
{
    public class StockMovement
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string? Notes { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int? PurchaseId { get; set; }
        public Purchase? Purchase { get; set; }

        public int? SaleId { get; set; }
        public Sale? Sale { get; set; }
    }
}
