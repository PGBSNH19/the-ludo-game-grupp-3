using LudoEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;


namespace TheLudoGame
{
    [TestClass]
    public class GameStateTests
    {

        #region IsPieceClearForMoving

        [TestMethod]
        public void IsPieceClearForMoving_NoPieceOnTheLandingSquare_true()
        {
            //Arrange
            var gameEngine = new GameEngine(2, 1);
            var gamestate = new GameState();
            gamestate.Players.Add(new Player("Sebbe"));
            var player = gamestate.Players[0];

            player.Pieces.Add(new Piece(Colors.red));
            player.Pieces.Add(new Piece(Colors.red));

            player.Pieces.ForEach(x => x.Position = 1);
            player.Pieces.ForEach(x => x.IsActive = true);

            //Act
            var canMove = gameEngine.IsPieceClearForMoving(player, player.Pieces[0], gamestate, 4);

            //Assert
            Assert.IsTrue(canMove);
        }

        [TestMethod]
        public void IsPieceClearForMoving_OpponentPieceOnTheLandingSquare_True()
        {
            //Arrange
            var gameEngine = new GameEngine(2, 1);
            var gamestate = new GameState();

            //Player1
            gamestate.Players.Add(new Player("Sebbe"));
            var playerone = gamestate.Players[0];
            playerone.Pieces.Add(new Piece(Colors.red));
            playerone.Pieces[0].Position = 6;
            playerone.Pieces[0].IsActive = true;

            //Player2
            gamestate.Players.Add(new Player("Eric"));
            var playertwo = gamestate.Players[1];
            playertwo.Pieces.Add(new Piece(Colors.blue));
            playertwo.Pieces[0].Position = 1;
            playertwo.Pieces[0].IsActive = true;

            //Act
            var canMove = gameEngine.IsPieceClearForMoving(playerone, playerone.Pieces[0], gamestate, 5);

            //Assert
            Assert.IsTrue(canMove);
        }

        [TestMethod]
        public void IsPieceClearForMoving_CurrentPlayerPieceOnPath_False()
        {
            //Arrange
            var gameEngine = new GameEngine(2, 1);
            var gamestate = new GameState();

            gamestate.Players.Add(new Player("Sebbe"));
            var playerone = gamestate.Players[0];
            playerone.Pieces.Add(new Piece(Colors.red));
            playerone.Pieces.Add(new Piece(Colors.red));

            playerone.Pieces[0].Position = 2;
            playerone.Pieces[0].IsActive = true;

            playerone.Pieces[1].Position = 6;
            playerone.Pieces[1].IsActive = true;

            //Act
            var canMove = gameEngine.IsPieceClearForMoving(playerone, playerone.Pieces[0], gamestate, 6);

            //Assert
            Assert.IsFalse(canMove);
        }

        [TestMethod]
        public void IsPieceClearForMoving_CurrentPlayerPieceOnLandingSquare_False()
        {
            //Arrange
            var gameEngine = new GameEngine(2, 1);
            var gamestate = new GameState();

            gamestate.Players.Add(new Player("Sebbe"));
            var playerone = gamestate.Players[0];
            playerone.Pieces.Add(new Piece(Colors.red));
            playerone.Pieces.Add(new Piece(Colors.red));

            playerone.Pieces[0].Position = 2;
            playerone.Pieces[0].IsActive = true;

            playerone.Pieces[1].Position = 6;
            playerone.Pieces[1].IsActive = true;

            //Act
            var canMove = gameEngine.IsPieceClearForMoving(playerone, playerone.Pieces[0], gamestate, 4);

            //Assert
            Assert.IsFalse(canMove);
        }

        [TestMethod]
        public void IsPieceClearForMoving_NoPieceOnPath_True()
        {
            //Arrange
            var gameEngine = new GameEngine(2, 1);
            var gamestate = new GameState();

            gamestate.Players.Add(new Player("Sebbe"));
            var playerone = gamestate.Players[0];
            playerone.Pieces.Add(new Piece(Colors.red));
            playerone.Pieces.Add(new Piece(Colors.red));

            playerone.Pieces[0].Position = 2;
            playerone.Pieces[0].IsActive = true;

            playerone.Pieces[1].Position = 39;
            playerone.Pieces[1].IsActive = true;

            //Act
            var canMove = gameEngine.IsPieceClearForMoving(playerone, playerone.Pieces[0], gamestate, 4);

            //Assert
            Assert.IsTrue(canMove);
        }

        #endregion

        [TestMethod]
        public void PickActivePieceToMove_ThereAreActivePieces_True()
        {
            var gameEngine = new GameEngine(2, 3);
            //Arrange
            var activePieceList = new List<Piece>
            {
                new Piece(Colors.red),
                new Piece(Colors.red),
                new Piece(Colors.red)
            };

            int playerpick = 2;
            //Act
            var result = gameEngine.PickActivePieceToMove(activePieceList, playerpick);

            //Assert
            Assert.AreEqual(activePieceList[2], result);
        }

        [TestMethod]
        public void MoveToStart_PiecePositionÍsOne_True()
        {
            //Arrange
            var gamestate = new GameState();
            var gameEngine = new GameEngine(2, 1);
            var list = new List<Piece> { new Piece(Colors.green) };

            //Act
            gameEngine.MoveToStart(list);
            //Assert
            Assert.AreEqual(list[0].IsActive, true);
            Assert.AreEqual(list[0].Position, 1);
        }

        [TestMethod]
        public void CheckForWinner_AllPiecesHasFinished_true()
        {
            //Arrange
            var gameEngine = new GameEngine();
            var gamestate = new GameState();

            gamestate.Players.Add(new Player("Sebbe"));
            var playerone = gamestate.Players[0];
            playerone.Pieces.Add(new Piece(Colors.red));
            playerone.Pieces.Add(new Piece(Colors.red));
            playerone.Pieces.Add(new Piece(Colors.red));
            playerone.Pieces.Add(new Piece(Colors.red));

            playerone.Pieces.ForEach(x => x.HasFinished = true);

            //Act
            var result = gameEngine.CheckForWinner(playerone);
            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CheckForWinner_AllPiecesHasFinished_False()
        {
            //Arrange
            var gameEngine = new GameEngine();
            var gamestate = new GameState();

            gamestate.Players.Add(new Player("Sebbe"));
            var playerone = gamestate.Players[0];
            playerone.Pieces.Add(new Piece(Colors.red));
            playerone.Pieces.Add(new Piece(Colors.red));
            playerone.Pieces.Add(new Piece(Colors.red));
            playerone.Pieces.Add(new Piece(Colors.red));

            for (int i = 0; i < 3; i++)
            {
                playerone.Pieces[i].HasFinished=true;
            }

            //Act
            var result = gameEngine.CheckForWinner(playerone);
            //Assert
            Assert.IsFalse(result);
        }
    }
}
