using Microsoft.EntityFrameworkCore.Migrations;

namespace TheDarkPortal.Data.Migrations
{
    public partial class UpdateTempCardstre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OnTurne",
                table: "TempBattleCards",
                newName: "IsSelected");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSelected",
                table: "TempBattleCards",
                newName: "OnTurne");
        }
    }
}
