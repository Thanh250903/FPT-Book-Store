using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM1670.Migrations
{
    /// <inheritdoc />
    public partial class admin1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "24262f9a-60b1-4824-9148-2bcbb82bc35e", 0, "835b1159-e588-47b9-9f8f-94cd42c6cdb1", "admin@admin.com", false, false, null, null, null, "admin@123", null, false, "01ed1e5c-6053-4307-a2f3-e4d78b74c210", false, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "24262f9a-60b1-4824-9148-2bcbb82bc35e");
        }
    }
}
