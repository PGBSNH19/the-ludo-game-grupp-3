using LudoEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            Assert.AreEqual(pieces,returnList);
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
