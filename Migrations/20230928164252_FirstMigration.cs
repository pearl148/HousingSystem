using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingSystem.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flat",
                columns: table => new
                {
                    FlatNo = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    SqFeetArea = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flat", x => x.FlatNo);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    OwnerId = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    OwnerFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerPhone = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    OwnerEmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerDateOfPurchase = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlatNo = table.Column<string>(type: "varchar(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.OwnerId);
                    table.ForeignKey(
                        name: "FK_Owner_Flat_FlatNo",
                        column: x => x.FlatNo,
                        principalTable: "Flat",
                        principalColumn: "FlatNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Occupant",
                columns: table => new
                {
                    OccupantId = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    OccupantIsOwned = table.Column<bool>(type: "bit", nullable: false),
                    OccupantFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccupantLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccupantPhone = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    OwnerEmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccupantStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OccupantEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlatNo = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    OwnerId = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupant", x => x.OccupantId);
                    table.ForeignKey(
                        name: "FK_Occupant_Flat_FlatNo",
                        column: x => x.FlatNo,
                        principalTable: "Flat",
                        principalColumn: "FlatNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Occupant_Owner_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owner",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    MaintenanceId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    MaintenanceMonthYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaintenanceAccountHead = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaintenanceAmount = table.Column<float>(type: "real", nullable: false),
                    MaintenanceRemark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlatNo = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    OccupantId = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.MaintenanceId);
                    table.ForeignKey(
                        name: "FK_Maintenance_Flat_FlatNo",
                        column: x => x.FlatNo,
                        principalTable: "Flat",
                        principalColumn: "FlatNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maintenance_Occupant_OccupantId",
                        column: x => x.OccupantId,
                        principalTable: "Occupant",
                        principalColumn: "OccupantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    PaymentAccountHead = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentReceiptId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccupantId = table.Column<string>(type: "varchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_Occupant_OccupantId",
                        column: x => x.OccupantId,
                        principalTable: "Occupant",
                        principalColumn: "OccupantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserActive = table.Column<bool>(type: "bit", nullable: false),
                    OccupantId = table.Column<string>(type: "varchar(5)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Occupant_OccupantId",
                        column: x => x.OccupantId,
                        principalTable: "Occupant",
                        principalColumn: "OccupantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_FlatNo",
                table: "Maintenance",
                column: "FlatNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_OccupantId",
                table: "Maintenance",
                column: "OccupantId");

            migrationBuilder.CreateIndex(
                name: "IX_Occupant_FlatNo",
                table: "Occupant",
                column: "FlatNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Occupant_OwnerId",
                table: "Occupant",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_FlatNo",
                table: "Owner",
                column: "FlatNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OccupantId",
                table: "Payment",
                column: "OccupantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_OccupantId",
                table: "User",
                column: "OccupantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Occupant");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "Flat");
        }
    }
}
