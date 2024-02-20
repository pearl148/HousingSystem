using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingSystem.Migrations
{
    /// <inheritdoc />
    public partial class eigthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Maintenance_FlatNo",
                table: "Maintenance");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_FlatNo",
                table: "Maintenance",
                column: "FlatNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Maintenance_FlatNo",
                table: "Maintenance");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_FlatNo",
                table: "Maintenance",
                column: "FlatNo",
                unique: true);
        }
    }
}
