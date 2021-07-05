using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class omarbetatGameState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameState_Player_NextPlayerID",
                table: "GameState");

            migrationBuilder.DropIndex(
                name: "IX_GameState_NextPlayerID",
                table: "GameState");

            migrationBuilder.DropColumn(
                name: "NextPlayerID",
                table: "GameState");

            migrationBuilder.AddColumn<int>(
                name: "GameStateID",
                table: "Player",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMyTurn",
                table: "Player",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Player_GameStateID",
                table: "Player",
                column: "GameStateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_GameState_GameStateID",
                table: "Player",
                column: "GameStateID",
                principalTable: "GameState",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_GameState_GameStateID",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_GameStateID",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "GameStateID",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "IsMyTurn",
                table: "Player");

            migrationBuilder.AddColumn<int>(
                name: "NextPlayerID",
                table: "GameState",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
