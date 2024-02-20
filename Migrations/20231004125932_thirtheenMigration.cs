using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingSystem.Migrations
{
    /// <inheritdoc />
    public partial class thirtheenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PaymentAmount",
                table: "Payment",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "Payment");
        }
    }
}
