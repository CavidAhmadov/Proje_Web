using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje_Web.Migrations
{
    /// <inheritdoc />
    public partial class antrenor_degisiklik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "isim",
                table: "Antrenor_Tablo",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isim",
                table: "Antrenor_Tablo");
        }
    }
}
