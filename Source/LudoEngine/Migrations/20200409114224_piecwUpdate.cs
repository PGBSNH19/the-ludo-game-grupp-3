using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoEngine.Migrations
{
    public partial class piecwUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piece_Player_PlayerID",
                table: "Piece");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerID",
                table: "Piece",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Piece_Player_PlayerID",
                table: "Piece",
                column: "PlayerID",
                principalTable: "Player",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piece_Player_PlayerID",
                table: "Piece");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerID",
                table: "Piece",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Piece_Player_PlayerID",
                table: "Piece",
                column: "PlayerID",
                principalTable: "Player",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
