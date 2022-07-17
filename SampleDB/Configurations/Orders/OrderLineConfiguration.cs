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
    public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(l => l.Id);
            builder.HasIndex(l => l.ProductId);
            builder.HasIndex(l => l.ProductName);
            builder.HasOne(l => l.Order)
                .WithMany(o => o.OrderLines)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
