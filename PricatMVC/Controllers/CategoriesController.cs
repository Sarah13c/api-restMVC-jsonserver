using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PricatMVC.Models;
using PricatMVC.Services;

namespace PricatMVC.Controllers
{
    public class CategoriesController : Controller

    {

        private static List<Category>? categoryList = null!;
        private static int numCategories;


        private static CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: CategoriesController
        public async Task<ActionResult> Index(int pg = 1)
        {
            categoryList = await _categoryService.GetAll();
            const int pageSize = 3;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = categoryList.Count;
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = categoryList.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            //return View(productList);
            return View(data);
        }

        // GET: CategoriesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var categoryFound = await _categoryService.GetById(id);

            if (categoryFound == null)
            {
                return NotFound();
            }

            return View(categoryFound);
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                var categoryFound = await _categoryService.CreateCategory(category);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)

        {
            var categoryFound = await _categoryService.GetById(id);

            if (categoryFound == null)
            {
                return NotFound();
            }

            return View(categoryFound);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category category)
        {
            try
            {
                var categoryFound = await _categoryService.EditCategory(id, category);
                if (categoryFound == null)
                {
                    return View();
                }
                categoryFound.Description = category.Description;
                return RedirectToAction(nameof(Index));
                
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var categoryFound = await _categoryService.GetById(id);

            if (categoryFound == null)
            {
                return NotFound();
            }

            return View(categoryFound);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Category category)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
