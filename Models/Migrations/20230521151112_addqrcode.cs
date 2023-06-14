using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    public partial class addqrcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Qrs_Code",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Qr_Duration = table.Column<int>(type: "int", nullable: false),
                    Qr_Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    LectureId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qrs_Code", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qrs_Code_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Qrs_Code_LectureId",
                table: "Qrs_Code",
                column: "LectureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Qrs_Code");
        }
    }
}
