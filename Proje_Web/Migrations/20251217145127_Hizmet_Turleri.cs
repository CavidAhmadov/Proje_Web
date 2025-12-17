using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje_Web.Migrations
{
    /// <inheritdoc />
    public partial class Hizmet_Turleri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hizmet_Turleri_Tablosu",
                columns: table => new
                {
                    Hizmet_turu = table.Column<string>(type: "text", nullable: false),
                    Fiyat = table.Column<decimal>(type: "numeric", nullable: false),
                    sure = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmet_Turleri_Tablosu", x => x.Hizmet_turu);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hizmet_Turleri_Tablosu");
        }
    }
}
