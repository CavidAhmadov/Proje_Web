using System.Text;
using System.Text.Json;

namespace Proje_Web.Services
{
    public class GeminiAIService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public GeminiAIService(IConfiguration configuration)
        {
            _apiKey = configuration["GeminiAI:ApiKey"];
            _httpClient = new HttpClient();
        }

        public async Task<string> GenerateContentAsync(string prompt, Stream? imageStream = null, string? mimeType = null)
        {

            try
            {
                var parts = new List<object> { new { text = prompt } };

                if (imageStream != null)
                {
                    using var ms = new MemoryStream();
                    await imageStream.CopyToAsync(ms);
                    parts.Add(new { inline_data = new { mime_type = mimeType, data = Convert.ToBase64String(ms.ToArray()) } });
                }

                var body = new { contents = new[] { new { parts = parts.ToArray() } } };
                //var url = $"https://generativelanguage.googleapis.com/v1/models/gemini-1.5-flash:generateContent?key={_apiKey}";
                // "v1" yazan yeri "v1beta" yapıyoruz
                var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";
                // URL'yi tam olarak listede gördüğün isimle (gemini-2.0-flash-exp) güncelle:
                // var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash-exp:generateContent?key={_apiKey}";
                var response = await _httpClient.PostAsync(url, new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
                var resContent = await response.Content.ReadAsStringAsync();

                // JSON'ı parse etmeden önce gelen ham veriyi kontrol et
                using var doc = JsonDocument.Parse(resContent);

                // HATA KONTROLÜ: Eğer API bir hata döndürdüyse 'candidates' anahtarı bulunmaz.
                if (doc.RootElement.TryGetProperty("candidates", out var candidates))
                {
                    return candidates[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString() ?? "Metin bulunamadı.";
                }
                else if (doc.RootElement.TryGetProperty("error", out var error))
                {
                    // API'den gelen gerçek hatayı burada yakala
                    return "Gemini API Hatası: " + error.GetProperty("message").GetString();
                }

                return "Beklenmedik bir yanıt formatı alındı.";
            }
            catch (Exception ex)
            {
                return "Servis Hatası: " + ex.Message;
            }
        }
        // Mevcut metodunun altına yapıştır
        
    }
}