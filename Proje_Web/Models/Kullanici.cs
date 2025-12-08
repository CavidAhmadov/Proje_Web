using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Proje_Web.Models
{
    public class Kullanici
    {
        [Display(Name ="Ogrenci ismi:")]
        [Required(ErrorMessage ="Bu alan zorunlu olmasi lazim ")]
        [MinLength(2)]
        public string Isim {  get; set; }

        [Display(Name = "Ogrenci parolasi")]
        [Required(ErrorMessage ="Sifre onemli")]
        public string parola {  get; set; }
    }
}
