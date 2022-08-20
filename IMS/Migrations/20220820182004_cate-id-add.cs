using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class cateidadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "PosPurchaseDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PosPurchaseDetail");
        }
    }
}
