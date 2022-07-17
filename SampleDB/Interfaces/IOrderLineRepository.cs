using SampleDB.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDB.Interfaces
{
    public interface IOrderLineRepository : IReadWriteRepository<Guid, OrderLine>
    {
    }
}
