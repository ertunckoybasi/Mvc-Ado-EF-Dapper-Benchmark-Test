using Mvc_Ado_EF_Dapper_Benchmark.Models;
using Persistence;

namespace Mvc_Ado_EF_Dapper_Benchmark.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    CategoryModel category = new CategoryModel { Name = $"Category {i}" };
                    context.Categories.Add(category);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
