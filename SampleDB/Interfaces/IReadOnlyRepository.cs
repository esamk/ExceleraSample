using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleDB.Interfaces
{
    public interface IReadOnlyRepository<TKey, TEntity> : IRepository
        where TKey : struct
        where TEntity : class
    {
        Task<TEntity> FindByIdAsync(TKey key, CancellationToken token = default);

        Task<IEnumerable<TEntity>> FindByPatternAsync(string pattern, CancellationToken token = default);

        Task<IEnumerable<TEntity>> ListAsync(int skip, int take, bool descending = false, CancellationToken token = default);
    }
}
