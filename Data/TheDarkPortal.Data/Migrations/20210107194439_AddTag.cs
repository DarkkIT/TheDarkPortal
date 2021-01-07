using Microsoft.EntityFrameworkCore.Migrations;

namespace TheDarkPortal.Data.Migrations
{
    public partial class AddTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqueTag",
                table: "TempBattleCards",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueTag",
                table: "TempBattleCards");
        }
    }
}
