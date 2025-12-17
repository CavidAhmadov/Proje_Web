using Microsoft.AspNetCore.Mvc;
using Proje_Web.Models;

namespace Proje_Web.Controllers
{
    public class HizmetController : Controller
    {
        private readonly UyeContext _context;

        public HizmetController(UyeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var list = _context.Hizmet_Turleri_Tablosu.ToList();
            return View(list);
        }
        
    }
}
