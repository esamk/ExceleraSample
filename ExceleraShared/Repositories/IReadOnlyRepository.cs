using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExceleraShared.Repositories
{
    public interface IReadOnlyRepository<TKey, TEntity>
    {
        Task<TEntity> FindByIdAsync(TKey key, CancellationToken token = default);

        Task<TEntity> FindByPatternAsync(string pattern, CancellationToken token = default);

        Task<TEntity> ListAsync(int skip, int take, bool descending = false, CancellationToken token = default);
    }
}
