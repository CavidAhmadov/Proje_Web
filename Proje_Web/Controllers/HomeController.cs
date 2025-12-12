using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proje_Web.Models;

namespace Proje_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // ? 1. Context'in özel, salt okunur bir alan?n? tan?mlay?n.
        private readonly UyeContext uyee;

        // ? 2. Context'i Kurucu Metotta (Constructor) talep edin (Enjekte edin).
        public HomeController(ILogger<HomeController> logger, UyeContext uyeContext)
        {
            _logger = logger;
            uyee = uyeContext; // Enjekte edilen Context'i alana atay?n.
        }
       
       
        public IActionResult Register()
        {       
               return View();
   
        }
        

        [HttpPost]
        public IActionResult Kaydet(Uye u)
        {
            if (ModelState.IsValid)
            {
                bool Kontrol = uyee.Uye_Tablo.Any(uye => uye.ad == u.ad);
                if (Kontrol)
                {
                   
                    return RedirectToAction("Hata");
                }
                else
                {
                    uyee.Add(u);
                    uyee.SaveChanges();
                    return RedirectToAction("Register");
                }
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Kayit(Uye u)
        {
            bool k=uyee.Uye_Tablo.Any(uye=>uye.ad==u.ad);
            bool k1 = uyee.Uye_Tablo.Any(uye => uye.parola == u.parola);
            if (k==true && k1==true)
            {
                return RedirectToAction("Main");
            }
            else
            {
                TempData["hata"] = "Oyle bir kullanici yok";

                // Kullan?c?y?, girdi?i verilerle birlikte (u) ayn? View'a geri döndür.
                // Bu, View'?n Register.cshtml oldu?unu varsayar.
                return RedirectToAction("Hata");

            }
        }
        public IActionResult Main()
        {
            return View();
        }

       public IActionResult Hata()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
