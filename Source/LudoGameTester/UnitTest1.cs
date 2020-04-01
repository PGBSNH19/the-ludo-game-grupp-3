using LudoEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;


namespace LudoGameTester
{
    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        public void GetPlayers_ValidPlayers_True()
        {
            //Arrange
            var gameState = new GameState();
            var player1 = new Player("Sebastian");
            var player2 = new Player("Eric");

            //Act
            gameState.AddPlayer(player1);
            gameState.AddPlayer(player2);
            var playerList = gameState.GetPlayers();

            //Assert
            Assert.AreEqual(playerList[0], player1);
            Assert.AreEqual(playerList[1], player2);
        }

        [TestMethod]
        public void GetPices_ValidPlayersAndPieces_True()
        {
            //Arrange
            var gamestate = new GameState();

            List<Piece> pieces = new List<Piece>();
            pieces.Add(new Piece("Red"));
            pieces.Add(new Piece("Red"));

            var player = new Player("Sebbe");

            //Act
            gamestate.AddPieces(player, pieces);
            var returnList = gamestate.GetPieces(player);

            //Assert
            Assert.AreEqual(pieces, returnList);
        }

        [TestMethod]
        public void MovePiece_ValidInput_SetPiecePositionPlusEqualToSteps()
        {
            //Arrange
            var gamestate = new GameState();
            List<Piece> pieces = new List<Piece>();
            pieces.Add(new Piece("Red"));

            var player = new Player("Sebbe");
            gamestate.AddPieces(player, pieces);

            // Gets the right piece and sets it's position to 1.
            var gamePiece = gamestate.GetPieces(player).Where(x => x == pieces[0]).FirstOrDefault();
            gamePiece.Position = 1;

            //Act
            gamestate.MovePiece(player, pieces[0], 4);

            //Assert
            Assert.AreEqual(5, gamePiece.Position);
        }

        [TestMethod]
        public void IsPieceClearForMoving_NoPieceOnTheLandingSquare_true()
        {
            //Arrange
            var gamestate = new GameState();
            List<Piece> pieces = new List<Piece>();
            var player = new Player("Sebbe");
            pieces.Add(new Piece("Red"));
            gamestate.AddPieces(player, pieces);
            var gamePiece = gamestate.GetPieces(player).Where(x => x == pieces[0]).FirstOrDefault();
            gamePiece.Position = 15;

            List<Piece> pieces2 = new List<Piece>();
            var player2 = new Player("kaptenen");
            pieces2.Add(new Piece("blue"));
            gamestate.AddPieces(player2, pieces2);
            var gamePiece2 = gamestate.GetPieces(player2).Where(x => x == pieces2[0]).FirstOrDefault();
            gamePiece2.Position = 11;
            gamePiece2.Steps = 6;


            //Act
            var canMove = gamestate.IsPieceClearForMoving(player2, pieces2[0]);

            //Assert
            Assert.IsTrue(canMove);
        }

        [TestMethod]
        public void IsPieceClearForMoving_OpponentPieceOnTheLandingSquare_True()
        {
            //Arrange
            var gamestate = new GameState();
            List<Piece> pieces = new List<Piece>();
            var player = new Player("Sebbe");
            pieces.Add(new Piece("Red"));
            gamestate.AddPieces(player, pieces);
            var gamePiece = gamestate.GetPieces(player).Where(x => x == pieces[0]).FirstOrDefault();
            gamePiece.Position = 17;
            

            List<Piece> pieces2 = new List<Piece>();
            var player2 = new Player("kaptenen");
            pieces2.Add(new Piece("blue"));
            gamestate.AddPieces(player2, pieces2);
            var gamePiece2 = gamestate.GetPieces(player2).Where(x => x == pieces2[0]).FirstOrDefault();
            gamePiece2.Position = 11;
            gamePiece2.Steps = 6;


            //Act
            var canMove = gamestate.IsPieceClearForMoving(player2, pieces2[0]);

            //Assert
            Assert.IsTrue(canMove);
        }


        [TestMethod]
        public void IsPieceClearForMoving_CurrentPlayerPieceOnPath_False()
        {
            //Arrange
            var gamestate = new GameState();
           
            List<Piece> pieces = new List<Piece>();
            var player2 = new Player("kaptenen");
            pieces.Add(new Piece("blue"));
            pieces.Add(new Piece("blue"));
            gamestate.AddPieces(player2, pieces);
            var gamePiece1 = gamestate.GetPieces(player2).Where(x => x == pieces[0]).FirstOrDefault();
            var gamePiece2 = gamestate.GetPieces(player2).Where(x => x == pieces[1]).FirstOrDefault();

            gamePiece1.Position = 9;
            gamePiece1.Steps = 0;

            gamePiece2.Position = 5;
            gamePiece2.Steps = 6;

            //Act
            var canMove = gamestate.IsPieceClearForMoving(player2, pieces[1]);

            //Assert
            Assert.IsFalse(canMove);
        }


        [TestMethod]
        public void IsPieceClearForMoving_CurrentPlayerPieceOnLandingSquare_False()
        {
            //Arrange
            var gamestate = new GameState();

            List<Piece> pieces = new List<Piece>();
            var player2 = new Player("kaptenen");
            pieces.Add(new Piece("blue"));
            pieces.Add(new Piece("blue"));
            gamestate.AddPieces(player2, pieces);
            var gamePiece1 = gamestate.GetPieces(player2).Where(x => x == pieces[0]).FirstOrDefault();
            var gamePiece2 = gamestate.GetPieces(player2).Where(x => x == pieces[1]).FirstOrDefault();

            gamePiece1.Position = 11;
            gamePiece1.Steps = 0;

            gamePiece2.Position = 5;
            gamePiece2.Steps = 6;

            //Act
            var canMove = gamestate.IsPieceClearForMoving(player2, pieces[1]);

            //Assert
            Assert.IsFalse(canMove);
        }

        [TestMethod]
        public void IsPieceClearForMoving_NoPieceOnPath_True()
        {
            //Arrange
            var gamestate = new GameState();
            List<Piece> pieces = new List<Piece>();
            var player = new Player("Sebbe");
            pieces.Add(new Piece("Red"));
            gamestate.AddPieces(player, pieces);
            var gamePiece = gamestate.GetPieces(player).Where(x => x == pieces[0]).FirstOrDefault();
            gamePiece.Position = 25;

            List<Piece> pieces2 = new List<Piece>();
            var player2 = new Player("kaptenen");
            pieces2.Add(new Piece("blue"));
            gamestate.AddPieces(player2, pieces2);
            var gamePiece2 = gamestate.GetPieces(player2).Where(x => x == pieces2[0]).FirstOrDefault();
            gamePiece2.Position = 11;
            gamePiece2.Steps = 6;


            //Act
            var canMove = gamestate.IsPieceClearForMoving(player2, pieces2[0]);

            //Assert
            Assert.IsTrue(canMove);
        }

        //[TestMethod]
        //public void ()
        //{
        //    //Arrange

        //    //Act

        //    //Assert

        //}


    }
}
