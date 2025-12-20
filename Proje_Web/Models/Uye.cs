using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Proje_Web.Models
{
    public class Uye
    {
        [MinLength(3,ErrorMessage ="Minimum 3 harf lazim")]
        [Required(ErrorMessage ="isim kismi gerekli")]
        [StringLength(30)]
        [EmailAddress(ErrorMessage ="mail icin @ gerekli ")]
        [Key]
        public string ad { get; set; }

        [MinLength(3, ErrorMessage = "Minimum 3 harf lazim")]
        [Required(ErrorMessage = "Parola kismi gerekli")]
        [StringLength(10)]
        public string parola { get; set; }
    }
}
