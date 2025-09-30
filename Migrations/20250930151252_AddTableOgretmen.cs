using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efcoreApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTableOgretmen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OgretmenId",
                table: "Kurslar",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ogretmenler",
                columns: table => new
                {
                    OgretmenId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: true),
                    Soyad = table.Column<string>(type: "TEXT", nullable: true),
                    Eposta = table.Column<string>(type: "TEXT", nullable: true),
                    Telefon = table.Column<string>(type: "TEXT", nullable: true),
                    BaslamaTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogretmenler", x => x.OgretmenId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kurslar_OgretmenId",
                table: "Kurslar",
                column: "OgretmenId");

            migrationBuilder.CreateIndex(
                name: "IX_KursKayitlar_KursId",
                table: "KursKayitlar",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_KursKayitlar_OgrenciId",
                table: "KursKayitlar",
                column: "OgrenciId");

            migrationBuilder.AddForeignKey(
                name: "FK_KursKayitlar_Kurslar_KursId",
                table: "KursKayitlar",
                column: "KursId",
                principalTable: "Kurslar",
                principalColumn: "KursId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KursKayitlar_Ogrenciler_OgrenciId",
                table: "KursKayitlar",
                column: "OgrenciId",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kurslar_Ogretmenler_OgretmenId",
                table: "Kurslar",
                column: "OgretmenId",
                principalTable: "Ogretmenler",
                principalColumn: "OgretmenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KursKayitlar_Kurslar_KursId",
                table: "KursKayitlar");

            migrationBuilder.DropForeignKey(
                name: "FK_KursKayitlar_Ogrenciler_OgrenciId",
                table: "KursKayitlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Kurslar_Ogretmenler_OgretmenId",
                table: "Kurslar");

            migrationBuilder.DropTable(
                name: "Ogretmenler");

            migrationBuilder.DropIndex(
                name: "IX_Kurslar_OgretmenId",
                table: "Kurslar");

            migrationBuilder.DropIndex(
                name: "IX_KursKayitlar_KursId",
                table: "KursKayitlar");

            migrationBuilder.DropIndex(
                name: "IX_KursKayitlar_OgrenciId",
                table: "KursKayitlar");

            migrationBuilder.DropColumn(
                name: "OgretmenId",
                table: "Kurslar");
        }
    }
}
