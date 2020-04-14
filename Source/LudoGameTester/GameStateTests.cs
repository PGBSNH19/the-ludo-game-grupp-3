using LudoEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;


namespace TheLudoGame
{
    [TestClass]
    public class GameStateTests
    {
        //[TestMethod]
        //public void MovePiece_ValidInput_SetPiecePositionPlusEqualToSteps()
        //{
        //    //Arrange
        //    var gamestate = new GameState();
        //    List<Piece> pieces = new List<Piece>();
        //    pieces.Add(new Piece(Colors.red));

        //    var player = new Player("Sebbe");
        //    gamestate.AddPieces(player, pieces);

        //    // Gets the right piece and sets it's position to 1.
        //    var gamePiece = gamestate.GetPieces(player).Where(x => x == pieces[0]).FirstOrDefault();
        //    gamePiece.Position = 1;

        //    //Act
        //    gamestate.MovePiece(player, pieces[0], 4);

        //    //Assert
        //    Assert.AreEqual(5, gamePiece.Position);
        //}

        //[TestMethod]
        //public void IsPieceClearForMoving_NoPieceOnTheLandingSquare_true()
        //{
        //    //Arrange
        //    var gameEngine = new GameEngine(2, 1);


        //    var gamestate = new GameState();
        //    List<Piece> pieces = new List<Piece>();
        //    var player = new Player("Sebbe");
        //    pieces.Add(new Piece(Colors.red));
        //    gamestate.AddPieces(player, pieces);
        //    var gamePiece = gamestate.GetPieces(player).Where(x => x == pieces[0]).FirstOrDefault();
        //    gamePiece.Position = 15;

        //    List<Piece> pieces2 = new List<Piece>();
        //    var player2 = new Player("kaptenen");
        //    pieces2.Add(new Piece(Colors.blue));
        //    gamestate.AddPieces(player2, pieces2);
        //    var gamePiece2 = gamestate.GetPieces(player2).Where(x => x == pieces2[0]).FirstOrDefault();
        //    gamePiece2.Position = 11;
        //    gamePiece2.Steps = 6;

        //    //Act
        //    var canMove = gameEngine.IsPieceClearForMoving(player2, pieces2[0], gamestate, gamePiece2.Steps);

        //    //Assert
        //    Assert.IsTrue(canMove);
        //}

        //[TestMethod]
        //public void IsPieceClearForMoving_OpponentPieceOnTheLandingSquare_True()
        //{
        //    //Arrange
        //    var gameEngine = new GameEngine(2, 1);

        //    var gamestate = new GameState();
        //    List<Piece> pieces = new List<Piece>();
        //    var player = new Player("Sebbe");
        //    pieces.Add(new Piece(Colors.red));
        //    gamestate.AddPieces(player, pieces);
        //    var gamePiece = gamestate.GetPieces(player).Where(x => x == pieces[0]).FirstOrDefault();
        //    gamePiece.Position = 17;


        //    List<Piece> pieces2 = new List<Piece>();
        //    var player2 = new Player("kaptenen");
        //    pieces2.Add(new Piece(Colors.blue));
        //    gamestate.AddPieces(player2, pieces2);
        //    var gamePiece2 = gamestate.GetPieces(player2).Where(x => x == pieces2[0]).FirstOrDefault();
        //    gamePiece2.Position = 11;
        //    gamePiece2.Steps = 6;


        //    //Act
        //    var canMove = gameEngine.IsPieceClearForMoving(player2, pieces2[0], gamestate, gamePiece2.Steps);

        //    //Assert
        //    Assert.IsTrue(canMove);
        //}

        //[TestMethod]
        //public void IsPieceClearForMoving_CurrentPlayerPieceOnPath_False()
        //{
        //    //Arrange
        //    var gameEngine = new GameEngine(2, 1);
        //    var gamestate = new GameState();

        //    List<Piece> pieces = new List<Piece>();
        //    var player2 = new Player("kaptenen");
        //    pieces.Add(new Piece(Colors.blue));
        //    pieces.Add(new Piece(Colors.blue));
        //    gamestate.AddPieces(player2, pieces);
        //    var gamePiece1 = gamestate.GetPieces(player2).Where(x => x == pieces[0]).FirstOrDefault();
        //    var gamePiece2 = gamestate.GetPieces(player2).Where(x => x == pieces[1]).FirstOrDefault();

