using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingSystem.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpenseMonthYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpenseAmount = table.Column<float>(type: "real", nullable: false),
                    ExpenseAccountHead = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenseRemark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenseProofData = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.ExpenseId);
                });

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
                name: "Expense");

           
        }
    }
}
