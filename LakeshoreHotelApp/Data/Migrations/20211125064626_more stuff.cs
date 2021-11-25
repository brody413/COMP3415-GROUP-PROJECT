using Microsoft.EntityFrameworkCore.Migrations;

namespace LakeshoreHotelApp.Data.Migrations
{
    public partial class morestuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rooms_customers_CustomerId",
                table: "rooms");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "rooms",
                newName: "customerID");

            migrationBuilder.RenameIndex(
                name: "IX_rooms_CustomerId",
                table: "rooms",
                newName: "IX_rooms_customerID");

            migrationBuilder.AddForeignKey(
                name: "FK_rooms_customers_customerID",
                table: "rooms",
                column: "customerID",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rooms_customers_customerID",
                table: "rooms");

            migrationBuilder.RenameColumn(
                name: "customerID",
                table: "rooms",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_rooms_customerID",
                table: "rooms",
                newName: "IX_rooms_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_rooms_customers_CustomerId",
                table: "rooms",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
