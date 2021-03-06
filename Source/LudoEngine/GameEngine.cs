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

        public GameEngine()
        {

        }

        public GameEngine(int numberOfPlayers, int piecesPerPlayer)
        {
            NumberOfPlayers = numberOfPlayers;
            PiecesPerPlayer = piecesPerPlayer;
        }

        private string GetName()
        {
            Console.WriteLine();
            Console.Write("Enter player name: ");
            string name = Console.ReadLine();

            return name;
        }

        public GameState StartNewGame()
        {
            var game = new GameState();

            List<string> availableColors = new List<string> { "red", "green", "yellow", "blue" };

            for (int i = 0; i < NumberOfPlayers; i++)
            {
                var pieces = new List<Piece>();
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine($"Player {i + 1}");

                // Clears the pieces list for the next player pick.
                pieces.Clear();

                var p = new Player(GetName());
                game.Players.Add(p);
                Colors pieceColor = Piece.PickColor(Menu.MenuOptions(availableColors, "What color do you want to play?"));

                for (int j = 0; j < PiecesPerPlayer; j++)
                {
                    pieces.Add(new Piece(pieceColor));
                }

                // Adds the pieces to the current gamestate.
                game.Players[i].Pieces = pieces;
                // Removes the color as an alternative for the next player to choose.
                availableColors.Remove(pieces[0].Color.ToString());

                Console.WriteLine();
                Console.WriteLine($"{p.Name} choose color {pieces[0].Color} and has been added to the game.");
                Thread.Sleep(2000);
            }
            //Sets the first player so be the start player.
            game.Players[0].IsMyTurn = true;
            game.SaveGame(game);
            return game;
        }

        public void PlayGame(GameState game)
        {
            // break loop when last person has finished
            while (true)
            {
                var activePlayer = game.Players.SingleOrDefault(x => x.IsMyTurn);
                bool rolledSix = false;

                // This will run if gets to roll again (rolled 6)
                do
                {
                    // Gets the current players pieces from the database and divides them into lists based on inactive/active.
                    var CurrentPlayerPieces = game.getPiecesFromDatabase(activePlayer);
                    var activePieces = GetPlayersActivePieces(CurrentPlayerPieces);
                    var inactivePieces = GetPlayersInactivePieces(CurrentPlayerPieces);

                    Console.Clear();
                    Menu.PrintPlayerName(activePlayer);

                    // Promts user to roll the dice and prints the roll in the console.
                    Menu.PromtUserToRollDice();
                    Console.ReadKey();
                    int roll = Dice.Roll();
                    Menu.PrintDiceRoll(activePlayer, roll);

                    if (roll == 1 || roll == 6)
                    {
                        UserRolledOneOrSix(game, activePlayer, activePieces, inactivePieces, roll);
                    }
                    if (roll > 1 && roll < 6)
                    {
                        UserRolledTwoToFive(game, activePlayer, activePieces, inactivePieces, roll);
                    }

                    // If someone wins run Main menu.
                    rolledSix = (roll == 6) ? true : false;
                    if (CheckForWinner(activePlayer))
                    {
                        Menu.MainMenu(
                            Menu.MenuOptions(new List<string>
                            { "Start New Game", "Load Unfinished Games", "Show Finished Games" }, "Options"));
                    }
                } while (rolledSix);

                //Changes the turn.
                game.ChangePlayerTurn(activePlayer);
                game.context.SaveChanges();
            }
        }

        public bool CheckForWinner(Player p)
        {
            var finishPiecesCount = p.Pieces.Where(x => x.HasFinished).Count();
            if (finishPiecesCount == 4)
            {
                p.IsWinner = true;

                Console.WriteLine();
                Console.WriteLine("YOU WON,END OF GAME! ");
                Console.WriteLine();

                Thread.Sleep(2000);
                return true;
            }

            return false;
        }

        private List<Piece> GetPlayersActivePieces(List<Piece> CurrentPlayerPieces)
        {
            var pieces = new List<Piece>();

            CurrentPlayerPieces.ForEach(x =>
            {
                if (x.IsActive)
                {
                    pieces.Add(x);
                }
            });

            return pieces;
        }

        private List<Piece> GetPlayersInactivePieces(List<Piece> CurrentPlayerPieces)
        {
            var pieces = new List<Piece>();

            CurrentPlayerPieces.ForEach(x =>
             {
                 if (!x.IsActive)
                 {
                     pieces.Add(x);
                 }
             });

            return pieces;
        }

        private void UserRolledOneOrSix(GameState game, Player p, List<Piece> activePieces, List<Piece> inactivePieces, int roll)
        {
            var inactivePieceCount = inactivePieces.Count();
            var activePieceCount = activePieces.Count();
            bool moveActive = false;
            bool moveTwoPiecesFromYard = false;

            // If the user chooses to move an ACTIVE piece, moveActive == true.
            if (activePieceCount > 0)
            {
                moveActive = Menu.WantToMoveActivePiece(Menu.MenuOptions(new List<string> { "yes", "no" }, "Do you want to move an active piece?"));
            }

            // If the user chooses to move an 2 INACTIVE piece, moveTwoPiecesFromYard == true.
            if (roll == 6
                && inactivePieceCount >= 2
                && !moveActive)
            {
                moveTwoPiecesFromYard = Menu.WantToMoveTwoPiecesFromYard();
            }


            // If the user has no pieces in yard and rolled 1 or 6.
            if ((roll == 1 || roll == 6)
                && inactivePieceCount == 0)
            {
                TryToMoveActivePiece(game, p, activePieces, roll);
            }

            // If the user has chosen to move an ACTIVE piece. Moves if possible.
            else if ((roll == 1 || roll == 6)
                && activePieceCount > 0
                && inactivePieceCount != 0
                && moveActive)
            {
                TryToMoveActivePiece(game, p, activePieces, roll);
            }

            // If the user has chosen to move an INACTIVE piece to square 1, will always move.
            else if (roll == 1 && inactivePieceCount > 0
                && !moveActive)
            {
                MoveToStart(inactivePieces);
                Thread.Sleep(2000);
            }

            // If the user has chosen to move an INACTIVE piece to sqaure 6 and the path is blocked by own piece.
            // Tries to move an active piece instead.
            else if (roll == 6
                && inactivePieceCount == 1
                && !moveActive
                && IsPieceClearForMoving(p, inactivePieces[0], game, roll))
            {
                game.MovePiece(p, inactivePieces[0], roll);
                inactivePieces.RemoveAt(0);
            }

            // If the user has chosen to move an INACTIVE piece to sqaure 6 and player only has 1 inactive piece.
            else if (roll == 6
                && inactivePieceCount == 1
                && !moveActive
                && !IsPieceClearForMoving(p, inactivePieces[0], game, roll))
            {
                Console.WriteLine();
                Console.WriteLine("The path is blocked. You have to move an active piece.");
                Thread.Sleep(2000);
                TryToMoveActivePiece(game, p, activePieces, roll);
            }

            // If the user wants to move 2 INACTIVE pieces and there are at least 2 available. 
            // Then sets the 2 first pieces to square 1.
            else if (roll == 6
                && !moveActive
                && inactivePieceCount > 1
                && moveTwoPiecesFromYard)
            {
                MoveToStart(inactivePieces);
                Thread.Sleep(2000);
                MoveToStart(inactivePieces);
                Thread.Sleep(2000);
            }

            // If the user has chosen to move an INACTIVE piece to sqaure 6 if possible. 
            else if (roll == 6 && inactivePieceCount > 0
                && !moveActive
                && !moveTwoPiecesFromYard
                && IsPieceClearForMoving(p, inactivePieces[0], game, roll))
            {
                game.MovePiece(p, inactivePieces[0], roll);
            }

            else if (roll == 6 && inactivePieceCount > 0
                   && !moveActive
                   && !moveTwoPiecesFromYard
                   && !IsPieceClearForMoving(p, inactivePieces[0], game, roll))
            {
                Console.WriteLine();
                Console.WriteLine("The path is blocked. You have to move an active piece.");
                Thread.Sleep(2000);
                TryToMoveActivePiece(game, p, activePieces, roll);
            }
        }

        private void UserRolledTwoToFive(GameState game, Player p, List<Piece> activePieces, List<Piece> inactivePieces, int roll)
        {
            if (roll > 1 && roll < 6
                && activePieces.Count() != 0)
            {
                TryToMoveActivePiece(game, p, activePieces, roll);
            }

            if (roll > 1 && roll < 6
                && activePieces.Count() == 0)
            {
                Console.WriteLine();
                Menu.PrintNoActivePieces();
            }
        }

        public bool IsPieceClearForMoving(Player currentPlayer, Piece piece, GameState game, int roll)
        {
            foreach (var p in game.Players)
            {
                int relativePosition = currentPlayer.GetRelativePositionOfOpponent(game, p);

                foreach (var opponentPiece in p.Pieces)
                {
                    // Checks position of each piece in front of the pice we want to move.
                    // Checks if the position of a piece found is in the way or in the same square we want to move to. 

                    if (piece.Position == 0 && piece.Position + roll == 1)
                    {
                        // If a player tries to move from 0 -> 1 and there is a piece there.
                        return true;
                    }

                    // If any of the sqaures in path are occupied.
                    if ((opponentPiece.Position > piece.Position
                        && opponentPiece.Position <= (piece.Position + roll)
                        && opponentPiece.HasFinished == false))
                    {
                        // If it's a piece owned by current player on the same square.
                        if (currentPlayer == p)
                        {
                            return false;
                        }
                        else if ((opponentPiece.Position + relativePosition == piece.Position + roll)
                            && piece.Position < 41)
                        {
                            // Push opponent piece back to start.
                            opponentPiece.IsActive = false;
                            opponentPiece.Position = 0;
                            return true;
                        }
                        else if (piece.Position + roll <= 46)
                        {
                            return true;
                        }
                        else if (piece.Position + roll > 46)
                        {
                            return false;
                        }
                    }
                }
            }
            if (piece.Position + roll > 46)
            {
                Console.WriteLine("");
                Console.WriteLine($"You need to roll a {46 - piece.Position} to finnish with your piece.");
                Thread.Sleep(2000);
                return false;
            }

            // No pieces in the way
            return true;
        }

        public void TryToMoveActivePiece(GameState game, Player p, List<Piece> activePieces, int roll)
        {
            var availableActivePieces = activePieces;

            while (true)
            {
                var activePiece = PickActivePieceToMove(activePieces, Menu.PickPieceFromList(availableActivePieces));
                if (IsPieceClearForMoving(p, activePiece, game, roll))
                {
                    game.MovePiece(p, activePiece, roll);
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("The path is blocked, choose another piece to move.");
                    availableActivePieces.Remove(activePiece);
                    Thread.Sleep(2000);
                }

                if (activePieces.Count == 0 && (roll != 1 || roll != 6))
                {
                    Console.WriteLine();
                    Console.WriteLine("You can't move an active piece.");
                    Thread.Sleep(2000);
                    break;
                }
                else if (activePieces.Count == 0 && (roll == 1 || roll == 6))
                {
                    Console.WriteLine();
                    Console.WriteLine("You can't move an active piece, tries to move an inactive piece.");
                    UserRolledOneOrSix(game, p, activePieces, GetPlayersInactivePieces(p.Pieces), roll);
                    Thread.Sleep(2000);
                    break;
                }
            }
        }

        public void MoveToStart(List<Piece> inactivePieces)
        {
            var piece = inactivePieces.Where(x => x.IsActive == false).ToList();
            piece.FirstOrDefault().IsActive = true;
            piece.FirstOrDefault().Position = 1;

            Console.WriteLine();
            Console.WriteLine($"Moved one piece to square one.");
        }

        public Piece PickActivePieceToMove(List<Piece> activePieces, int playerpick)
        {
            return activePieces[playerpick];
        }
    }
}
