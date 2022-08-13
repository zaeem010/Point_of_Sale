using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.Migrations
{
    public partial class tbltest3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TranscationDetails_Testmaster_Invid",
                table: "TranscationDetails");

            migrationBuilder.DropTable(
                name: "Testdetail");

            migrationBuilder.DropTable(
                name: "Testmaster");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Testmaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testmaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testdetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestmasterId = table.Column<long>(type: "bigint", nullable: true),
                    loas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testdetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Testdetail_Testmaster_TestmasterId",
                        column: x => x.TestmasterId,
                        principalTable: "Testmaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Testdetail_TestmasterId",
                table: "Testdetail",
                column: "TestmasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_TranscationDetails_Testmaster_Invid",
                table: "TranscationDetails",
                column: "Invid",
                principalTable: "Testmaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
