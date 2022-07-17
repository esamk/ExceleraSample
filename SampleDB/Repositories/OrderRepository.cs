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
    public class OrderRepository : RepositoryBase<Guid, Order>, IOrderRepository
    {
        public OrderRepository(IContext context)
            :base(context)
        { }

        public async Task<Order> Create(Order entity, CancellationToken token = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (Guid.Empty.Equals(entity.Id))
            {
                entity.Id = Guid.NewGuid();
            }
            else
            {
                var product = await FindByIdAsync(entity.Id);
                if (!(product is null))
                {
                    throw new InvalidOperationException("Order exists already.");
                }
            }
            Context.Orders.Add(entity);
            return entity;
        }

        public async Task<bool> Delete(Order entity, CancellationToken token = default)
        {
            // Varmistetaan, että tuote on olemassa.
            var order = await FindByIdAsync(entity.Id);
            if (order is null)
            {
                throw new ArgumentException("No such an order.", nameof(entity));
            }
            Context.Orders.Remove(order);
            return true;
        }

        public override async Task<IEnumerable<Order>> FindByPatternAsync(string pattern, CancellationToken token = default)
        {
            return await Context.Orders
                .Include(o => o.OrderLines)
                .Where(o => o.OrderNumber.ToString().Contains(pattern))
                .ToListAsync();
        }

        public override async Task<IEnumerable<Order>> ListAsync(int skip, int take, bool descending = false, CancellationToken token = default)
        {
            var query = Context.Orders
                .Include(o => o.OrderLines)
                .AsQueryable();

            if (descending)
            {
                query = query.OrderByDescending(p => p.OrderNumber);
            }
            else
            {
                query = query.OrderBy(p => p.OrderNumber);
            }
            query = query.Skip(skip)
                         .Take(take);
            
            return await query.ToListAsync();
        }

        public async Task<Order> Update(Order entity, CancellationToken token = default)
        {
            // Varmistetaan, että tilaus on olemassa.
            var order = await FindByIdAsync(entity.Id);
            if (order is null)
            {
                throw new ArgumentException("No such an order.", nameof(entity));
            }
            Context.Orders.Update(order);
            return entity;
        }
    }
}
