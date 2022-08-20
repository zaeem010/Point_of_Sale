using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "PosPurchaseMaster");

            migrationBuilder.AddColumn<long>(
                name: "PosPurchaseMasterId",
                table: "Stock",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosPurchaseMasterId",
                table: "Stock");

            migrationBuilder.AddColumn<double>(
                name: "SubTotal",
                table: "PosPurchaseMaster",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
