using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryBookingServiceSystemAPI.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PinCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "Address", "AdminEmail", "AdminName", "Age", "City", "Password", "Phone", "PinCode" },
                values: new object[] { 1, "No.61, Anna Nagar", "admin@gmail.com", "Admin", 32, "Chennai", "Admin", "9443354155", "600006" });

            migrationBuilder.UpdateData(
                table: "Executives",
                keyColumn: "ExecutiveId",
                keyValue: 100,
                columns: new[] { "Address", "Age", "City", "ExecutiveEmail", "Password", "Phone", "PinCode" },
                values: new object[] { "No.81, Annai Nagar", 22, "Hosur", "arun@gmail.com", "1234", "9943354155", "601206" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.UpdateData(
                table: "Executives",
                keyColumn: "ExecutiveId",
                keyValue: 100,
                columns: new[] { "Address", "Age", "City", "ExecutiveEmail", "Password", "Phone", "PinCode" },
                values: new object[] { "No.61, Anna Nagar", 32, "Chennai", "admin@gmail.com", "Admin", "9443354155", "600006" });
        }
    }
}
