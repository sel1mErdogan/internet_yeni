using kutuphane_otomasyou.Models.table.kitaplar;
using kutuphane_otomasyou.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kutuphane_otomasyou.Models.table;
using kutuphane_otomasyou.Models.table.kisiler;
using System.Runtime.ConstrainedExecution;

namespace kutuphane_otomasyou.Controllers
{
    public class EkleSilController : Controller
    {
        // GET: EkleSil,
        //[HttpPost]
        [Authorize]
        public ActionResult ekle(string kitap_adi, string yazar, string turu, string ozet, int? sayfa_sayisi, string resimi, int? yili)
        {
            ViewBag.Turler = new List<SelectListItem>
             {
                  new SelectListItem { Value = "Roman", Text = "Roman" },
                 new SelectListItem { Value = "Bilim Kurgu", Text = "Bilim Kurgu" },
                 new SelectListItem { Value = "Tarih", Text = "Tarih" },
                 new SelectListItem { Value = "Biyografi", Text = "Biyografi" },
                 new SelectListItem { Value = "Korku", Text = "Korku" },
                 new SelectListItem { Value = "Macera", Text = "Macera" },
                 new SelectListItem { Value = "Polisiye", Text = "Polisiye" },
                 new SelectListItem { Value = "Fantastik", Text = "Fantastik" },
                 new SelectListItem { Value = "Şiir", Text = "Şiir" },
                 new SelectListItem { Value = "Klasik", Text = "Klasik" },
                 new SelectListItem { Value = "Felsefe", Text = "Felsefe" },
                 new SelectListItem { Value = "Kurgu Dışı", Text = "Kurgu Dışı" },
                 new SelectListItem { Value = "Oyun", Text = "Oyun" },
                 new SelectListItem { Value = "Çocuk", Text = "Çocuk" },
             };

            if (Session["gizli"] != null)
            {
                if (!string.IsNullOrEmpty(kitap_adi) && !string.IsNullOrEmpty(yazar) && !string.IsNullOrEmpty(turu) && !string.IsNullOrEmpty(ozet) && !string.IsNullOrEmpty(resimi) && sayfa_sayisi.HasValue && yili.HasValue)
                {
                    if (yili.Value > 0 && sayfa_sayisi.Value > 0)
                    {
                        databaseContextcs db = new databaseContextcs();
                        var yeni_kitap = new Kitap
                        {
                            kitap_adi = kitap_adi,
                            yazar = yazar,
                            turu = turu,
                            ozet = ozet,
                            resimi = resimi,
                            yili = yili.Value,
                            sayfa_sayisi = sayfa_sayisi.Value,
                            
                        };

                        db.kitaptablosu.Add(yeni_kitap);
                        db.SaveChanges();

                        return View(); // Metodu tamamlayan dönüş ifadesi
                    }

                }
                if (!string.IsNullOrEmpty(kitap_adi) || !string.IsNullOrEmpty(yazar) || !string.IsNullOrEmpty(turu) || !string.IsNullOrEmpty(ozet) || !string.IsNullOrEmpty(resimi) || sayfa_sayisi.HasValue || yili.HasValue)
                {
                    TempData["bos"] = "bos";
                    return View(); // Metodu tamamlayan dönüş ifadesi
                }
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }
        }
    

          
     
        [Authorize]
        public ActionResult sil(string Kitapismi)
        {
            if (Session["gizli"] != null)
            {
                databaseContextcs db = new databaseContextcs();
                List<Kitap> kitaplistesi = db.kitaptablosu.ToList();
                if (Kitapismi != null)
                {


                    Kitap kitapSil = db.kitaptablosu.Where(x => x.kitap_adi == Kitapismi).FirstOrDefault();
                    db.kitaptablosu.Remove(kitapSil);
                    db.SaveChanges();




                }
                return View(kitaplistesi);
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }

        }

