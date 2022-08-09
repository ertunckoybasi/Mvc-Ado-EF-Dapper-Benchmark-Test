using Mvc_Ado_EF_Dapper_Benchmark.Models;
using Mvc_Ado_EF_Dapper_Benchmark.Services;
using System.Data;
using System.Data.SqlClient;

namespace Mvc_Ado_EF_Dapper_Benchmark.Persistence.Repositories.AdoNet
{
    public class ServiceAdoNet : IAdoNetService
    {
        protected readonly IConfiguration _configuration;

        public ServiceAdoNet(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ProductModel AddProduct(ProductModel product)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("SqlServerConnection")))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Products (Name,Description,Price,CategoryId) VALUES (@Name,@Description,@Price,@CategoryId) ", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.Parameters.AddWithValue("Name", product.Name);
                cmd.Parameters.AddWithValue("Description", product.Description);
                cmd.Parameters.AddWithValue("Price", product.Price);
                cmd.Parameters.AddWithValue("CategoryId", product.CategoryId);
                cmd.ExecuteNonQuery();
            }
            return product;
        }

        public List<ProductModel> GetProducts()
        {
            List<ProductModel> lstProductModel = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("SqlServerConnection")))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products ", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ProductModel ProductModel = new ProductModel();
                    ProductModel.Id = Convert.ToInt32(rdr["Id"]);
                    ProductModel.Name = rdr["Name"].ToString();
                    ProductModel.Description = rdr["Description"].ToString();
                    ProductModel.Price = rdr["Price"].ToString();
                    lstProductModel.Add(ProductModel);
                }
            }
            return lstProductModel;
        }

        public List<ProductModel> GetProductsWithCategory()
        {
            List<ProductModel> lstProductModel = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("SqlServerConnection")))
            {
                SqlCommand cmd = new SqlCommand("SELECT p.Id,p.Description,p.CategoryId,p.Name,p.Price,c.Id as CId,c.Name As CategoryName from     Products p INNER JOIN   Categories  c ON p.CategoryId = c.Id", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ProductModel ProductModel = new ProductModel();
                    CategoryModel CategoryModel = new CategoryModel();
                    ProductModel.Category=new CategoryModel { Id = Convert.ToInt32(rdr["CId"]), Name = rdr["CategoryName"].ToString() };  

                    ProductModel.Id = Convert.ToInt32(rdr["Id"]);
                    ProductModel.Name = rdr["Name"].ToString();
                    ProductModel.Description = rdr["Description"].ToString();
                    ProductModel.Price = rdr["Price"].ToString();
                    ProductModel.CategoryId = Convert.ToInt32(rdr["CId"].ToString());
                    lstProductModel.Add(ProductModel);
                }
            }
            return lstProductModel;
        }
    }
}
