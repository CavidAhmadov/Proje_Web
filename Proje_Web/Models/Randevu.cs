using Proje_Web.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_Web.Models
{
    public class Randevu
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Randevu tarihi seçilmelidir.")]
        [Display(Name = "Randevu Tarihi")]
        public DateTime RandevuTarihi { get; set; }

        // --- İlişkiler ---

        [Display(Name = "Hizmet Türü")]
        [Required(ErrorMessage = "Hizmet Turu zorunludur")]
        public string HizmetTuru { get; set; }

        [Display(Name = "Hizmet Suresi")]
        public string HizmetAy {  get; set; }

        [Display(Name = "Hizmet ucreti")]
        public string HizmetUcret { get; set; }

        [Required(ErrorMessage ="Egitmen girilmelidir")]
        [Display(Name = "Eğitmen")]
        public string Egitmen { get; set; }

    }
}
