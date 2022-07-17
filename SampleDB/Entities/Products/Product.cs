using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDB.Entities.Products
{
    public class Product
    {
        public Guid Id { get; set; }

        public int ProductNumber { get; set; }

        public string Name { get; set; }
    }
}
