using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegoSoftWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class cusdefaultaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_cus_default_address_id",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_cus_default_address_id",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "cus_default_address_id",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "cus_default_address_id",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_cus_default_address_id",
                table: "Customers",
                column: "cus_default_address_id",
                unique: true,
                filter: "[cus_default_address_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_cus_default_address_id",
                table: "Customers",
                column: "cus_default_address_id",
                principalTable: "Addresses",
                principalColumn: "add_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
