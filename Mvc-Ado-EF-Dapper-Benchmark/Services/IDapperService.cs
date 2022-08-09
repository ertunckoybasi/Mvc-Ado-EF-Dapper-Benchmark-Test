using Mvc_Ado_EF_Dapper_Benchmark.Models;

namespace Mvc_Ado_EF_Dapper_Benchmark.Services
{
    public interface IDapperService
    {
        List<ProductModel> GetProducts();
        ProductModel AddProduct(ProductModel product);
        List<ProductModel> GetProductsWithCategory();
    }
}
