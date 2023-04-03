using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class tblstockupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosPurchaseMasterId",
                table: "Stock",
                newName: "PosMasterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosMasterId",
                table: "Stock",
                newName: "PosPurchaseMasterId");
        }
    }
}
