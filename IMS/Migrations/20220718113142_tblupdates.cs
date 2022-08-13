using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class tblupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TranscationDetails_PurchaseMaster_Invid",
                table: "TranscationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherMaster_TranscationDetails_Invid",
                table: "VoucherMaster");

            migrationBuilder.DropIndex(
                name: "IX_VoucherMaster_Invid",
                table: "VoucherMaster");

            migrationBuilder.DropIndex(
                name: "IX_TranscationDetails_Invid",
                table: "TranscationDetails");

            migrationBuilder.DropColumn(
                name: "Invid",
                table: "VoucherMaster");

            

            migrationBuilder.AlterColumn<long>(
                name: "Invid",
                table: "TranscationDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<long>(
                name: "Invid",
                table: "VoucherMaster",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Invid",
                table: "TranscationDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

          

            migrationBuilder.CreateIndex(
                name: "IX_VoucherMaster_Invid",
                table: "VoucherMaster",
                column: "Invid");

            migrationBuilder.CreateIndex(
                name: "IX_TranscationDetails_Invid",
                table: "TranscationDetails",
                column: "Invid");

            migrationBuilder.AddForeignKey(
                name: "FK_TranscationDetails_PurchaseMaster_Invid",
                table: "TranscationDetails",
                column: "Invid",
                principalTable: "PurchaseMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherMaster_TranscationDetails_Invid",
                table: "VoucherMaster",
                column: "Invid",
                principalTable: "TranscationDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
