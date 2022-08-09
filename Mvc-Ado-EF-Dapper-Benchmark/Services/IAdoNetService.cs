﻿using Mvc_Ado_EF_Dapper_Benchmark.Models;

namespace Mvc_Ado_EF_Dapper_Benchmark.Services;

public interface IAdoNetService
{
    List<ProductModel> GetProducts();
    List<ProductModel> GetProductsWithCategory();
    ProductModel AddProduct(ProductModel product);
}

