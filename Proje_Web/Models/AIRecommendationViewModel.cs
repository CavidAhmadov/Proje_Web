using Microsoft.AspNetCore.Http;

namespace Proje_Web.Models
{
    public class AIRecommendationViewModel
    {
        // Giriş Alanları
        public string? VucutTipi { get; set; }
        public int BoyCm { get; set; }
        public int KiloKg { get; set; }
        public IFormFile? ResimDosyasi { get; set; }

        // Yanıt Alanları
        public string? EgzersizOnerisi { get; set; }
        public string? DiyetPlani { get; set; }
        public string? VucutTahmini { get; set; }
        public string? HataMesaji { get; set; }
    }
}