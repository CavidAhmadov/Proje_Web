using System.ComponentModel.DataAnnotations;

namespace Proje_Web.Models
{
    public class HizmetTurleri
    {
        [Key]

        [Display(Name ="Hizmet Turu")]
        public string Hizmet_turu { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        [Display(Name ="Fiyatlar")]
        public decimal Fiyat { get; set; }

        [Required]
        [Display(Name = "Sure")]
        public string sure { get; set; }
    }
}
