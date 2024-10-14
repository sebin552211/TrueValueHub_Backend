using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrueValueHub.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProjectDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProjectCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalPartNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SupplierName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeliverySiteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DrawingNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IncoTerms = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AnnualVolume = table.Column<int>(type: "int", nullable: false),
                    BomQty = table.Column<int>(type: "int", nullable: false),
                    DeliveryFrequency = table.Column<int>(type: "int", nullable: false),
                    LotSize = table.Column<int>(type: "int", nullable: false),
                    ManufacturingCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PackagingType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductLifeRemaining = table.Column<int>(type: "int", nullable: false),
                    PaymentTerms = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LifetimeQuantityRemaining = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_Parts_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturings",
                columns: table => new
                {
                    ManufacturingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubProcessType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MachineDetails = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MachineDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MCAutomation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MachineEfficiency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ToolingCost = table.Column<int>(type: "int", nullable: false),
                    LoadingTime = table.Column<int>(type: "int", nullable: false),
                    PartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturings", x => x.ManufacturingId);
                    table.ForeignKey(
                        name: "FK_Manufacturings_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturings_PartId",
                table: "Manufacturings",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ProjectId",
                table: "Parts",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Manufacturings");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
