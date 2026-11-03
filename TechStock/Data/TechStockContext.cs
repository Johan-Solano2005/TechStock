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
        // Nombres reales de las tablas (en singular)
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Supplier>().ToTable("Supplier");
        modelBuilder.Entity<ProductSupplier>().ToTable("ProductSupplier");
        modelBuilder.Entity<Purchase>().ToTable("Purchase");
        modelBuilder.Entity<PurchaseDetail>().ToTable("PurchaseDetail");
        modelBuilder.Entity<Sale>().ToTable("Sale");
        modelBuilder.Entity<SaleDetail>().ToTable("SaleDetail");
        modelBuilder.Entity<StockMovement>().ToTable("StockMovement");

        // Llave primaria compuesta de la tabla intermedia
        modelBuilder.Entity<ProductSupplier>()
            .HasKey(ps => new { ps.ProductId, ps.SupplierId });

        // Precisión de los decimales, igual que en las columnas de SQL
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