using kutuphane_otomasyou.Models.table;
using kutuphane_otomasyou.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kutuphane_otomasyou.Controllers
{
    public class iletisimController : Controller
    {
        // GET: iletisim

        private databaseContextcs db = new databaseContextcs();

        [HttpGet]
        public ActionResult iletisim()
        {
            return View();
        }

        [HttpPost]
        public ActionResult iletisim(sikayetler model)
        {
            if (ModelState.IsValid)
            {
                // Veritabanına kaydet
                db.sikayetlertablosu.Add(model);
                db.SaveChanges();

                // Başarılı işlemlerden sonra formu temizlemek için boş bir model oluştur
                ModelState.Clear();
                TempData["SuccessMessage"] = "Form başarıyla gönderildi.";
                return RedirectToAction("iletisim");
            }

            // Model geçerli değilse formu yeniden göster
            return View(model);
        }

        public ActionResult Success()
        {
            return View();
        }


    }
}