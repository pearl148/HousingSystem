using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingSystem.Migrations
{
    /// <inheritdoc />
    public partial class twentyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Noticenew",
                columns: table => new
                {
                    NoticeId = table.Column<string>(type: "varchar(12)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoticeTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoticePara = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noticenew", x => x.NoticeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Noticenew");
        }
    }
}
