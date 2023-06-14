using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    public partial class addSectionNumtoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Section_Num",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Section_Num",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Section_Num",
                table: "Tables");

            migrationBuilder.AlterColumn<long>(
                name: "Section_Num",
                table: "Students",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
