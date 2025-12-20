using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Proje_Web.Models;
using Proje_Web.Services;

namespace Proje_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // ? 1. Context'in özel, salt okunur bir alan?n? tan?mlay?n.
        private readonly UyeContext uyee;
        private readonly GeminiAIService _aiService;

        // ? 2. Context'i Kurucu Metotta (Constructor) talep edin (Enjekte edin).
        public HomeController(ILogger<HomeController> logger, UyeContext uyeContext, GeminiAIService aiService)
        {
            _logger = logger;
            uyee = uyeContext; // Enjekte edilen Context'i alana atay?n.
            _aiService = aiService;
        }

        
        public IActionResult Register()
        {       
               return View();
   
        }
        

        [HttpPost]
        public IActionResult Kaydet(Uye u)
        {
            if (ModelState.IsValid)//Sunucu tarfindan
            {
                bool Kontrol = uyee.Uye_Tablo.Any(uye => uye.ad == u.ad);
                if (Kontrol)
                {
                    TempData["hata"] = "Bu kullanici zaten var";
                    return RedirectToAction("Register");
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
        
        public IActionResult Admin()
        {
            
            var list = uyee.Uye_Tablo.ToList();
            return View(list);
        }

        [HttpPost]
        public IActionResult Kayit(Uye u)
        {
            if (u.parola=="sau" && u.ad== "b231210560@sakarya.edu.tr")
            {
                return RedirectToAction("Admin");
            }
            bool k=uyee.Uye_Tablo.Any(uye=>uye.ad==u.ad);
            bool k1 = uyee.Uye_Tablo.Any(uye => uye.parola == u.parola);

         

                if (k==true && k1==true)
            {
                return RedirectToAction("Main");
            }
            else
            {
                TempData["hata"] = "Oyle bir kullanici yok yada parola yanlis";

                // Kullan?c?y?, girdi?i verilerle birlikte (u) ayn? View'a geri döndür.
                // Bu, View'?n Register.cshtml oldu?unu varsayar.
                return RedirectToAction("Register");

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

       

        

        [HttpGet]
        public IActionResult Yapayzeka() => View(new AIRecommendationViewModel());

        [HttpPost]
        public async Task<IActionResult> Yapayzeka(AIRecommendationViewModel model)
        {
            try
            {
                // Kullan?c?n?n girdi?i metne göre API'ye talimat (Prompt) haz?rl?yoruz
                string prompt = $"Bir spor ve beslenme uzman? gibi davran. Boyu {model.BoyCm} cm, kilosu {model.KiloKg} kg olan ve vücut durumunu '{model.VucutTipi}' olarak tan?mlayan bu ki?i için: " +
                                "1- Detayl? bir haftal?k antrenman plan? haz?rla. " +
                                "2- Günlük kalori ihtiyac?na göre diyet önerileri sun. " +
                                "3- E?er bu programa sad?k kal?rsa 3 ay sonraki fiziksel de?i?imi hakk?nda detayl? tahminlerde bulun." +
                                "Yan?t?n? verirken en ba?a kullan?c? için HTML ve SVG kodlar? kullanarak görsel bir analiz ?emas? (progress bar veya vücut tipi grafi?i) çiz.Egzersizi uygulasa insan  nasil gozukcek. Markdown kullanma, sadece saf HTML etiketleri döndür";

                if (model.ResimDosyasi != null && model.ResimDosyasi.Length > 0)
                {
                    // E?er resim varsa hem metni hem resmi analiz eder
                    using var stream = model.ResimDosyasi.OpenReadStream();
                    model.VucutTahmini = await _aiService.GenerateContentAsync(prompt, stream, model.ResimDosyasi.ContentType);
                }
                else
                {
                    // Resim yoksa sadece girilen verilere (Boy, Kilo, Durum) göre plan yapar
                    model.VucutTahmini = await _aiService.GenerateContentAsync(prompt);
                }
            }
            catch (Exception ex)
            {
                model.HataMesaji = "Yapay zeka ?u an me?gul, lütfen tekrar deneyin: " + ex.Message;
            }

            return View(model);
        }
    }
}
