using Microsoft.AspNetCore.Mvc;
using Mvc_Ado_EF_Dapper_Benchmark.Models;
using Mvc_Ado_EF_Dapper_Benchmark.Services;
using System.Diagnostics;

namespace Mvc_Ado_EF_Dapper_Benchmark.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdoNetService _adonetservice;
        private readonly IEFCoreService _efcoreservice;
        private readonly IDapperService _dapperservice;

        public HomeController(ILogger<HomeController> logger, IAdoNetService adonetsvc, IEFCoreService efcoresvc, IDapperService dappersvc)
        {
            _logger = logger;
            _adonetservice = adonetsvc;
            _efcoreservice = efcoresvc;
            _dapperservice = dappersvc;
        }

        public IActionResult Index()
        {
            TempData["InsertResultAdoNet"] = string.Empty;
            TempData["InsertResultEfCore"] = string.Empty;
            TempData["InsertResultDapper"] = string.Empty;
            TempData["GetResultAdoNet"] = string.Empty;
            TempData["GetResultEFCore"] = string.Empty;
            TempData["GetResultDapper"] = string.Empty;
            TempData["GetResultAdoNetJoin"] = string.Empty;
            TempData["GetResultEFCoreJoin"] = string.Empty;
            TempData["GetResultDapperJoin"] = string.Empty;
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        #region EFCore
        [HttpGet]
        public IActionResult GetProductsEFCore()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var response = _efcoreservice.GetProducts();
            stopwatch.Stop();
            TempData["GetResultEFCore"] = $"GetProductsEFCore() {response.Count} Records Selected in {stopwatch.ElapsedMilliseconds} MiliSeconds";
            return View("Index");
        }

        [HttpGet]
        public IActionResult GetProductsWithCategoryEFCore()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var response = _efcoreservice.GetProductsWithCategory();
            stopwatch.Stop();
            TempData["GetResultEFCoreJoin"] = $"GetProductsWithCategoryEFCore() {response.Count} Records Selected in {stopwatch.ElapsedMilliseconds} MiliSeconds";
            return View("Index");
        }

        [HttpPost]
        public IActionResult AddProductEfCore(int count)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int miresult = 0;
            Random ran = new Random();
            for (int i = 0; i < count; i++)
            {
                ProductModel model = new ProductModel();
                model.Name = $"Product {i}";
                model.Description = $"Description {i}";
                model.Price = i.ToString();
                model.CategoryId = ran.Next(1, 4);
                _efcoreservice.AddProduct(model);
                miresult++;
            }

            stopwatch.Stop();
            TempData["InsertResultEfCore"] = $"AddProductEfCore() {count} Records Added in {stopwatch.ElapsedMilliseconds} MiliSeconds"; ;
            return View("Index");
        }

        [HttpPost]
        #endregion

        #region AdoNet
        public IActionResult AddProductAdoNet(int count)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Random ran = new Random();
            for (int i = 0; i < count; i++)
            {
                ProductModel model = new ProductModel();
                model.Name = $"Product {i}";
                model.Description = $"Description {i}";
                model.Price = i.ToString();
                model.CategoryId = ran.Next(1, 4);
                _adonetservice.AddProduct(model);
            }

            stopwatch.Stop();
            TempData["InsertResultAdoNet"] = $"AddProductAdoNet() {count} Records Added in {stopwatch.ElapsedMilliseconds} MiliSeconds";
            return View("Index");
        }
        
        [HttpGet]
        public IActionResult GetProductsAdoNet()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var response = _adonetservice.GetProducts();
            stopwatch.Stop();
            TempData["GetResultAdoNet"] = $"GetProductsAdoNet() {response.Count} Records Selected in {stopwatch.ElapsedMilliseconds} MiliSeconds";
            return View("Index");
        }

        [HttpGet]
        public IActionResult GetProductsWithCategoryAdoNet()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var response = _adonetservice.GetProductsWithCategory();
            stopwatch.Stop();
            TempData["GetResultAdoNetJoin"] = $"GetProductsWithCategoryAdoNet() {response.Count} Records Selected in {stopwatch.ElapsedMilliseconds} MiliSeconds";
            return View("Index");
        }
        #endregion

        #region Dapper
        public IActionResult AddProductDapper(int count)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Random ran = new Random();
            for (int i = 0; i < count; i++)
            {
                ProductModel model = new ProductModel();
                model.Name = $"Product {i}";
                model.Description = $"Description {i}";
                model.Price = i.ToString();
                model.CategoryId = ran.Next(1, 4);
                _dapperservice.AddProduct(model);
            }

            stopwatch.Stop();
            TempData["InsertResultDapper"] = $"AddProductDapper() {count} Records Added in {stopwatch.ElapsedMilliseconds} MiliSeconds";
            return View("Index");
        }

        [HttpGet]
        public IActionResult GetProductsDapper()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var response = _dapperservice.GetProducts();
            stopwatch.Stop();
            TempData["GetResultDapper"] = $"GetProductsDapper() {response.Count} Records Selected in {stopwatch.ElapsedMilliseconds} MiliSeconds";
            return View("Index");
        }

        [HttpGet]
        public IActionResult GetProductsWithCategoryDapper()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var response = _dapperservice.GetProductsWithCategory();
            stopwatch.Stop();
            TempData["GetResultDapperJoin"] = $"GetProductsWithCategoryDapper() {response.Count} Records Selected in {stopwatch.ElapsedMilliseconds} MiliSeconds";
            return View("Index");
        }
        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}