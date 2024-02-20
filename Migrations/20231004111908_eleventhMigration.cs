using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingSystem.Migrations
{
    /// <inheritdoc />
    public partial class eleventhMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payment_OccupantId",
                table: "Payment");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OccupantId",
                table: "Payment",
                column: "OccupantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payment_OccupantId",
                table: "Payment");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OccupantId",
                table: "Payment",
                column: "OccupantId",
                unique: true);
        }
    }
}
