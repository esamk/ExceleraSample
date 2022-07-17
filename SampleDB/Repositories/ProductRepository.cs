using Microsoft.EntityFrameworkCore;
using SampleDB.Entities.Products;
using SampleDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleDB.Repositories
{
    public class ProductRepository : RepositoryBase<Guid, Product>, IProductRepository
    {
       
        public ProductRepository(IContext context)
            : base(context)
        {}

        public async Task<Product> Add(Product entity, CancellationToken token = default)
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
                    throw new InvalidOperationException("Product exists already.");
                }
            }
            Context.Products.Add(entity);
            return entity;
        }

        public Task<Product> Create()
        {
            return Task.FromResult(new Product());
        }

        public async Task<bool> Delete(Product entity, CancellationToken token = default)
        {
            // Varmistetaan, että tuote on olemassa.
            var product = await FindByIdAsync(entity.Id);
            if (product is null)
            {
                throw new ArgumentException("No such a product.", nameof(entity));
            }
            Context.Products.Remove(product);
            return true;
        }

        public override async Task<IEnumerable<Product>> FindByPatternAsync(string pattern, CancellationToken token = default)
        {
            return await Context.Products
                .Where(p => p.Name.ToUpper().Contains(pattern.ToUpper()))
                .ToListAsync();
        }

        public override async Task<IEnumerable<Product>> ListAsync(int skip, int take, bool descending = false, CancellationToken token = default)
        {
            var query = Context.Products.AsQueryable();
                           
            if (descending)
            {
                query = query.OrderByDescending(p => p.Name);
            }
            else
            {
                query = query.OrderBy(p => p.Name);
            }
            query = query.Skip(skip)
                         .Take(take);
            return await query.ToListAsync();
        }

        public async Task<Product> Update(Product entity, CancellationToken token = default)
        {
            // Varmistetaan, että tuote on olemassa.
            var product = await FindByIdAsync(entity.Id);
            if (product is null)
            {
                throw new ArgumentException("No such a product.", nameof(entity));
            }
            Context.Products.Update(entity);
            return entity;
        }
    }
}
