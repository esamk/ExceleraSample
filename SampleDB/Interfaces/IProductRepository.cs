using SampleDB.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDB.Interfaces
{
    public interface IProductRepository : IReadWriteRepository<Guid, Product>
    {
    }
}
