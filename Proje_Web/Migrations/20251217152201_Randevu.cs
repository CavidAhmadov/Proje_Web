using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Proje_Web.Migrations
{
    /// <inheritdoc />
    public partial class Randevu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Randevu_tablosu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RandevuTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HizmetTuru = table.Column<string>(type: "text", nullable: false),
                    HizmetAy = table.Column<string>(type: "text", nullable: false),
                    HizmetUcret = table.Column<string>(type: "text", nullable: false),
                    Egitmen = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevu_tablosu", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevu_tablosu");
        }
    }
}
