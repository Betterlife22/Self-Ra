using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selfra_Repositories.Migrations
{
    /// <inheritdoc />
    public partial class SeventhMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PackageId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PackageId",
                table: "Transactions",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Packages_PackageId",
                table: "Transactions",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Packages_PackageId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PackageId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Transactions");
        }
    }
}