        [Authorize]
        public ActionResult cezaliKisiler(string kisiIsmi)
        {if (Session["gizli"] != null) { 
            databaseContextcs db = new databaseContextcs();
            List<kisi> kisiler = db.kisitablosu.ToList();
            if (kisiIsmi != null)
            {
                kisi kisiSil = db.kisitablosu.Where(x => x.ad == kisiIsmi).FirstOrDefault();

                int kisi_id = kisiSil.Id;
                List<AlinanKitaplar> kitaplar = db.AlinanKitapTaplosu.Where(x => x.kullanici_ıd == kisiSil.Id).ToList();



                foreach (var kitap in kitaplar)
                {
                    var geri_koy = new Kitap // kitaptablosu'na eklemek için doğru sınıfı kullanmalısınız
                    {
                        kitap_adi = kitap.kitap_adi,
                        yazar = kitap.yazar,
                        turu = kitap.turu,
                        ozet = kitap.ozet,
                        resimi = kitap.resimi,
                        yili = kitap.yili,
                        sayfa_sayisi = kitap.sayfa_sayisi,
                        // Diğer alanları da gerekiyorsa burada ayarlayabilirsiniz
                    };

                    db.kitaptablosu.Add(geri_koy);
                    db.AlinanKitapTaplosu.Remove(kitap);
                }

                db.SaveChanges();


                if (kisiSil != null)
                {
                    db.kisitablosu.Remove(kisiSil);
                }
               
               

                var cezalandi = new CezaliKisiler
                {
                    ad = kisiSil.ad,
                    soyad = kisiSil.soyad,
                    email = kisiSil.email,
                    sifre = kisiSil.sifre,
            


                };
                db.CezaliKisilertablosu.Add(cezalandi);
                db.SaveChanges();


                return RedirectToAction("cezaliKisiler","EkleSil");

            }
            return View(kisiler);
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }

        }
        [Authorize]
        public ActionResult admin_paneli()
        {
            if (Session["gizli"] != null)
            {
                databaseContextcs db = new databaseContextcs();
                int count = db.kisitablosu.Count();
                int count2 = db.CezaliKisilertablosu.Count();
                int count3 = db.kitaptablosu.Count();
                int count4 = db.AlinanKitapTaplosu.Count();
                TempData["kisi sayisi"] = count.ToString();
                TempData["Cezali Kisiler sayisi"] = count2.ToString();
                TempData["kitap sayisi"] = count3.ToString();
                TempData["Alinan Kitap sayisi"] = count4.ToString();

                return View();
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }

        }
        [Authorize]
        public ActionResult AlinanKitaplarKisiTablo()
        {
            if (Session["gizli"] != null)
            {
                // Veritabanı bağlantısını oluştur
                databaseContextcs db = new databaseContextcs();

                // Boş model oluştur
                var model = new KisiKitapViewModel
                {
                    Kisiler = new List<kisi>(),
                    Kitaplar = new List<AlinanKitaplar>()
                };

                // Veritabanından kisileri ve kitapları çek ve modele ekle
                model.Kisiler = db.kisitablosu.ToList();
                model.Kitaplar = db.AlinanKitapTaplosu.ToList();

                // Modeli view'e ileterek göster
                return View(model);
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }



        }
        [Authorize]
        public ActionResult sikayetler()
        {
            databaseContextcs db = new databaseContextcs();
            List<sikayetler> sikayet = db.sikayetlertablosu.ToList();


            return View(sikayet); 



        }
        [Authorize]
        public ActionResult banlananKisiler()
        {

            databaseContextcs db = new databaseContextcs();

            List<CezaliKisiler> cezalilar = db.CezaliKisilertablosu.ToList();



            return View(cezalilar);
           

        }
        [Authorize]
        public ActionResult baniKaldir(string kisiIsmi)
        {
            if (!string.IsNullOrEmpty(kisiIsmi))
            {
                databaseContextcs db = new databaseContextcs();
                // Kullanıcıyı cezalı listesinden bul ve kaldır
                CezaliKisiler kisiSil = db.CezaliKisilertablosu.FirstOrDefault(x => x.ad == kisiIsmi);

                if (kisiSil != null)
                {
                    // Kullanıcıyı cezalı listesinden kaldır
                    db.CezaliKisilertablosu.Remove(kisiSil);

                    // Kullanıcıyı normal kullanıcı listesine ekle
                    var cezaKaldi = new kisi
                    {
                        ad = kisiSil.ad,
                        soyad = kisiSil.soyad,
                        email = kisiSil.email,
                        sifre = kisiSil.sifre,
                    };
                    db.kisitablosu.Add(cezaKaldi);

                    db.SaveChanges();
                }

                // İşlem tamamlandıktan sonra BanlıKullanicilar sayfasına yönlendirme yap
                return RedirectToAction("banlananKisiler", "EkleSil");
            }

            // Eğer kisiIsmi boş ise BanliKullanicilar sayfasına yönlendir
            return RedirectToAction("banlananKisiler", "EkleSil");
        }
        [Authorize]
        public ActionResult sikayetSil(int kisiId)
        {


            databaseContextcs db = new databaseContextcs();
            // Kullanıcıyı cezalı listesinden bul ve kaldır
            sikayetler sikayet = db.sikayetlertablosu.FirstOrDefault(x => x.Id == kisiId);
            db.sikayetlertablosu.Remove(sikayet);
            db.SaveChanges();
            return RedirectToAction("sikayetler", "EkleSil");


        }

    }
}