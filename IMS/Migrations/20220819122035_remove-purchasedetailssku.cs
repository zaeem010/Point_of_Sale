using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class removepurchasedetailssku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sku",
                table: "PosPurchaseDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "PosPurchaseDetail",
                type: "nvarchar(455)",
                maxLength: 455,
                nullable: true);
        }
    }
}
