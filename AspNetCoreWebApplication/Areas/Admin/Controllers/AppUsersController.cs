using AspNetCoreWebApplication.Data;
using AspNetCoreWebApplication.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class AppUsersController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public AppUsersController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // GET: AppUsersController
        public async Task<ActionResult> Index() //async ifadesi asenkron çalışacağını ifade eder
        {
            return View(await _databaseContext.AppUsers.ToListAsync());
        }

        // GET: AppUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUsersController/Create
        public ActionResult Create() // Get metotları sayfa ilk açıldığında çalışan metotlardır
        {
            return View(); // Eğer sayfa ilk açıldığında view a bir veri göndermemiz gerekirse bu blokta göndermeliyiz.
        }

        // POST: AppUsersController/Create
        [HttpPost] //bu metoda istek gelir.
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppUser appUser) //Parametrelerin içi dolduruluyor.
        {
            if (ModelState.IsValid) //burada durum kontrol ediliyor, boş geçilen alan yoksa aşaıdaki bloklardan devam eder.
            {
                try
                {
                    //appuser den gelen bilgiyi burada veri tabanına gönderme ve kaydetme işelemi yapacağız.
                    await _databaseContext.AppUsers.AddAsync(appUser); // gelen appUser nesnesini _databaseContext e eklemek üzere işeretliyor. (artık appUser nesnesi veritabanına gönderilir.)
                    await _databaseContext.SaveChangesAsync(); //eklenen nesnenin kaydedilmesi için de bu metodu kullanıyoruz. (gönderilen nesne üzerinden değişlik uygulanır,kaydeliri)
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError(" ", "Hata Oluştu");
                }
            }
            return View(appUser);
        }

        // GET: AppUsersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            // Ampülden make method async yi seçiyoruz
            return View(await _databaseContext.AppUsers.FindAsync(id)); // FindAsync metodu kendisine verdiğimiz id ye sahip kaydı veritabanından bulup bize getirir
        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AppUser appUser )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _databaseContext.Entry(appUser).State = EntityState.Modified;
                    await _databaseContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError(" ", "Hata Oluştu");
                }
            }
            return View(appUser);
        }

        // GET: AppUsersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _databaseContext.AppUsers.FindAsync(id));
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, AppUser appUser)
        {
            try
            {
                _databaseContext.Entry(appUser).State = EntityState.Deleted;
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
