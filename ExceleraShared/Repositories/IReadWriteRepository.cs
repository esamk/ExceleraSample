using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceleraShared.Repositories
{
    public interface IReadWriteRepository<TKey, TEntity> 
        : IReadOnlyRepository<TKey, TEntity>
    {

    }
}
