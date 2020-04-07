﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LudoEngine
{
    public class GameState
    {
        public int ID { get; set; }
        public int NextPlayerID { get; set; }
        public Player NextPlayer { get; set; }
        public bool HasFinished { get; set; }

        List<Player> players;
        // public EFKeyValuePair<Player, List<Piece>> EFKeyValuePair { get; set; }
        public Dictionary<Player, List<Piece>> playerPieces { get; set; }

        #region Constructors
        public GameState()
        {
            players = new List<Player>();
            playerPieces = new Dictionary<Player, List<Piece>>();
        }


        /// <summary>
        ///This censtruktor is for reading in a whole game, mostly the parameterless ctor will be used!
        ///Every GameState takes each player and it's pieces in list form. 
        ///Saves the players to a temp list.
        ///Both the players and each of it's pieces will then be saved in dictionary PlayerPieces.
        /// </summary>
        public GameState(Player playerOne, List<Piece> playerOnePieces
            , Player playerTwo, List<Piece> playerTwoPieces
            , Player playerThree = null, List<Piece> playerThreePieces = null
            , Player playerFour = null, List<Piece> playerFourPieces = null)
            : base()
        {

            playerPieces = new Dictionary<Player, List<Piece>>();

            players.Add(playerOne);
            players.Add(playerTwo);
            players.Add(playerThree);
            players.Add(playerFour);


            foreach (var player in players)
            {
                if (player != null)
                {
                    //PlayerPieces.Add(player, playerOnePieces[0]);
                    //PlayerPieces.Add(player, playerOnePieces[1]);
                    //PlayerPieces.Add(player, playerOnePieces[2]);
                    //PlayerPieces.Add(player, playerOnePieces[3]);
                }
            }
        }
        #endregion

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public List<Player> GetPlayers()
        {
            return players;
        }
        public void AddPieces(Player player, List<Piece> pieces)
        {
            playerPieces.Add(player, pieces);
        }

        public List<Piece> GetPieces(Player player)
        {
            return playerPieces[player];
        }

        public void MovePiece(Player player, Piece piece, int steps)
        {
            var correctPiece = playerPieces[player].Where(x => x == piece).FirstOrDefault();
            correctPiece.Steps = steps;


            Console.Write($"You moved {correctPiece.Steps} steps from square {correctPiece.Position} ");


            correctPiece.Position += correctPiece.Steps;

            if (correctPiece.Position == 46)
            {
                correctPiece.HasFinished = true;
                correctPiece.IsActive = false;
                Console.WriteLine($"Piece: {correctPiece.ID} of {correctPiece.Color} has finished.");


            }
            else
            {
                Console.Write($"and landed on square {correctPiece.Position}.\n");
                correctPiece.IsActive = true;
                correctPiece.Steps = 0;
            }

            Thread.Sleep(2500);
        }

        public Dictionary<Player, List<Piece>> GetPlayerPieces()
        {
            return playerPieces;
        }

        public override string ToString()
        {
            if (players.Count == 2)
            {

                return $"Players in this game {ID}: {players[0].Name}, {players[1].Name}. ";
            }
            else if (players.Count == 3)
            {
                return $"Players in this game {ID}: {players[0].Name}, {players[1].Name}, {players[2].Name}. ";

            }
            else
            {
                return $"Players in this game {ID}: {players[0].Name}, {players[1].Name}, {players[2].Name}, {players[3].Name}. ";

            }
        }
    }
}