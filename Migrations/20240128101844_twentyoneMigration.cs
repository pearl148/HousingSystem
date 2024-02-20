using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingSystem.Migrations
{
    /// <inheritdoc />
    public partial class twentyoneMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Complaint",
                columns: table => new
                {
                    ComplaintId = table.Column<string>(type: "varchar(12)", nullable: false),
                    ComplaintGiver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfComplaint = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ComplaintTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplaintCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplaintDetail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaint", x => x.ComplaintId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complaint");
        }
    }
}
