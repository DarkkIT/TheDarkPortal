using Microsoft.EntityFrameworkCore.Migrations;

namespace TheDarkPortal.Data.Migrations
{
    public partial class AddBattleCardPropertyToCardTierOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBattleSetCard",
                table: "CardsLevelOne",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBattleSetCard",
                table: "CardsLevelOne");
        }
    }
}
