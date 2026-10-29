namespace TechStock.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public bool IsCancelled { get; set; }

        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public ICollection<PurchaseDetail> Details { get; set; } = new List<PurchaseDetail>();
    }
}
