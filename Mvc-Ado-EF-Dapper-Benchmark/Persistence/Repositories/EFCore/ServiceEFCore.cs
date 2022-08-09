using Microsoft.EntityFrameworkCore;
using Mvc_Ado_EF_Dapper_Benchmark.Models;
using Mvc_Ado_EF_Dapper_Benchmark.Services;
using Persistence;

namespace Mvc_Ado_EF_Dapper_Benchmark.Persistence.Repositories.EFCore
{
    public class ServiceEFCore : IEFCoreService
    {
        protected ApplicationDbContext _context;

        public ServiceEFCore(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProductModel AddProduct(ProductModel product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }
        public List<ProductModel> GetProducts()
        {
           return _context.Products.AsNoTracking().ToList();
        }

        public List<ProductModel> GetProductsWithCategory()
        {
           return _context.Products.Include(t=>t.Category).AsNoTracking().ToList();    
        }
    }
}
