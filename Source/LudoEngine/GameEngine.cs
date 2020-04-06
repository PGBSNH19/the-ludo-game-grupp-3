﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LudoEngine
{
    public class GameEngine
    {


        private int _numberOfPlayers;
        public int PiecesPerPlayer { get; set; }

        /// <summary>
        /// There has to be between 2 and 4 players.
        /// </summary>
        public int NumberOfPlayers
        {
            get { return _numberOfPlayers; }

            set
            {
                if (value < 2)
                {
                    throw new IndexOutOfRangeException("There needs to be at least two players.");
                }
                else if (value > 4)
                {
                    throw new IndexOutOfRangeException("There needs to be more than four players.");
                }

                _numberOfPlayers = value;
            }
        }

        public void TryToMoveActivePiece(GameState game, Player p, List<Piece> activePieces, int roll)
        {
            while (true)
            {
                var activePiece = PickActivePieceToMove(activePieces, Menu.PickFromList(activePieces));

                if (IsPieceClearForMoving(p, activePiece, game))
                {
                    game.MovePiece(p, activePiece, roll);
                    break;
                }
                else
                {
                    Console.WriteLine("The path is blocked, choose another piece to move.");
                    Thread.Sleep(2000);
                }
            }
        }

        public GameEngine(int numberOfPlayers, int piecesPerPlayer)
        {
            NumberOfPlayers = numberOfPlayers;
            PiecesPerPlayer = piecesPerPlayer;
        }

        public GameState StartNewGame()
        {
            var game = new GameState();
            var pieces = new List<Piece>();
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                pieces.Clear();
                var p = new Player(GetName());
                game.AddPlayer(p);

                string pieceColor = PickColor();

                for (int j = 0; j < PiecesPerPlayer; j++)
                {
                    pieces.Add(new Piece(pieceColor));
                }
                game.AddPieces(p, pieces);
            }
            return game;
        }

        public void PlayGame(GameState game)
        {
            var players = game.GetPlayers();
            game.NextPlayer = players[0];

            // break loop when last person has finished
            while (true)
            {
                // One round.
                foreach (var p in players)
                {
                    var CurrentPlayerPieces = game.GetPieces(p);
                    bool rolledSix = false;

                    // This will run if gets to roll again (rolled 6)
                    do
                    {
                        var activePieces = CurrentPlayerPieces.Where(x => x.IsActive == true).ToList();
                        var inactivePieces = CurrentPlayerPieces.Where(x => x.IsActive == false).ToList();

                        Console.Clear();
                        //här ska vi skriva ut current game state så man vet var pjäserna står.
                        Menu.PrintPlayerName(p);

                        // Promts user to roll the dice and prints the roll in the console.
                        Menu.PromtUserToRollDice();
                        Console.ReadKey();
                        int roll = Dice.Roll();
                        roll = 6;
                        Menu.PrintDiceRoll(p, roll);

                        if (roll == 1 || roll == 6)
                        {
                            UserRolledOneOrSix(game, p, activePieces, inactivePieces, roll);
                        }
                        if (roll > 1 || roll < 6)
                        {
                            UserRolledTwoToFive(game, p, activePieces, inactivePieces, roll);
                        }

                        rolledSix = (roll == 6) ? true : false;
                    } while (rolledSix);
                }



            }
        }

        public void UserRolledOneOrSix(GameState game, Player p, List<Piece> activePieces, List<Piece> inactivePieces, int roll)
        {
            var inactivePieceCount = inactivePieces.Count();
            var activePieceCount = activePieces.Count();
            bool moveActive = false;
            bool moveTwoPiecesFromYard = false;


            if (activePieceCount > 0)
            {
                moveActive = Menu.WantToMoveActivePiece();

            }

            if (!moveActive && inactivePieceCount > 0)
            {
                inactivePieces[0].Steps = roll;
            }

            if (roll == 6
                && inactivePieces.Count >= 2
                && !moveActive)
            {
                moveTwoPiecesFromYard = Menu.WantToMoveTwoPiecesFromYard();
            }

            // If the user has no pieces in yard and rolled 1 or 6.
            if ((roll == 1 || roll == 6)
                && inactivePieces.Count == 0)
            {
                TryToMoveActivePiece(game, p, activePieces, roll);
            }

            // If the user has chosen to move an ACTIVE piece. Moves if possible.
            if ((roll == 1 || roll == 6)
                && activePieces.Count > 0
                && moveActive)
            {
                TryToMoveActivePiece(game, p, activePieces, roll);
            }

            // If the user has chosen to move an INACTIVE piece to square 1, will always move.
            if (roll == 1 && inactivePieces.Count() > 0
                && !moveActive)
            {
                MoveToStart(inactivePieces);
            }

            // If the user has chosen to move an INACTIVE piece to sqaure 6 and the path is blocked by own piece.
            // Tries to move an active piece instead.
            if (roll == 6
                && inactivePieces.Count() == 1
                && !moveActive
                && IsPieceClearForMoving(p, inactivePieces[0], game))
            {
                game.MovePiece(p, inactivePieces[0], inactivePieces[0].Steps);
            }

            if (roll == 6
                && inactivePieces.Count() == 1
                && !moveActive
                && !IsPieceClearForMoving(p, inactivePieces[0], game))
            {
                Console.WriteLine("The path is blocked. You have to move an active piece.");
                Thread.Sleep(2000);
                TryToMoveActivePiece(game, p, activePieces, roll);
            }

            // If the user wants to move 2 INACTIVE pieces and there are at least 2 available. 
            // Then sets the 2 first pieces to square 1.
            if (roll == 6
                && !moveActive
                && inactivePieces.Count() > 1
                && moveTwoPiecesFromYard)
            {
                MoveToStart(inactivePieces);
                MoveToStart(inactivePieces);
            }

            // If the user has chosen to move an INACTIVE piece to sqaure 6 if possible. 
            if (roll == 6 && inactivePieces.Count() > 0
                && !moveActive
                && !moveTwoPiecesFromYard
                && IsPieceClearForMoving(p, inactivePieces[0], game))
            {
                game.MovePiece(p,inactivePieces[0],inactivePieces[0].Steps);
            }


            if (roll == 6 && inactivePieces.Count() > 0
                   && !moveActive
                   && !moveTwoPiecesFromYard
                   && !IsPieceClearForMoving(p, inactivePieces[0], game))
            {
                Console.WriteLine("The path is blocked. You have to move an active piece.");
                Thread.Sleep(2000);
                TryToMoveActivePiece(game, p, activePieces, roll);
            }
        }

        public void UserRolledTwoToFive(GameState game, Player p, List<Piece> activePieces, List<Piece> inactivePieces, int roll)
        {
            if (roll > 1 && roll < 6
                && activePieces.Count() != 0)
            {
                TryToMoveActivePiece(game, p, activePieces, roll);
            }

            if (roll > 1 && roll < 6
                && activePieces.Count() == 0)
            {
                Menu.PrintNoActivePieces();
            }
        }

        //public bool CanPieceMoveFromYard(GameState game, Player p, List<Piece> activePieces, List<Piece> inactivePieces, int roll)
        //{
        //    if (IsPieceClearForMoving(p, inactivePieces[0], game))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public bool IsPieceClearForMoving(Player currentPlayer, Piece piece, GameState game)
        {
            var playerPieces = game.GetPlayerPieces();
            // Looparna kan refaktoreras till Linq

            foreach (var p in playerPieces)
            {
                foreach (var item in p.Value)
                {
                    // Checks position of each piece in front of the pice we want to move.
                    // Checks if the position of a piece found is in the way or in the same square we want to move to. 
                    if (item.Position > piece.Position && item.Position <= piece.Position + piece.Steps)
                    {
                        if (piece.Position == 0 && item.Position == 1)
                        {
                            // If a player tries to move from 0 -> 1 and there is a piece there.
                            return true;
                        }

                        // If any of the sqaures in path are occupied.
                        else if (currentPlayer == p.Key)
                        {
                            // If it's a piece owned by current player on the same square.
                            return false;
                        }
                        else if (item.Position == piece.Position + piece.Steps)
                        {
                            // Push opponent piece back to start.
                            item.Position = 0;
                            return true;
                        }
                    }
                }
            }
            // No pieces in the way
            return true;
        }

        public string PickColor()
        {
            Console.WriteLine();
            Console.Write("Choose a color: ");
            string color = Console.ReadLine();

            // Här ska det göras ett call till databasen som kollar så att färgen som försöker väljas är ledig.

            return color;
        }

        public string GetName()
        {
            Console.WriteLine();
            Console.Write("Enter player name: ");
            string name = Console.ReadLine();

            return name;
        }

        public GameState LoadGame()
        {

            return null;
        }

        public void SaveGame()
        {

        }

        public void MoveToStart(List<Piece> inactivePieces)
        {
            inactivePieces.FirstOrDefault().IsActive = true;
            inactivePieces.FirstOrDefault().Position = 1;
        }
        public Piece PickActivePieceToMove(List<Piece> activePieces, int playerpick)
        {
            return activePieces[playerpick - 1];

        }

    }
}
