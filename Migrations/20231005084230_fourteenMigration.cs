using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingSystem.Migrations
{
    /// <inheritdoc />
    public partial class fourteenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentMaintenanceAssociations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    MaintenanceId = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMaintenanceAssociations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentMaintenanceAssociations_Maintenance_MaintenanceId",
                        column: x => x.MaintenanceId,
                        principalTable: "Maintenance",
                        principalColumn: "MaintenanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentMaintenanceAssociations_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMaintenanceAssociations_MaintenanceId",
                table: "PaymentMaintenanceAssociations",
                column: "MaintenanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMaintenanceAssociations_PaymentId",
                table: "PaymentMaintenanceAssociations",
                column: "PaymentId",
                unique: true);

                migrationBuilder.Sql(@"
        INSERT INTO PaymentMaintenanceAssociations (PaymentId, MaintenanceId)
        SELECT p.PaymentId, m.MaintenanceId
        FROM Payment p
        INNER JOIN Maintenance m ON p.PaymentId = m.MaintenanceId
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentMaintenanceAssociations");
        }
    }
}
