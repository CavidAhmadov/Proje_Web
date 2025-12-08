using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Proje_Web.Models
{
    public class UyeContext:DbContext
    {
        public DbSet<Uye> Uye_Tablo { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Register;Trusted_Connection=True;");
        }
       
    }
}
