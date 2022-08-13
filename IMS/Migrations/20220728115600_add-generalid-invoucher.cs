using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class addgeneralidinvoucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GeneralId",
                table: "VoucherMaster",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneralId",
                table: "VoucherMaster");
        }
    }
}
