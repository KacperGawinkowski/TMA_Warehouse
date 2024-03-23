using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMA_Warehouse.Server.Migrations
{
    public partial class WarehouseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitsOfMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitsOfMeasurements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemGroupId = table.Column<int>(type: "int", nullable: false),
                    UnitOfMeasurementId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    PriceWithoutVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorageLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContantPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemGroups_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_UnitsOfMeasurements_UnitOfMeasurementId",
                        column: x => x.UnitOfMeasurementId,
                        principalTable: "UnitsOfMeasurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ItemGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Group_1" },
                    { 2, "Group_2" },
                    { 3, "Group_3" }
                });

            migrationBuilder.InsertData(
                table: "UnitsOfMeasurements",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Unit" },
                    { 2, "Kilograms" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "ContantPerson", "ItemGroupId", "Name", "PhotoURL", "PriceWithoutVAT", "Quantity", "Status", "StorageLocation", "UnitOfMeasurementId" },
                values: new object[,]
                {
                    { 1, "John Smith", 1, "Bricks", "brick_image.jpg", 1m, 1000.0, "In Stock", "Warehouse A, Shelf 1", 1 },
                    { 2, "John Smith", 1, "Cement bags", "cement_image.jpg", 25m, 500.0, "In Stock", "Warehouse A, Shelf 2", 2 },
                    { 3, "John Smith", 1, "Concrete blocks", "concrete_blocks_image.jpg", 5m, 800.0, "In Stock", "Warehouse A, Shelf 3", 1 },
                    { 4, "Michael Brown", 2, "Roofing materials", "roofing_materials_image.jpg", 15m, 200.0, "In Stock", "Warehouse B, Shelf 4", 1 },
                    { 5, null, 1, "Lumber", null, 2m, 600.0, "In Stock", null, 1 },
                    { 6, null, 2, "Nails", "nails_image.jpg", 0.15m, 5000.0, "In Stock", null, 1 },
                    { 7, "Michael Brown", 2, "Paint", "paint_image.jpg", 20m, 100.0, "In Stock", "Warehouse B, Shelf 2", 1 },
                    { 8, "Michael Brown", 2, "Drywall sheets", null, 17.5m, 300.0, "In Stock", "Warehouse B, Shelf 3", 1 },
                    { 9, "Olivia Anderson", 1, "Insulation", null, 35m, 400.0, "In Stock", "Warehouse A, Shelf 4", 1 },
                    { 10, null, 2, "Plumbing fixtures", "plumbing_fixtures_image.jpg", 150m, 50.0, "In Stock", "Warehouse B, Shelf 5", 1 },
                    { 11, "Olivia Anderson", 2, "Electrical wiring", "electrical_wiring_image.jpg", 7.49m, 2000.0, "In Stock", null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemGroupId",
                table: "Items",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UnitOfMeasurementId",
                table: "Items",
                column: "UnitOfMeasurementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ItemGroups");

            migrationBuilder.DropTable(
                name: "UnitsOfMeasurements");
        }
    }
}
