using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class addheadidintransactiontable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeadId",
                table: "TranscationDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadId",
                table: "TranscationDetails");
        }
    }
}
