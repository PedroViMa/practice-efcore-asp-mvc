using StoreWebApp_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreWebApp_DAL.FluentConfig
{
    internal class PurchaseConfig : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");

            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnName("purchaseId");

            builder.Property(p => p.Quantity).HasColumnName("purchaseQty")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Price).HasColumnName("purchasePrice")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(p => p.Date).HasColumnName("purchaseDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(p => p.ProductId).HasColumnName("productId");
            builder.HasOne(p => p.Product).WithMany(p => p.Purchases)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
