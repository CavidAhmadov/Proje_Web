using Microsoft.AspNetCore.Mvc;

namespace Proje_Web.Controllers
{
    public class AntrenorController : Controller
    {
        public IActionResult Index()
        {
            var antrenorler = new List<Proje_Web.Models.Antrenor>
        {
            new Proje_Web.Models.Antrenor { Id = 1, AdSoyad = "Ahmet Yılmaz", UzmanlikAlanlari = "Kas Geliştirme, Güç Antrenmanı"},
            new Proje_Web.Models.Antrenor { Id = 2, AdSoyad = "Elif Kaya", UzmanlikAlanlari = "Yoga, Pilates, Esneklik" },
            new Proje_Web.Models.Antrenor { Id = 3, AdSoyad = "Caner Özdemir", UzmanlikAlanlari = "Kilo Verme, Fonksiyonel Antrenman"}
        };

            return View(antrenorler); // Listeyi View'a gönder
            
        }
    }
}
