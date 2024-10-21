using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegoSoftWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class newRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suppliers_sup_default_address_id",
                table: "Suppliers");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_sup_default_address_id",
                table: "Suppliers",
                column: "sup_default_address_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suppliers_sup_default_address_id",
                table: "Suppliers");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_sup_default_address_id",
                table: "Suppliers",
                column: "sup_default_address_id",
                unique: true);
        }
    }
}
