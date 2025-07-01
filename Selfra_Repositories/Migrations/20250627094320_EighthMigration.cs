using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selfra_Repositories.Migrations
{
    /// <inheritdoc />
    public partial class EighthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPackages_AspNetUsers_UserId",
                table: "UserPackages");

            migrationBuilder.DropIndex(
                name: "IX_UserPackages_UserId",
                table: "UserPackages");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserPackages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserPackages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPackages_UserId",
                table: "UserPackages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPackages_AspNetUsers_UserId",
                table: "UserPackages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
