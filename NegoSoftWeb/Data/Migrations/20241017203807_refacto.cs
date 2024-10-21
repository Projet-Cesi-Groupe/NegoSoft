using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegoSoftWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class refacto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlcoholProducts");

            migrationBuilder.AddColumn<float>(
                name: "pro_alcohol_volume",
                table: "Products",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "pro_is_active",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "pro_year",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pro_alcohol_volume",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "pro_is_active",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "pro_year",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "AlcoholProducts",
                columns: table => new
                {
                    ap_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pro_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ap_alcohol_volume = table.Column<float>(type: "real", nullable: false),
                    ap_year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlcoholProducts", x => x.ap_id);
                    table.ForeignKey(
                        name: "FK_AlcoholProducts_Products_pro_type_id",
                        column: x => x.pro_type_id,
                        principalTable: "Products",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlcoholProducts_pro_type_id",
                table: "AlcoholProducts",
                column: "pro_type_id",
                unique: true);
        }
    }
}
