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
        public IActionResult Create()
        {
            // Egitmen listesini ViewBag ile View'a gönder
            ViewBag.EgitmenList = new SelectList(_context.Antrenor_Tablo.ToList(), "Id", "isim");
           // ViewBag.EgitmenList1 = _context.Antrenor_Tablo.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Randevu randevuModeli)
        {
            // 1. Hocayı ismine göre buluyoruz
            var hocaDetay = _context.Antrenor_Tablo
                                    .FirstOrDefault(h => h.isim == randevuModeli.Egitmen);

            // 2. Seçilen hizmetin detaylarını buluyoruz
            var hizmetDetay = _context.Hizmet_Turleri_Tablosu
                                       .FirstOrDefault(h => h.Hizmet_turu == randevuModeli.HizmetTuru);

            if (hocaDetay != null && hizmetDetay != null)
            {
                // Alanları dolduruyoruz
                randevuModeli.Uzmanlink_alani_hocanin = hocaDetay.Uzmanlik_alani;
                randevuModeli.HizmetUcret = hizmetDetay.Fiyat;
                randevuModeli.HizmetAy = hizmetDetay.sure;

                // --- HATA BURADAYDI ---
                // PostgreSQL (Npgsql) 'timestamp with time zone' sütunu için UTC bekler.
                // Tarihi UTC olarak işaretliyoruz.
                randevuModeli.RandevuTarihi = DateTime.SpecifyKind(randevuModeli.RandevuTarihi, DateTimeKind.Utc);

                try
                {
                    _context.Randevu_tablosu.Add(randevuModeli);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Veritabanı hatası alırsanız buraya düşer, hatayı debug edebilirsiniz.
                    ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu: " + ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "Seçilen eğitmen veya hizmet bulunamadı.");
            }

            // Hata varsa listeleri tekrar doldurmamız gerekir (View'da Dropdown'lar boş kalmasın diye)
            ViewBag.EgitmenList = new SelectList(_context.Antrenor_Tablo.ToList(), "Id", "isim");
            // Eğer Hizmet listesi de dropdown ise onu da yenilemelisiniz.

            return View(randevuModeli);
        }

        [HttpGet]
        public IActionResult KontrolEt(string egitmenIsmi, DateTime secilenTarih)
        {
            // 1. Hocayı bul
            var hoca = _context.Antrenor_Tablo.FirstOrDefault(h => h.isim == egitmenIsmi);
            if (hoca == null)
                return Json(new { uygun = false, mesaj = "Lütfen önce geçerli bir hoca seçiniz." });

            // 2. Seçilen zamanın gününü ve saatini al
            DayOfWeek gun = secilenTarih.DayOfWeek;
            TimeSpan secilenSaat = secilenTarih.TimeOfDay;

            // 3. Salon Çalışma Saatlerini String Olarak Tanımla
            string haftaIciAralik = "07:00-23:00";
            string cumartesiAralik = "09:00-21:00";
            string aktifSalonAraligi = "";

            // --- GÜN KONTROLÜ ---
            if (gun == DayOfWeek.Sunday)
            {
                return Json(new { uygun = false, mesaj = "Salonumuz Pazar günleri kapalıdır." });
            }
            else if (gun == DayOfWeek.Saturday)
            {
                aktifSalonAraligi = cumartesiAralik;
            }
            else // Pazartesi - Cuma
            {
                aktifSalonAraligi = haftaIciAralik;
            }

            // --- SALON SAAT KONTROLÜ (String Split) ---
            var salonParcalar = aktifSalonAraligi.Split('-');
            TimeSpan salonBas = TimeSpan.Parse(salonParcalar[0].Trim());
            TimeSpan salonBit = TimeSpan.Parse(salonParcalar[1].Trim());

            if (secilenSaat < salonBas || secilenSaat >= salonBit)
            {
                return Json(new { uygun = false, mesaj = $"Salonumuz bu gün sadece {aktifSalonAraligi} saatleri arasında hizmet vermektedir." });
            }

            // --- HOCA ÖZEL SAAT KONTROLÜ (String Split) ---
            if (!string.IsNullOrEmpty(hoca.musaitlik_saatleri))
            {
                try
                {
                    var hocaParcalar = hoca.musaitlik_saatleri.Split('-');
                    TimeSpan hocaBas = TimeSpan.Parse(hocaParcalar[0].Trim());
                    TimeSpan hocaBit = TimeSpan.Parse(hocaParcalar[1].Trim());

                    if (secilenSaat < hocaBas || secilenSaat >= hocaBit)
                    {
                        return Json(new { uygun = false, mesaj = $"Hoca bu saatte çalışmamaktadır. Müsaitlik: {hoca.musaitlik_saatleri}" });
                    }
                }
                catch
                {
                    return Json(new { uygun = false, mesaj = "Hoca saat formatı hatalı (Örn: 10:00-14:00)." });
                }
            }

            // --- DOLULUK KONTROLÜ (Veritabanı) ---
            var utcTarih = DateTime.SpecifyKind(secilenTarih, DateTimeKind.Utc);
            bool doluMu = _context.Randevu_tablosu.Any(r =>
                r.Egitmen == egitmenIsmi && r.RandevuTarihi == utcTarih);

            if (doluMu)
            {
                return Json(new { uygun = false, mesaj = "Bu saatte hocanın başka bir randevusu mevcut." });
            }

            // Her şey uygunsa
            return Json(new { uygun = true });
        }

        [HttpGet]
        public IActionResult GetHizmetlerByEgitmen(string egitmenIsmi)
        {
            if (string.IsNullOrEmpty(egitmenIsmi)) return Json(new List<string>());

            var hizmetler = _context.Antrenor_Tablo
                .Where(h => h.isim == egitmenIsmi)
                .Select(h => h.hizmet_turleri)
                .ToList() // Veriyi önce hafızaya alıyoruz
                .SelectMany(x => x.Split(',')) // Virgüllerden parçalıyoruz
                .Select(s => s.Trim()) // Boşlukları temizliyoruz
                .Distinct()
                .ToList();

            return Json(hizmetler);
        }


    }
}
