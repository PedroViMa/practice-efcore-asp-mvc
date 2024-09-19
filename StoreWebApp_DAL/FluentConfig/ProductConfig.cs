using StoreWebApp_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreWebApp_DAL.FluentConfig
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnName("productId");

            builder.Property(p => p.Name).HasColumnName("productName")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Description).HasColumnName("productDescription")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(p => p.Price).HasColumnName("productPrice")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(p => p.QuantityInStock).HasColumnName("productStock")
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
