using AspNetCoreWebApplication.Data;
using AspNetCoreWebApplication.Entities;
using AspNetCoreWebApplication.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public ProductsController(DatabaseContext context)
        {
            _databaseContext = context;
        }

        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            return View(await _databaseContext.Products.Include(c => c.Category).Include(b => b.Brand).ToListAsync());
        }

        // GET: ProductsController/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _databaseContext.Categories.ToListAsync(), "Id", "Name"); //view de Categori nin ismini göstermek için
            ViewBag.BrandId = new SelectList(await _databaseContext.Brands.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    product.Image = await FileHelper.FileLoaderAsync(Image);
                    await _databaseContext.Products.AddAsync(product);
                    await _databaseContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }

            }
            ViewBag.CategoryId = new SelectList(await _databaseContext.Categories.ToListAsync(), "Id", "Name"); //view de Categori nin ismini göstermek için
            ViewBag.BrandId = new SelectList(await _databaseContext.Brands.ToListAsync(), "Id", "Name");
            return View(product);
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Product product = await _databaseContext.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.CategoryId = new SelectList(await _databaseContext.Categories.ToListAsync(), "Id", "Name"); 
            ViewBag.BrandId = new SelectList(await _databaseContext.Brands.ToListAsync(), "Id", "Name");
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product, IFormFile? Image, bool resmiSil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(resmiSil)
                    {
                        FileHelper.FileRemover(product.Image);
                        product.Image = string.Empty;
                    }
                    if(Image != null) product.Image = await FileHelper.FileLoaderAsync(Image);
                    _databaseContext.Products.Update(product);
                    await _databaseContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }

            }
            ViewBag.CategoryId = new SelectList(await _databaseContext.Categories.ToListAsync(), "Id", "Name"); //view de Categori nin ismini göstermek için
            ViewBag.BrandId = new SelectList(await _databaseContext.Brands.ToListAsync(), "Id", "Name");
            return View(product);
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return View(await _databaseContext.Products.Include(c => c.Category).Include(c => c.Brand).FirstOrDefaultAsync(p => p.Id == id));
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Product product)
        {
            try
            {
                _databaseContext.Products.Remove(product);
                await _databaseContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