        //    gamePiece1.Position = 9;
        //    gamePiece1.Steps = 0;

        //    gamePiece2.Position = 5;
        //    gamePiece2.Steps = 6;

        //    //Act
        //    var canMove = gameEngine.IsPieceClearForMoving(player2, pieces[1], gamestate, gamePiece2.Steps);

        //    //Assert
        //    Assert.IsFalse(canMove);
        //}

        //[TestMethod]
        //public void IsPieceClearForMoving_CurrentPlayerPieceOnLandingSquare_False()
        //{
        //    //Arrange
        //    var gameEngine = new GameEngine(2, 1);
        //    var gamestate = new GameState();

        //    List<Piece> pieces = new List<Piece>();
        //    var player2 = new Player("kaptenen");
        //    pieces.Add(new Piece(Colors.blue));
        //    pieces.Add(new Piece(Colors.blue));
        //    gamestate.AddPieces(player2, pieces);
        //    var gamePiece1 = gamestate.GetPieces(player2).Where(x => x == pieces[0]).FirstOrDefault();
        //    var gamePiece2 = gamestate.GetPieces(player2).Where(x => x == pieces[1]).FirstOrDefault();

        //    gamePiece1.Position = 11;
        //    gamePiece1.Steps = 0;

        //    gamePiece2.Position = 5;
        //    gamePiece2.Steps = 6;

        //    //Act
        //    var canMove = gameEngine.IsPieceClearForMoving(player2, pieces[1], gamestate, gamePiece2.Steps);

        //    //Assert
        //    Assert.IsFalse(canMove);
        //}

        //[TestMethod]
        //public void IsPieceClearForMoving_NoPieceOnPath_True()
        //{
        //    //Arrange
        //    var gamestate = new GameState();
        //    var gameEngine = new GameEngine(2, 1);

        //    List<Piece> pieces = new List<Piece>();
        //    var player = new Player("Sebbe");
        //    pieces.Add(new Piece(Colors.red));
        //    gamestate.AddPieces(player, pieces);
        //    var gamePiece = gamestate.GetPieces(player).Where(x => x == pieces[0]).FirstOrDefault();
        //    gamePiece.Position = 25;

        //    List<Piece> pieces2 = new List<Piece>();
        //    var player2 = new Player("kaptenen");
        //    pieces2.Add(new Piece(Colors.blue));
        //    gamestate.AddPieces(player2, pieces2);
        //    var gamePiece2 = gamestate.GetPieces(player2).Where(x => x == pieces2[0]).FirstOrDefault();
        //    gamePiece2.Position = 11;
        //    gamePiece2.Steps = 6;


        //    //Act
        //    var canMove = gameEngine.IsPieceClearForMoving(player2, pieces2[0], gamestate, gamePiece2.Steps);

        //    //Assert
        //    Assert.IsTrue(canMove);
        //}

        //#region PickActivePieceToMove
        //[TestMethod]
        //public void PickActivePieceToMove_ThereAreActivePieces_True()
        //{

        //    var gameEngine = new GameEngine(2, 3);
        //    //Arrange
        //    var activePieceList = new List<Piece>
        //    {
        //        new Piece(Colors.red),
        //        new Piece(Colors.red),
        //        new Piece(Colors.red)
        //    };

        //    int playerpick = 3;
        //    //Act
        //    var result = gameEngine.PickActivePieceToMove(activePieceList, playerpick);

        //    //Assert
        //    Assert.AreEqual(activePieceList[2], result);
        //}

        //[TestMethod]
        //public void PlayGame_RolledOneThereIsInActivePieces_True()
        //{
        //    //Arrange
        //    var gamestate = new GameState();
        //    var gameEngine = new GameEngine(2, 1);

        //    List<Piece> pieces = new List<Piece>();
        //    var player = new Player("Sebbe");
        //    pieces.Add(new Piece("Red"));
        //    gamestate.AddPieces(player, pieces);
        //    var gamePiece = gamestate.GetPieces(player).Where(x => x == pieces[0]).FirstOrDefault();
        //    gamePiece.Position = 0;

        //    //Act
        //    gameEngine.PlayGame(gamestate);
        //    //Assert

        //}


    }
}
