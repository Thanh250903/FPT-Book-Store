using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM1670.Migrations
{
    /// <inheritdoc />
    public partial class abcd3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedByUserEmail",
                table: "Cart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedTime",
                table: "Cart",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedByUserEmail",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "AddedTime",
                table: "Cart");
        }
    }
}
