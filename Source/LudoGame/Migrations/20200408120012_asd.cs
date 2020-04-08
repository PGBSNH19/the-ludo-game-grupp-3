using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoGame.Migrations
{
    public partial class asd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerID",
                table: "Piece",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Piece_PlayerID",
                table: "Piece",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Piece_Player_PlayerID",
                table: "Piece",
                column: "PlayerID",
                principalTable: "Player",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piece_Player_PlayerID",
                table: "Piece");

            migrationBuilder.DropIndex(
                name: "IX_Piece_PlayerID",
                table: "Piece");

            migrationBuilder.DropColumn(
                name: "PlayerID",
                table: "Piece");
        }
    }
}
