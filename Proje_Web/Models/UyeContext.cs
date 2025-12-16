using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Proje_Web.Models
{
    public class UyeContext:DbContext
    {
        protected readonly IConfiguration Configuration;
        public UyeContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }
        public DbSet<Uye> Uye_Tablo { get; set; }
        public DbSet<Antrenor> Antrenor_Tablo { get; set; }
       
    }
}
