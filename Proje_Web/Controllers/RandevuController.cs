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
            var list = _context.Antrenor_Tablo.ToList();
            ViewBag.EgitmenList = new SelectList(list, "Id","isim");

           
            return View();
        }
        [HttpGet]
        public IActionResult GetHizmetlerByEgitmen(string egitmenId)
        {
            // Bu eğitmenin verdiği hizmetleri veritabanından buluyoruz
            var hizmetler = _context.Randevu_tablosu
                                    .Where(h => h.Egitmen == egitmenId)
                                    .Select(h => new { id = h.Id, ad = h.HizmetTuru })
                                    .ToList();

            return Json(hizmetler); // Listeyi JSON formatında gönder
        }
    }
}
