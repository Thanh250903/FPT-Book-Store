using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM1670.Migrations
{
    /// <inheritdoc />
    public partial class pk1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "DisplayOrder", "Name" },
                values: new object[] { 4, "So difficult", 4, "Science" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
