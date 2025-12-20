using System.ComponentModel.DataAnnotations;

namespace Proje_Web.Models
{
    public class HizmetTurleri
    {
        [Key]
        [Display(Name ="Hizmet Turu")]
        public string Hizmet_turu { get; set; }
        [Required(ErrorMessage = "Fiyat alanı zorunludur.")]
        [Display(Name = "Fiyatlar")]
        public int Fiyat { get; set; }
        [Required]
        [Display(Name = "Sure")]
        public int sure { get; set; }
    }
}
