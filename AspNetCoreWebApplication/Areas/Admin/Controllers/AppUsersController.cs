using AspNetCoreWebApplication.Data;
using AspNetCoreWebApplication.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppUsersController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public AppUsersController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // GET: AppUsersController
        public async Task<ActionResult> IndexAsync() //async ifadesi asenkron çalışacağını ifade eder
        {
            return View(await _databaseContext.AppUsers.ToListAsync());
        }

        // GET: AppUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return RedirectToAction(nameof(IndexAsync));
                }
                catch
                {
                    ModelState.AddModelError(" ", "Hata Oluştu");
                }
            }
            return View();
        }

        // GET: AppUsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppUsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
