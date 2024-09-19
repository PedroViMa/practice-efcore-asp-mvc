using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreWebApp_Model.Models;

namespace StoreWebApp_DAL.FluentConfig
{
    internal class InventoryConfig : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventory");

            builder.HasKey(x => x.Id);
            builder.Property(i => i.Id).HasColumnName("inventoryId");

            builder.Property(i => i.Quantity).HasColumnName("inventoryQty")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(i => i.Date).HasColumnName("inventoryLastUpdate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(i => i.ProductId).HasColumnName("productId");
            builder.HasOne(i => i.Product).WithOne(i => i.Inventory)
                .HasForeignKey<Inventory>(i => i.ProductId);
        }
    }
}
