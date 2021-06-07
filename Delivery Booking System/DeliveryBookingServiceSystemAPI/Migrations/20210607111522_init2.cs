using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryBookingServiceSystemAPI.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExecutiveEmail",
                table: "Executives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Executives",
                keyColumn: "ExecutiveId",
                keyValue: 100,
                column: "ExecutiveEmail",
                value: "admin@gmail.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecutiveEmail",
                table: "Executives");
        }
    }
}
