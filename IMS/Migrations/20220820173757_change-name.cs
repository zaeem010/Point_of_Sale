using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class changename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PosPurchaseMaster_ThirdLevel_ThirdlevelId",
                table: "PosPurchaseMaster");

            migrationBuilder.RenameColumn(
                name: "ThirdlevelId",
                table: "PosPurchaseMaster",
                newName: "ThirdLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_PosPurchaseMaster_ThirdlevelId",
                table: "PosPurchaseMaster",
                newName: "IX_PosPurchaseMaster_ThirdLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PosPurchaseMaster_ThirdLevel_ThirdLevelId",
                table: "PosPurchaseMaster",
                column: "ThirdLevelId",
                principalTable: "ThirdLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PosPurchaseMaster_ThirdLevel_ThirdLevelId",
                table: "PosPurchaseMaster");

            migrationBuilder.RenameColumn(
                name: "ThirdLevelId",
                table: "PosPurchaseMaster",
                newName: "ThirdlevelId");

            migrationBuilder.RenameIndex(
                name: "IX_PosPurchaseMaster_ThirdLevelId",
                table: "PosPurchaseMaster",
                newName: "IX_PosPurchaseMaster_ThirdlevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PosPurchaseMaster_ThirdLevel_ThirdlevelId",
                table: "PosPurchaseMaster",
                column: "ThirdlevelId",
                principalTable: "ThirdLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
