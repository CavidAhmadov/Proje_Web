using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje_Web.Models;

namespace Proje_Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly UyeContext _context;

        public AdminController(UyeContext context)
        {
            _context = context;
        }
        public IActionResult randevu()
        {
            var list = _context.Randevu_tablosu.ToList();
            return View(list);
        }
        public IActionResult antenor()
        {
            var liste = _context.Antrenor_Tablo.ToList();
            return View(liste);
        }
        public IActionResult hizmet()
        {
            var list = _context.Hizmet_Turleri_Tablosu.ToList();
            return View(list);
        }
        public IActionResult Add_an()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_an(Antrenor model)
        {
            // 1. Veritabanında aynı isimde başka bir hoca var mı kontrol et
            bool varMi = _context.Antrenor_Tablo.Any(h => h.isim == model.isim);

            if (varMi)
            {
                // Eğer varsa, hata mesajı ekle ve sayfayı yenilemeden kullanıcıya göster
                ModelState.AddModelError("isim", "Bu isimde bir antrenör zaten kayıtlı!");
                return View(model);
            }
            // Formdan ne gelirse gelsin, ID'yi 0 yapıyoruz ki duplicate key hatası vermesin
            model.Id = model.isim;

            if (ModelState.IsValid)
            {
                _context.Antrenor_Tablo.Add(model);
                _context.SaveChanges();
                TempData["mesaj"] = "Yeni kayıt başarıyla eklendi.";
                return RedirectToAction("antenor");
            }

            return View(model);
        }
        public IActionResult Delete_an(string id) // Parametre olarak id almalı
        {
            if (id == null)
            {
                return NotFound();
            }

            // Listeyi değil, sadece o ID'ye sahip TEK BİR kişiyi buluyoruz
            var antrenor = _context.Antrenor_Tablo.FirstOrDefault(m => m.Id == id);

            if (antrenor == null)
            {
                return NotFound();
            }

            return View(antrenor); // Tek bir modeli View'a gönderiyoruz
        }
        [HttpPost, ActionName("Delete_an")]
        public IActionResult DeleteConfirmed(string id)
        {
            // Hata ayıklama için: id'nin gelip gelmediğini kontrol et
            if (string.IsNullOrEmpty(id))
            {
                return Content("Hata: ID boş geldi!");
            }

            var antrenor = _context.Antrenor_Tablo.Find(id);
            if (antrenor != null)
            {
                _context.Antrenor_Tablo.Remove(antrenor);
                _context.SaveChanges();
                TempData["mesaj"] = "Kayıt başarıyla silindi.";
            }

            // Yönlendirme isminin doğruluğundan emin ol
            return RedirectToAction("antenor");
        }
        // 1. Düzenleme Sayfasını Açan Metot (GET)
        public IActionResult Edit(string id)
        {
            if (id == null) return NotFound();

            // Veritabanından o kişiyi bul
            var antrenor = _context.Antrenor_Tablo.Find(id);

            if (antrenor == null) return NotFound();

            return View(antrenor); // Verileri forma gönder
        }

        // 2. Form Gönderildiğinde Çalışan Metot (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Antrenor model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Veritabanında bu kaydı güncelle
                    _context.Antrenor_Tablo.Update(model);
                    _context.SaveChanges();
                    TempData["mesaj"] = "Güncelleme başarıyla tamamlandı.";
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction("antenor"); // Listeye geri dön
            }
            return View(model);
        }

        // 1. Düzenleme Sayfasını Açan Metot (GET)
        public IActionResult Edit_hi(string id)
        {
            if (id == null) return NotFound();

            // Veritabanından o hizmeti bul
            var hizmet = _context.Hizmet_Turleri_Tablosu.Find(id);

            if (hizmet == null) return NotFound();

            return View(hizmet);
        }

        // 2. Güncellemeyi Kaydeden Metot (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit_hi(HizmetTurleri model) // Model ismin neyse (Hizmet/Service) onu yaz
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Hizmet_Turleri_Tablosu.Update(model);
                    _context.SaveChanges();
                    TempData["mesaj"] = "Hizmet başarıyla güncellendi.";
                    return RedirectToAction("hizmet"); // Hizmetlerin listelendiği sayfaya dön
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
                }
            }
            return View(model);
        }

        // GET: Silme onay sayfasını açan metot
        // Buraya 'id' parametresi eklemelisin ki hangi hizmetin silineceğini bilelim
        public IActionResult Delete_hi(string id)
        {
            if (id == null) return NotFound();

            // Listeyi değil, sadece o ID'ye sahip TEK BİR hizmeti buluyoruz
            var hizmet = _context.Hizmet_Turleri_Tablosu.Find(id);

            if (hizmet == null) return NotFound();

            return View(hizmet); // View'a listeyi değil, bulduğumuz TEK hizmeti gönderiyoruz
        }

        // POST: Gerçek Silme İşlemi (Burası doğru, sadece parametre ismine dikkat)
        [HttpPost, ActionName("Delete_hi")]
        public IActionResult Delete_hi_Post(string id)
        {
            var hizmet = _context.Hizmet_Turleri_Tablosu.Find(id);
            if (hizmet != null)
            {
                _context.Hizmet_Turleri_Tablosu.Remove(hizmet);
                _context.SaveChanges();
                TempData["mesaj"] = "Hizmet başarıyla silindi.";
            }
            return RedirectToAction("hizmet");
        }
    

     // GET: Hizmet Ekleme Sayfasını Aç
     // GET: Hizmet Ekleme Sayfasını Aç
        public IActionResult Add_hi()
        {
            return View();
        }

        // POST: Yeni Hizmeti Veritabanına Kaydet
        [HttpPost]
        public IActionResult Add_hi(HizmetTurleri model)
        {
            if (ModelState.IsValid)
            {
                // Önce bu hizmet türünün zaten var olup olmadığını kontrol edelim (Duplicate Key hatası almamak için)
                var varMi = _context.Hizmet_Turleri_Tablosu.Any(x => x.Hizmet_turu == model.Hizmet_turu);

                if (varMi)
                {
                    ModelState.AddModelError("Hizmet_turu", "Bu hizmet türü zaten mevcut!");
                    return View(model);
                }

                // Veritabanına ekle
                _context.Hizmet_Turleri_Tablosu.Add(model);
                _context.SaveChanges();

                TempData["mesaj"] = "Hizmet başarıyla eklendi.";
                return RedirectToAction("hizmet"); // Liste sayfasına geri dön
            }

            // Model geçerli değilse hatalarla formu geri göster
            return View(model);
        }
        // POST: Gerçek Silme İşlemi

        public IActionResult RandevuSilOnay(int id) // Buradaki 'id' URL'den gelir (Örn: /RandevuSilOnay/5)
        {
            if (id == 0) return NotFound();

            // 1. Veritabanından o ID'ye ait randevuyu buluyoruz
            var randevu = _context.Randevu_tablosu.FirstOrDefault(x => x.Id == id);

            // 2. Eğer böyle bir kayıt yoksa hata döndür
            if (randevu == null) return NotFound();

            // 3. Bulduğumuz TEK kaydı View'a parametre olarak gönderiyoruz (En önemli kısım burası)
            return View(randevu);
        }

        [HttpPost, ActionName("RandevuSilOnay")]
        public IActionResult RandevuSilOna(int id)
        {
            var randevu = _context.Randevu_tablosu.Find(id);
            if (randevu != null)
            {
                _context.Randevu_tablosu.Remove(randevu);
                _context.SaveChanges();
                TempData["mesaj"] = "Randevu başarıyla iptal edildi/silindi.";
            }
            return RedirectToAction("randevu"); // Randevuların listelendiği action adı
        }


        // GET: Düzenleme sayfasını verilerle birlikte açar
        // GET: Verileri Form İçine Doldurur
        [HttpGet]
        public IActionResult RandevuDuzenle(int id)
        {
            var randevu = _context.Randevu_tablosu.Find(id);
            if (randevu == null) return NotFound();

            // Dropdown listelerini dolduran yardımcı metodu çağırıyoruz
            ViewBag.Egitmenler = _context.Antrenor_Tablo
                .Select(a => new SelectListItem { Value = a.isim, Text = a.isim }).ToList();

            ViewBag.Hizmetler = _context.Hizmet_Turleri_Tablosu
                .Select(h => new SelectListItem { Value = h.Hizmet_turu, Text = h.Hizmet_turu }).ToList();

            return View(randevu);
        }

        // POST: Sadece Değişen Alanları Günceller
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuDuzenle(Randevu model)
        {
            try
            {
                // Önce veritabanındaki mevcut kaydı çekiyoruz (Takip hatası almamak için)
                var mevcutKayit = _context.Randevu_tablosu.FirstOrDefault(x => x.Id == model.Id);

                if (mevcutKayit != null)
                {
                    // Sadece formdan gelen yeni değerleri atıyoruz
                    mevcutKayit.RandevuTarihi = model.RandevuTarihi;
                    mevcutKayit.Egitmen = model.Egitmen;
                    mevcutKayit.Uzmanlink_alani_hocanin = model.Uzmanlink_alani_hocanin;

                    _context.SaveChanges();
                    TempData["mesaj"] = "Randevu başarıyla güncellendi.";
                    return RedirectToAction("randevu");
                }
            }
            catch (Exception ex)
            {
                // Eğer hala hata alıyorsan mesajı burada görebiliriz
                ModelState.AddModelError("", "Hata: " + ex.Message);
            }

            // Hata varsa listeleri tekrar doldur ki sayfa patlamasın
            ViewBag.Egitmenler = _context.Antrenor_Tablo
                .Select(a => new SelectListItem { Value = a.isim, Text = a.isim }).ToList();
            ViewBag.Hizmetler = _context.Hizmet_Turleri_Tablosu
                .Select(h => new SelectListItem { Value = h.Hizmet_turu, Text = h.Hizmet_turu }).ToList();

            return View(model);
        }
    }

}
