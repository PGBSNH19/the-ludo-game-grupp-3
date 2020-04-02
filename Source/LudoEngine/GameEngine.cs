using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void TryToMoveActivePiece(GameState game, Player p,List<Piece> activePieces, int roll)
        {
            while (true)
            {
                var activePiece = Menu.PickActivePieceToMove(activePieces);
                if (IsPieceClearForMoving(p, activePiece, game))
                {
                    game.MovePiece(p, activePiece, roll);
                    break;
                }
                else
                {
                    Menu.InvalidPieceMove();
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


                    do
                    {
                        var activePieces = CurrentPlayerPieces.Where(x => x.IsActive == true).ToList();
                        var inactivePieces = CurrentPlayerPieces.Where(x => x.IsActive == false).ToList();
                        int roll = Dice.Roll();

                        if (roll == 1 || roll == 6)
                        {
                            if (inactivePieces != null)
                            {
                                if (Menu.WantToMoveActivePiece())
                                {
                                    TryToMoveActivePiece(game,p,activePieces,roll);
                                }
                                else
                                {
                                    if (roll == 6)
                                    {
                                        if (inactivePieces.Count() > 1)
                                        {
                                            if (Menu.WantToMoveTwoPiecesFromYard())
                                            {
                                                MoveToStart(inactivePieces);
                                                MoveToStart(inactivePieces);
                                            }
                                            else
                                            {
                                                CheckIfPieceCanMoveFromYard(game, p, activePieces, inactivePieces, roll);
                                            }
                                        }
                                        else
                                        {
                                            CheckIfPieceCanMoveFromYard(game, p, activePieces, inactivePieces, roll);
                                        }
                                    }
                                }
                            }
                        }

                        rolledSix = (roll == 6) ? true : false;
                    } while (rolledSix);
                }



            }
        }

        private void CheckIfPieceCanMoveFromYard(GameState game, Player p, List<Piece> activePieces, List<Piece> inactivePieces, int roll)
        {
            if (IsPieceClearForMoving(p, inactivePieces[0], game))
            {
                game.MovePiece(p, inactivePieces[0], roll);
            }
            else
            {
                TryToMoveActivePiece(game, p, activePieces, roll);
            }
        }

        public static void PickPieceToMove(List<Piece> activePieces, int roll, List<Piece> inactivePieces, GameState game)
        {

            if (Menu.WantToMoveActivePiece())
            {
                if (roll == 6)
                {
                    // Gör val av PickInactivePieceToMove eller PickActivePieceToMove
                    if (inactivePieces.Count > 1 && Menu.WantToMoveTwoPiecesFromYard())
                    {
                    }
                    else
                    {

                    }

                }
                else if (roll == 1)
                {
                    // Gör val av PickInactivePieceToMove eller PickActivePieceToMove

                }


            }
            else
            {

            }


            //skriv menu för inaktiva pjäser
        }
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

    }
}
