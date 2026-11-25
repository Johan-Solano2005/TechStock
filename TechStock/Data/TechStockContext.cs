using Microsoft.EntityFrameworkCore;
using TechStock.Models;

namespace TechStock.Web.Data;

public class TechStockContext : DbContext
{
    public TechStockContext(DbContextOptions<TechStockContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<ProductSupplier> ProductSuppliers { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleDetail> SaleDetails { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Real table names (singular); the DbSet properties are plural
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Supplier>().ToTable("Supplier");
        modelBuilder.Entity<ProductSupplier>().ToTable("ProductSupplier");
        modelBuilder.Entity<Purchase>().ToTable("Purchase");
        modelBuilder.Entity<PurchaseDetail>().ToTable("PurchaseDetail");
        modelBuilder.Entity<Sale>().ToTable("Sale");
        modelBuilder.Entity<SaleDetail>().ToTable("SaleDetail");
        modelBuilder.Entity<StockMovement>().ToTable("StockMovement");

        // Composite primary key: each row is identified by the product + supplier pair
        modelBuilder.Entity<ProductSupplier>()
            .HasKey(ps => new { ps.ProductId, ps.SupplierId });

        // Fixed to US English so decimals use a dot (e.g. 16000.50), as sent by the numeric inputs
        modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(10, 2);
        modelBuilder.Entity<Purchase>().Property(p => p.Total).HasPrecision(18, 2);
        modelBuilder.Entity<PurchaseDetail>().Property(d => d.UnitCost).HasPrecision(10, 2);
        modelBuilder.Entity<Sale>().Property(s => s.Subtotal).HasPrecision(18, 2);
        modelBuilder.Entity<Sale>().Property(s => s.DiscountTotal).HasPrecision(18, 2);
        modelBuilder.Entity<Sale>().Property(s => s.TaxTotal).HasPrecision(18, 2);
        modelBuilder.Entity<Sale>().Property(s => s.Total).HasPrecision(18, 2);
        modelBuilder.Entity<SaleDetail>().Property(d => d.UnitPrice).HasPrecision(10, 2);
        modelBuilder.Entity<SaleDetail>().Property(d => d.DiscountAmount).HasPrecision(10, 2);
        modelBuilder.Entity<SaleDetail>().Property(d => d.TaxRate).HasPrecision(5, 2);
        modelBuilder.Entity<SaleDetail>().Property(d => d.TaxAmount).HasPrecision(10, 2);
    }
}