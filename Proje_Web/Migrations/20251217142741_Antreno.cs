using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje_Web.Migrations
{
    /// <inheritdoc />
    public partial class Antreno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Antrenor_Tablo",
               columns: table => new
               {
                   Id = table.Column<string>(type: "text", nullable: false),
                   Uzmanlik_alani = table.Column<string>(type: "text", nullable: false),
                   hizmet_turleri = table.Column<string>(type: "text", nullable: false),
                   musaitlik_saatleri = table.Column<string>(type: "text", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Antrenor_Tablo", x => x.Id);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Antrenor_Tablo");
        }
    }
}
