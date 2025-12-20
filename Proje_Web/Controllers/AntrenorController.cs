using Microsoft.AspNetCore.Mvc;
using Proje_Web.Models;
using System;

namespace Proje_Web.Controllers
{
    public class AntrenorController : Controller
    {
        private readonly UyeContext _context;

        public AntrenorController(UyeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            var liste = _context.Antrenor_Tablo.ToList();
            return View(liste);
            
        }

        [HttpGet]
        public IActionResult GetAntrenorlerApi()
        {
            // Veritabanındaki Antrenor_Tablo verilerini JSON olarak döndürür
            var liste = _context.Antrenor_Tablo.ToList();
            return Json(liste); // REST API çıktısı burasıdır
        }
    }
}
