namespace TechStock.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Subtotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Total { get; set; }
        public bool IsCancelled { get; set; }

        public ICollection<SaleDetail> Details { get; set; } = new List<SaleDetail>();
    }
}
