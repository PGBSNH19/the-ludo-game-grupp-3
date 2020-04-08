using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class uppdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameState_Player_NextPlayerID1",
                table: "GameState");

            migrationBuilder.DropIndex(
                name: "IX_GameState_NextPlayerID1",
                table: "GameState");

            migrationBuilder.DropColumn(
                name: "GameStateID",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "NextPlayerID1",
                table: "GameState");

            migrationBuilder.CreateIndex(
                name: "IX_GameState_NextPlayerID",
                table: "GameState",
                column: "NextPlayerID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameState_Player_NextPlayerID",
                table: "GameState",
                column: "NextPlayerID",
                principalTable: "Player",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameState_Player_NextPlayerID",
                table: "GameState");

            migrationBuilder.DropIndex(
                name: "IX_GameState_NextPlayerID",
                table: "GameState");

            migrationBuilder.AddColumn<int>(
                name: "GameStateID",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NextPlayerID1",
                table: "GameState",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameState_NextPlayerID1",
                table: "GameState",
                column: "NextPlayerID1");

            migrationBuilder.AddForeignKey(
                name: "FK_GameState_Player_NextPlayerID1",
                table: "GameState",
                column: "NextPlayerID1",
                principalTable: "Player",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
