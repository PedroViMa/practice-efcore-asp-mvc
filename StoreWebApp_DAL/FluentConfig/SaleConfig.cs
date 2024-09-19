using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreWebApp_Model.Models;

namespace StoreWebApp_DAL.FluentConfig
{
    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sale");

            builder.HasKey(x => x.Id);
            builder.Property(s => s.Id).HasColumnName("saleId");

            builder.Property(s => s.Quantity).HasColumnName("saleQty")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(s => s.Price).HasColumnName("salePrice")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(s => s.Date).HasColumnName("saleDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(s => s.ProductId).HasColumnName("productId");

            builder.HasOne(u => u.Product).WithMany(u => u.Sales)
                .HasForeignKey(u => u.ProductId);
        }
    }
}
