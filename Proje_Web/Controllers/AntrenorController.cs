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
    }
}
