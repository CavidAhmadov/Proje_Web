using Microsoft.AspNetCore.Mvc;

namespace Proje_Web.Controllers
{
    public class AntrenorController : Controller
    {
        public IActionResult Index()
        {
        
            return View(); // Listeyi View'a gönde
            
        }
    }
}
