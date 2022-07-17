using SampleDB.Contexts;
using SampleDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleDB.Repositories
{
    public abstract class RepositoryBase<TKey, TEntity> : IReadOnlyRepository<TKey, TEntity>, IRepository
        where TKey : struct
        where TEntity : class
    {
        private readonly SampleContext _ctx;
        protected RepositoryBase(IContext context)
        {
            _ctx = (context as SampleContext) ?? throw new ArgumentNullException(nameof(context));
        }

        public SampleContext Context => _ctx;

        public async Task<TEntity> FindByIdAsync(TKey key, CancellationToken token = default)
        {
            return await _ctx.FindAsync(typeof(TEntity), key) as TEntity;
        }

        public abstract Task<IEnumerable<TEntity>> FindByPatternAsync(string pattern, CancellationToken token = default);

        public abstract Task<IEnumerable<TEntity>> ListAsync(int skip, int take, bool descending = false, CancellationToken token = default);

        public Task<int> SaveAsync(CancellationToken token = default)
        {
            return _ctx.SaveChangesAsync(token);
        }
    }
}
