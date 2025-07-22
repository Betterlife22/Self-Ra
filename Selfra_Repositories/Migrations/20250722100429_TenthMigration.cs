using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selfra_Repositories.Migrations
{
    /// <inheritdoc />
    public partial class TenthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PackageId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PackageId",
                table: "Courses",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Packages_PackageId",
                table: "Courses",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Packages_PackageId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_PackageId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Courses");
        }
    }
}
