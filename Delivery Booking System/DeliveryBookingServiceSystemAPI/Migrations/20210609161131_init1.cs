using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryBookingServiceSystemAPI.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "City",
                value: "CHENNAI");

            migrationBuilder.UpdateData(
                table: "Executives",
                keyColumn: "ExecutiveId",
                keyValue: 100,
                column: "City",
                value: "HOSUR");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "City",
                value: "Chennai");

            migrationBuilder.UpdateData(
                table: "Executives",
                keyColumn: "ExecutiveId",
                keyValue: 100,
                column: "City",
                value: "Hosur");
        }
    }
}
