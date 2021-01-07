using Microsoft.EntityFrameworkCore.Migrations;

namespace TheDarkPortal.Data.Migrations
{
    public partial class UpdateTempCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HaveTurne",
                table: "TempBattleCards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDestroed",
                table: "TempBattleCards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnTurne",
                table: "TempBattleCards",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaveTurne",
                table: "TempBattleCards");

            migrationBuilder.DropColumn(
                name: "IsDestroed",
                table: "TempBattleCards");

            migrationBuilder.DropColumn(
                name: "OnTurne",
                table: "TempBattleCards");
        }
    }
}
