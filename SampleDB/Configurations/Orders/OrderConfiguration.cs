using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleDB.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDB.Configurations.Orders
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasIndex(o => o.OrderNumber).IsUnique(true);
            builder.Property(o => o.OrderNumber)
                .HasDefaultValueSql("NEXT VALUE FOR OrderNumbers");
            builder.HasMany(o => o.OrderLines)
                .WithOne(l => l.Order)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
