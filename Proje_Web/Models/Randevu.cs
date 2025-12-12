using Proje_Web.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Proje_Web.Models
{
    public class Randevu
    {
        public DateTime SecilenSaat { get; set; }

        public int SecilenHizmetId { get; set; }
        public int SecilenAntrenorId { get; set; }
        
        

    }
}
