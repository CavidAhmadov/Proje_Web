using System.ComponentModel.DataAnnotations;

namespace Proje_Web.Models
{
    public class Antrenor
    {
        [Key]
        [Display(Name = "Antrenor Id")]
        public string Id { get; set; }

        [Display(Name = "Ismi")]
        [Required(ErrorMessage = "Isim alani zorunludur")]
        public string isim { get; set; }
        [Required(ErrorMessage ="Uzmanlik alani yazilmalidir")]
        [Display(Name = "Uzmanlik Alani")]
        public string Uzmanlik_alani {  get; set; }
        [Required(ErrorMessage = "hizmet turleri alani yazilmalidir")]
        [Display(Name = "Hizmet Turleri")]
        public string hizmet_turleri {  get; set; }
        [Required(ErrorMessage = "musaitlik saati alani yazilmalidir")]

        [Display(Name = "Musaitlik saatleri(örg:09:00-13:00)")]
        public string musaitlik_saatleri { get; set; }
    }
}
