using basic_delivery_api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace basic_delivery_api.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .ToTable("products")
                .HasKey(p => p.Id);

            builder.Entity<Product>()
                .Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("name");

            builder.Entity<Product>()
                .Property(p => p.UnitOfMeasurement)
                .IsRequired()
                .HasConversion<byte>()
                .HasColumnName("unit_of_measurement");

            builder.Entity<Sale>()
                .ToTable("sales")
                .HasKey(s => s.Id);

            builder.Entity<Sale>()
                .Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Entity<Sale>()
                .Property(s => s.SaleDate)
                .IsRequired()
                .HasColumnName("sale_date");

            builder.Entity<Sale>()
                .Property(s => s.TotalAmount)
                .IsRequired()
                .HasColumnName("total_amount");

            builder.Entity<SaleItem>()
                .ToTable("sale_items")
                .HasKey(si => si.Id);

            builder.Entity<SaleItem>()
                .Property(si => si.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Entity<SaleItem>()
                .Property(si => si.ProductId)
                .IsRequired()
                .HasColumnName("product_id");

            builder.Entity<SaleItem>()
                .Property(si => si.Quantity)
                .IsRequired()
                .HasColumnName("quantity");

            builder.Entity<SaleItem>()
                .Property(si => si.UnitPrice)
                .IsRequired()
                .HasColumnName("unit_price");

            builder.Entity<SaleItem>()
                .Property(si => si.SaleId)
                .IsRequired()
                .HasColumnName("sale_id");

            builder.Entity<Sale>()
                .HasMany(s => s.SaleItems)
                .WithOne(si => si.Sale)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SaleItem>()
                .HasOne(si => si.Product)
                .WithMany()
                .HasForeignKey(si => si.ProductId);
        }
    }
}