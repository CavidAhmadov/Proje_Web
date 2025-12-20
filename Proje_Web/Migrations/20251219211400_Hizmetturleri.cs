using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje_Web.Migrations
{
    /// <inheritdoc />
    public partial class Hizmetturleri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hizmet_Turleri_Tablosu", // Veritabanındaki tablo adınız (DbContext'teki DbSet adınız neyse o olmalı)
                columns: table => new
                {
                    // Hizmet_turu string ve [Key] olduğu için Primary Key olacak
                    Hizmet_turu = table.Column<string>(type: "text", nullable: false),
                    Fiyat = table.Column<int>(type: "integer", nullable: false),
                    sure = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_Hizmet_Turleri_Tablosu", x => x.Hizmet_turu);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Migration geri alınırsa tabloyu siler
            migrationBuilder.DropTable(
                name: "Hizmet_Turleri_Tablosu");
        }
    }
}
