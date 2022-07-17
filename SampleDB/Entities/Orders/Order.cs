using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDB.Entities.Orders
{
    public class Order
    {
        public Order()
        {
            OrderLines = new List<OrderLine>();
        }
        public Guid Id { get; set; }

        public int OrderNumber { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; }
    }
}
