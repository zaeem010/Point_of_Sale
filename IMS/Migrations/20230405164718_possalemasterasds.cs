using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class possalemasterasds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PosSaleMaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    InvDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThirdLevelId = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(455)", maxLength: 455, nullable: true),
                    NetTotal = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    GrandTotal = table.Column<double>(type: "float", nullable: false),
                    ReceivedTotal = table.Column<double>(type: "float", nullable: false),
                    ReturnPrice = table.Column<double>(type: "float", nullable: false),
                    VType = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosSaleMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosSaleMaster_ThirdLevel_ThirdLevelId",
                        column: x => x.ThirdLevelId,
                        principalTable: "ThirdLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PosSaleDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosSaleMasterId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    Qty = table.Column<double>(type: "float", nullable: false),
                    SalePrice = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosSaleDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosSaleDetail_PosSaleMaster_PosSaleMasterId",
                        column: x => x.PosSaleMasterId,
                        principalTable: "PosSaleMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PosSaleDetail_PosSaleMasterId",
                table: "PosSaleDetail",
                column: "PosSaleMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PosSaleMaster_ThirdLevelId",
                table: "PosSaleMaster",
                column: "ThirdLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PosSaleDetail");

            migrationBuilder.DropTable(
                name: "PosSaleMaster");
        }
    }
}
