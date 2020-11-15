using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext Context { get; set; }
        public EFStoreRepository(StoreDbContext ctx)
        {
            Context = ctx;
        }
        public IQueryable<Product> Products => Context.Products;

        public void SaveProduct(Product p)
        {
            Context.SaveChanges();
        }

        public void CreateProduct(Product p)
        {
            Context.Add(p);
            Context.SaveChanges();
        }

        public void DeleteProduct(Product p)
        {
            Context.Remove(p);
            Context.SaveChanges();
        }
    }
}
