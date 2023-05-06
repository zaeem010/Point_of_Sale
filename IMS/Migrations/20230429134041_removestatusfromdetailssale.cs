using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class removestatusfromdetailssale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PosSaleDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PosSaleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
