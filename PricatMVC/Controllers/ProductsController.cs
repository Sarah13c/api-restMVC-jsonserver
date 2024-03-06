using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PricatMVC.Models;
using PricatMVC.Services;
using System.Net.Http;


namespace PricatMVC.Controllers
{
    public class ProductsController : Controller
    {

        private static List<Product>? productList = null!;
        private static int numProducts;


        private static ProductService _productService;
        private static CategoryService _categoryService;

        public ProductsController(ProductService productService, CategoryService categoryService)
        {

            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: ProductsController

        public async Task<ActionResult> Index(bool filterByCategory = false, int categoryId = 0)
        {
            productList = await _productService.GetAll();
            var categories = await _categoryService.GetAll();
            categories.Insert(0, new Category { Id = 0, Description = "Todos" });
            ViewBag.Categories = new SelectList(categories, "Id", "Description", categoryId);

            if (filterByCategory && categoryId != 0)
            {
                productList = productList.Where(x => x.CategoryId == categoryId).ToList();
            }

            return View(productList);
        }

        [HttpPost]
        public async Task<ActionResult> Index(int categoryId)
        {
            return RedirectToAction("Index", new { filterByCategory = true, categoryId });
        }


        // GET: ProductsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var productFound = await _productService.GetById(id);

            if (productFound == null)
            {
                return NotFound();
            }

            return View(productFound);
        }


        // GET: ProductsController/Create
        public async Task<IActionResult> Create(int categoryId = 0)
        {
            var categories = await _categoryService.GetAll();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Description", categoryId);
            var product = new Product { CategoryId = categoryId };
            return View(product);
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
               var productFound  = await _productService.CreateProduct(product);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(int id)

        {
            var productFound = await _productService.GetById(id);

            if (productFound == null)
            {
                return NotFound();
            }

            return View(productFound);
        }
        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product product)
        {
            try
            {
               var productFound = await _productService.EditProduct(id, product);
                if (productFound == null)
                {
                    return View();
                }
                productFound.CategoryId = product.CategoryId;
                productFound.Description = product.Description;
                productFound.EanCode = product.EanCode;
                productFound.Price = product.Price;
                productFound.Unit = product.Unit;
                productFound.Category = product.Category;

                return RedirectToAction(nameof(Index));
                
                
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var productFound = await _productService.GetById(id);

            if (productFound == null)
            {
                return NotFound();
            }

            return View(productFound);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Product product)
        {
            try
            {
                _productService.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> NuevoBoton(int categoryId)
        {
            return RedirectToAction("Index", new { filterByCategory = true, categoryId });
        }
    }
}
