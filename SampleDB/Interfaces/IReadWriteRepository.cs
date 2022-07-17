using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleDB.Interfaces
{
    public interface IReadWriteRepository<TKey, TEntity> : IReadOnlyRepository<TKey, TEntity>
        where TKey : struct
        where TEntity : class
    {
        Task<TEntity> Create(TEntity entity, CancellationToken token = default);

        Task<TEntity> Update(TEntity entity, CancellationToken token = default);

        Task<bool> Delete(TEntity entity, CancellationToken token = default);
    }
}
