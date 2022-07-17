using Microsoft.EntityFrameworkCore;
using SampleDB.Entities.Orders;
using SampleDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleDB.Repositories
{
    public class OrderLineRepository : RepositoryBase<Guid, OrderLine>, IOrderLineRepository
    {
        public OrderLineRepository(IContext context)
            : base(context)
        { }

        public async Task<OrderLine> Add(OrderLine entity, CancellationToken token = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (Guid.Empty.Equals(entity.OrderId))
            {
                throw new ArgumentException("Orderline does not have parent order id set.", nameof(entity));
            }
            if (Guid.Empty.Equals(entity.Id))
            {
                entity.Id = Guid.NewGuid();
            }
            else
            {
                var line = await FindByIdAsync(entity.Id);
                if (!(line is null))
                {
                    throw new InvalidOperationException("OrderLine exists already.");
                }
            }
            Context.OrderLines.Add(entity);
            return entity;
        }

        public Task<OrderLine> Create()
        {
            return Task.FromResult(new OrderLine());
        }

        public async Task<bool> Delete(OrderLine entity, CancellationToken token = default)
        {
            // Varmistetaan, että tilausrivi on olemassa.
            var line = await FindByIdAsync(entity.Id);
            if (line is null)
            {
                throw new ArgumentException("No such an orderline.", nameof(entity));
            }
            Context.OrderLines.Remove(line);
            return true;
        }

        public override Task<IEnumerable<OrderLine>> FindByPatternAsync(string pattern, CancellationToken token = default)
        {
            // ei jaksa :)
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<OrderLine>> ListAsync(int skip, int take, bool descending = false, CancellationToken token = default)
        {
            var query = Context.OrderLines
               .Include(o => o.Order)
               .AsQueryable();

            if (descending)
            {
                query = query.OrderByDescending(o => o.Order.OrderNumber)
                             .ThenBy(p => p.LineNr);
            }
            else
            {
                query = query.OrderBy(o => o.Order.OrderNumber)
                             .ThenBy(p => p.LineNr);
            }
            query = query.Skip(skip)
                         .Take(take);

            return await query.ToListAsync();
        }

        public async Task<OrderLine> Update(OrderLine entity, CancellationToken token = default)
        {
            // Varmistetaan, että tilausrivi on olemassa.
            var line = await FindByIdAsync(entity.Id);
            if (line is null)
            {
                throw new ArgumentException("No such an orderline.", nameof(entity));
            }
            Context.OrderLines.Update(line);
            return entity;
        }
    }
}
