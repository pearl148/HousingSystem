using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeventhMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occupant_Flat_FlatNo",
                table: "Occupant");

            migrationBuilder.DropForeignKey(
                name: "FK_Occupant_Owner_OwnerId",
                table: "Occupant");

            migrationBuilder.AddForeignKey(
                name: "FK_Occupant_Flat_FlatNo",
                table: "Occupant",
                column: "FlatNo",
                principalTable: "Flat",
                principalColumn: "FlatNo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Occupant_Owner_OwnerId",
                table: "Occupant",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occupant_Flat_FlatNo",
                table: "Occupant");

            migrationBuilder.DropForeignKey(
                name: "FK_Occupant_Owner_OwnerId",
                table: "Occupant");

            migrationBuilder.AddForeignKey(
                name: "FK_Occupant_Flat_FlatNo",
                table: "Occupant",
                column: "FlatNo",
                principalTable: "Flat",
                principalColumn: "FlatNo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Occupant_Owner_OwnerId",
                table: "Occupant",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
