using Dapper;
using Mvc_Ado_EF_Dapper_Benchmark.Models;
using Mvc_Ado_EF_Dapper_Benchmark.Services;
using System.Data;
using System.Data.SqlClient;

namespace Mvc_Ado_EF_Dapper_Benchmark.Persistence.Repositories.Dapper
{
    public class ServiceDapper : IDapperService
    {
        protected readonly IConfiguration _configuration;

        public ServiceDapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ProductModel AddProduct(ProductModel product)
        {
            string sql = "INSERT INTO Products (Name,Description,Price,CategoryId) VALUES (@Name,@Description,@Price,@CategoryId)";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServerConnection")))
            {
                connection.Open();
                var result = connection.Execute(sql, product);
                return product;
            }
        }
        public List<ProductModel> GetProducts()
        {
            string sql = "SELECT * from Products ";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServerConnection")))
            {
                connection.Open();
                var result = connection.Query<ProductModel>(sql);
                return result.ToList();
            }
        }
        public List<ProductModel> GetProductsWithCategory()
        {
            List<ProductModel> products = new List<ProductModel>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlServerConnection")))
            {
                products = connection.Query<ProductModel, CategoryModel, ProductModel>
         (
           @"SELECT
            [Products].[Id],
            [Products].[Name],
            [Products].[Price],
            [Products].[CategoryId],
            [Categories].[Id],
            [Categories].[Name]
              FROM
            [Products]
              JOIN
            [Categories]
              ON
            [Categories].[Id] = [Products].[CategoryId]",
            (product, category) =>
            {
                product.Category = category;
                return product;
            }
         ).ToList();
            }
            return products;
        }



    }
}
