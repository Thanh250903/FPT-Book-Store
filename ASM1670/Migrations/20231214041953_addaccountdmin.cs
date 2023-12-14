using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM1670.Migrations
{
    /// <inheritdoc />
    public partial class addaccountdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5f5b0096-0a86-4c3e-ad5f-946c0674d73d", "763648d4-4f8f-41ef-ab0d-e235d400836a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5f5b0096-0a86-4c3e-ad5f-946c0674d73d", "763648d4-4f8f-41ef-ab0d-e235d400836a" });
        }
    }
}
