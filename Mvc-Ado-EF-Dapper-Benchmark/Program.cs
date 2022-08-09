using Microsoft.EntityFrameworkCore;
using Mvc_Ado_EF_Dapper_Benchmark.Persistence;
using Mvc_Ado_EF_Dapper_Benchmark.Persistence.Repositories.AdoNet;
using Mvc_Ado_EF_Dapper_Benchmark.Persistence.Repositories.Dapper;
using Mvc_Ado_EF_Dapper_Benchmark.Persistence.Repositories.EFCore;
using Mvc_Ado_EF_Dapper_Benchmark.Services;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAdoNetService, ServiceAdoNet>();
builder.Services.AddScoped<IEFCoreService, ServiceEFCore>();
builder.Services.AddScoped<IDapperService, ServiceDapper>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   builder.Configuration.GetConnectionString("SqlServerConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)), ServiceLifetime.Singleton);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    await ApplicationDbContextSeed.SeedSampleDataAsync(db);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
