using Microsoft.EntityFrameworkCore.Migrations;

namespace Artizanalii.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Denumire",
                table: "Produs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Denumire",
                table: "Produs");
        }
    }
}
