using System.ComponentModel.DataAnnotations;

namespace Mvc_Ado_EF_Dapper_Benchmark.Models;

public class ProductCategoryModel
{
    [Key]
    public int Id { get; set; }
    [MaxLength(250)]
    public string Name { get; set; }
    [MaxLength(250)]
    public string Description { get; set; }
    [MaxLength(20)]
    public string Price { get; set; }
    public int CategoryId { get; set; }
    public CategoryModel Category { get; set; }
}

