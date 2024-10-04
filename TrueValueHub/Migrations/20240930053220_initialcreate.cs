using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrueValueHub.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalPartNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeliverySiteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DrawingNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IncoTerms = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AnnualVolume = table.Column<int>(type: "int", nullable: false),
                    BomQty = table.Column<int>(type: "int", nullable: false),
                    DeliveryFrequency = table.Column<int>(type: "int", nullable: false),
                    LotSize = table.Column<int>(type: "int", nullable: false),
                    ManufacturingCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PackagingType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductLifeRemaining = table.Column<int>(type: "int", nullable: false),
                    PaymentTerms = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LifetimeQuantityRemaining = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartId);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturings",
                columns: table => new
                {
                    ManufacturingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubProcessType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MachineDetails = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MachineDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MCAutomation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MachineEfficiency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Manufacturings");

            migrationBuilder.DropTable(
                name: "Parts");
        }
    }
}
