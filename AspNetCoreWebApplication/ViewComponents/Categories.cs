﻿using AspNetCoreWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.ViewComponents
{
    public class Categories : ViewComponent // Bir class ın ViewComponent olabilmesi için ViewComponent classından miras alması gerekir.
    {
        // View Components kullanmak için ana dizindeki View > Shared klasörü içine Components isminde bir klasör eklemliyiz. Sonrasında bu klasöre de Categories isminde bir klasör ekliyoruz. Farklı komponentler kullanacaksak onlar için de bu şekilde yapmalıyız. Son olarak Categories isimli klasörün içine Default.cshtml isminde boş bir view oluşturuyoruz. Buradan veritabanından çekeceğimiz kategori listesini bu view da listeleteceğiz.
        private readonly DatabaseContext _database; // nesnemizi oluşturduktan sonra _database e sağ klik yapıp açılan menüden ampul simgesine tıklayıp, quicq action and refactoring menüsüne tıklıyoruz > Generate Constructor a tıklıyoruz 

        public Categories(DatabaseContext database) // DI
        {
            _database = database;
        }

        public async Task<IViewComponentResult> InvokeAsync() // InvokeAsync metodu asenkron bir şekilde verileri shared > components > categories altındaki default view ına gönderecek
        {
            return View(await _database.Categories.ToListAsync()); // view çalıştırılırken içinde kategori listeleyeceğimiz için modele gerekli veriyi burada gönderiyoruz.
        }

    }
}