using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheDarkPortal.Data.Migrations
{
    public partial class AddBattleCardAndRoomEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BattleCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tire = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<double>(type: "float", nullable: false),
                    CurrentPower = table.Column<double>(type: "float", nullable: false),
                    Defense = table.Column<double>(type: "float", nullable: false),
                    CurrentDefense = table.Column<double>(type: "float", nullable: false),
                    TotalHealth = table.Column<double>(type: "float", nullable: false),
                    CurrentHealth = table.Column<double>(type: "float", nullable: false),
                    Element = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BattleRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerOneId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlayerTwoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsFirstPlayerTurn = table.Column<bool>(type: "bit", nullable: false),
                    TimeLeftInTurn = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BattleRooms_AspNetUsers_PlayerOneId",
                        column: x => x.PlayerOneId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BattleRooms_AspNetUsers_PlayerTwoId",
                        column: x => x.PlayerTwoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserBattleCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BattleCardId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBattleCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBattleCards_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBattleCards_BattleCards_BattleCardId",
                        column: x => x.BattleCardId,
                        principalTable: "BattleCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BattleCards_IsDeleted",
                table: "BattleCards",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BattleRooms_IsDeleted",
                table: "BattleRooms",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BattleRooms_PlayerOneId",
                table: "BattleRooms",
                column: "PlayerOneId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleRooms_PlayerTwoId",
                table: "BattleRooms",
                column: "PlayerTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBattleCards_BattleCardId",
                table: "UserBattleCards",
                column: "BattleCardId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBattleCards_IsDeleted",
                table: "UserBattleCards",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserBattleCards_UserId",
                table: "UserBattleCards",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattleRooms");

            migrationBuilder.DropTable(
                name: "UserBattleCards");

            migrationBuilder.DropTable(
                name: "BattleCards");
        }
    }
}
