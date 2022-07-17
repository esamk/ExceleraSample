using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleDB.Interfaces
{
    public interface IRepository
    {

        Task<int> SaveAsync(CancellationToken token = default);
    }
}
