using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proje_Web.Models;

namespace Proje_Web.Controllers
{
    public class RandevuController : Controller
    {
        private readonly UyeContext _context;

        public RandevuController(UyeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var list = _context.Randevu_tablosu.ToList();
            return View(list);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Randevu randevuModeli)
        {
            // 1. Kullanıcının seçtiği hoca ve hizmete göre tüm detayları veritabanından buluyoruz
            var hocaDetay = _context.Antrenor_Tablo
                                    .FirstOrDefault(h => h.isim == randevuModeli.Egitmen &&
                                                         h.hizmet_turleri == randevuModeli.HizmetTuru);
            var hizmet = _context.Hizmet_Turleri_Tablosu.FirstOrDefault(h => h.Hizmet_turu==randevuModeli.HizmetTuru);
            if (hocaDetay != null)
            {
                // 2. Formdan gelmeyen ama randevu tablosuna kaydedilmesi gereken alanları dolduruyoruz
                randevuModeli.Uzmanlink_alani_hocanin = hocaDetay.Uzmanlik_alani;
               // randevuModeli.HizmetUcret = hizmet.Fiyat;
               // randevuModeli.HizmetAy = h.sure;
                // RandevuTarihi zaten formdan (input) geliyor olacak
            }

            if (ModelState.IsValid)
            {
                _context.Randevu_tablosu.Add(randevuModeli); // Randevu tablosuna ekle
                _context.SaveChanges();                 // Hepsini birden kaydet
                return RedirectToAction("Index");
            }

            // Hata varsa listeyi tekrar yükle
            ViewBag.EgitmenList = new SelectList(_context.Antrenor_Tablo.ToList(), "Id", "isim");
            return View(randevuModeli);
        }
        [HttpGet]

        public IActionResult GetHizmetlerByEgitmen(string egitmenIsmi)

        {

            if (string.IsNullOrEmpty(egitmenIsmi)) return Json(new List<string>());



            // Antrenör tablosundan seçilen isme ait benzersiz hizmetleri çekiyoruz

            var hizmetler = _context.Antrenor_Tablo

                                    .Where(h => h.isim == egitmenIsmi)

                                    .Select(h => h.hizmet_turleri)

                                    .Distinct()

                                    .ToList();



            return Json(hizmetler); // Örn: ["Fitness", "Boks"]

        }


    }
}
