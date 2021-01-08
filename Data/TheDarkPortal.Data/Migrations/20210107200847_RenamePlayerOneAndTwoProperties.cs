using Microsoft.EntityFrameworkCore.Migrations;

namespace TheDarkPortal.Data.Migrations
{
    public partial class RenamePlayerOneAndTwoProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleRooms_AspNetUsers_PlayerOneId",
                table: "BattleRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleRooms_AspNetUsers_PlayerTwoId",
                table: "BattleRooms");

            migrationBuilder.DropColumn(
                name: "HaveTakenTurn",
                table: "CardsLevelOne");

            migrationBuilder.DropColumn(
                name: "IsAttacker",
                table: "CardsLevelOne");

            migrationBuilder.DropColumn(
                name: "IsDestroyed",
                table: "CardsLevelOne");

            migrationBuilder.RenameColumn(
                name: "PlayerTwoId",
                table: "BattleRooms",
                newName: "DefenderId");

            migrationBuilder.RenameColumn(
                name: "PlayerOneId",
                table: "BattleRooms",
                newName: "AttackerId");

            migrationBuilder.RenameColumn(
                name: "IsFirstPlayerTurn",
                table: "BattleRooms",
                newName: "IsAttackerTurn");

            migrationBuilder.RenameIndex(
                name: "IX_BattleRooms_PlayerTwoId",
                table: "BattleRooms",
                newName: "IX_BattleRooms_DefenderId");

            migrationBuilder.RenameIndex(
                name: "IX_BattleRooms_PlayerOneId",
                table: "BattleRooms",
                newName: "IX_BattleRooms_AttackerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleRooms_AspNetUsers_AttackerId",
                table: "BattleRooms",
                column: "AttackerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleRooms_AspNetUsers_DefenderId",
                table: "BattleRooms",
                column: "DefenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleRooms_AspNetUsers_AttackerId",
                table: "BattleRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleRooms_AspNetUsers_DefenderId",
                table: "BattleRooms");

            migrationBuilder.RenameColumn(
                name: "IsAttackerTurn",
                table: "BattleRooms",
                newName: "IsFirstPlayerTurn");

            migrationBuilder.RenameColumn(
                name: "DefenderId",
                table: "BattleRooms",
                newName: "PlayerTwoId");

            migrationBuilder.RenameColumn(
                name: "AttackerId",
                table: "BattleRooms",
                newName: "PlayerOneId");

            migrationBuilder.RenameIndex(
                name: "IX_BattleRooms_DefenderId",
                table: "BattleRooms",
                newName: "IX_BattleRooms_PlayerTwoId");

            migrationBuilder.RenameIndex(
                name: "IX_BattleRooms_AttackerId",
                table: "BattleRooms",
                newName: "IX_BattleRooms_PlayerOneId");

            migrationBuilder.AddColumn<bool>(
                name: "HaveTakenTurn",
                table: "CardsLevelOne",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAttacker",
                table: "CardsLevelOne",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDestroyed",
                table: "CardsLevelOne",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleRooms_AspNetUsers_PlayerOneId",
                table: "BattleRooms",
                column: "PlayerOneId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleRooms_AspNetUsers_PlayerTwoId",
                table: "BattleRooms",
                column: "PlayerTwoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
