using Microsoft.EntityFrameworkCore.Migrations;

namespace TheDarkPortal.Data.Migrations
{
    public partial class AddUserWinsAndLossesProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMobe",
                table: "AspNetUsers",
                newName: "IsMob");

            migrationBuilder.AddColumn<int>(
                name: "ArenaLosses",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArenaWins",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArenaLosses",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ArenaWins",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IsMob",
                table: "AspNetUsers",
                newName: "IsMobe");
        }
    }
}
