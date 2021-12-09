using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LakeshoreHotelApp.Data.Migrations
{
    public partial class AddedrestaurantReservationmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestaurantReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfGuests = table.Column<int>(nullable: false),
                    CustomerId = table.Column<string>(nullable: false),
                    ReservationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantReservations_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantReservations_CustomerId",
                table: "RestaurantReservations",
                column: "CustomerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantReservations");
        }
    }
}
