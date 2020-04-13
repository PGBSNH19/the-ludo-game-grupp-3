using System;
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


        public GameEngine()
        {

        }

        public GameEngine(int numberOfPlayers, int piecesPerPlayer)
        {
            NumberOfPlayers = numberOfPlayers;
            PiecesPerPlayer = piecesPerPlayer;
        }
        public string GetName()
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

            game.SaveGame(game);
            return game;
        }

        public void PlayGame(GameState game)
        {
            // break loop when last person has finished
            while (true)
            {
                // One round per person.
                foreach (var p in game.Players)
                {
                    // Sets the turn to the active player.
                    game.ChangePlayerTurn(p);
                    bool rolledSix = false;

                    // This will run if gets to roll again (rolled 6)
                    do
                    {
                        var CurrentPlayerPieces = game.getPiecesFromDatabase(p);
                        var activePieces = GetPlayersActivePieces(CurrentPlayerPieces);
                        var inactivePieces = GetPlayersInactivePieces(CurrentPlayerPieces);

                        Console.Clear();
                        //här ska vi skriva ut current game state så man vet var pjäserna står.
                        Menu.PrintPlayerName(p);

                        // Promts user to roll the dice and prints the roll in the console.
                        Menu.PromtUserToRollDice();
                        Console.ReadKey();
                        int roll = Dice.Roll();
                        //roll = 6;
                        Menu.PrintDiceRoll(p, roll);

                        if (roll == 1 || roll == 6)
                        {
                            UserRolledOneOrSix(game, p, activePieces, inactivePieces, roll);
                        }
                        if (roll > 1 && roll < 6)
                        {
                            UserRolledTwoToFive(game, p, activePieces, inactivePieces, roll);
                        }

                        // If someone wins run Main menu.
                        rolledSix = (roll == 6) ? true : false;
                        if (CheckForWinner(p)) Menu.MainMenu(Menu.MenuOptions(new List<string> { "Start new game", "Load game", "Save game" }, "Options")); ;

                    } while (rolledSix);
                }
            }
        }

        public bool CheckForWinner(Player p)
        {
            var temp = p.Pieces.Where(x => x.HasFinished).Count();
            if (temp == 4)
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

        public List<Piece> GetPlayersActivePieces(List<Piece> CurrentPlayerPieces)
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

        public List<Piece> GetPlayersInactivePieces(List<Piece> CurrentPlayerPieces)
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

        public void UserRolledOneOrSix(GameState game, Player p, List<Piece> activePieces, List<Piece> inactivePieces, int roll)
        {
            var inactivePieceCount = inactivePieces.Count();
            var activePieceCount = activePieces.Count();
            bool moveActive = false;
            bool moveTwoPiecesFromYard = false;


            if (activePieceCount > 0)
            {
                moveActive = Menu.WantToMoveActivePiece(Menu.MenuOptions(new List<string> { "yes", "no" }, "Do you want to move an active piece?"));
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
                && inactivePieces.Count != 0
                && moveActive)
            {
                TryToMoveActivePiece(game, p, activePieces, roll);
            }

            // If the user has chosen to move an INACTIVE piece to square 1, will always move.
            if (roll == 1 && inactivePieces.Count() > 0
                && !moveActive)
            {
                MoveToStart(inactivePieces);
                Thread.Sleep(2000);
            }

            // If the user has chosen to move an INACTIVE piece to sqaure 6 and the path is blocked by own piece.
            // Tries to move an active piece instead.
            if (roll == 6
                && inactivePieces.Count() == 1
                && !moveActive
                && IsPieceClearForMoving(p, inactivePieces[0], game, roll))
            {
                game.MovePiece(p, inactivePieces[0], roll);
                inactivePieces.RemoveAt(0);

            }

            // If the user has chosen to move an INACTIVE piece to sqaure 6 and player only has 1 inactive piece.
            if (roll == 6
                && inactivePieces.Count() == 1
                && !moveActive
                && !IsPieceClearForMoving(p, inactivePieces[0], game, roll))
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
                Thread.Sleep(2000);
                MoveToStart(inactivePieces);
                Thread.Sleep(2000);
            }

            // If the user has chosen to move an INACTIVE piece to sqaure 6 if possible. 
            if (roll == 6 && inactivePieces.Count() > 0
                && !moveActive
                && !moveTwoPiecesFromYard
                && IsPieceClearForMoving(p, inactivePieces[0], game, roll))
            {
                game.MovePiece(p, inactivePieces[0], roll);
                //inactivePieces.RemoveAt(0);
            }

            else if (roll == 6 && inactivePieces.Count() > 0
                   && !moveActive
                   && !moveTwoPiecesFromYard
                   && !IsPieceClearForMoving(p, inactivePieces[0], game, roll))
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

        public bool IsPieceClearForMoving(Player currentPlayer, Piece piece, GameState game, int roll)
        {
            foreach (var p in game.Players)
            {
                int relativePosition = currentPlayer.GetRelativePositionOfOpponent(game, p);

                foreach (var item in p.Pieces)
                {
                    // Checks position of each piece in front of the pice we want to move.
                    // Checks if the position of a piece found is in the way or in the same square we want to move to. 

                    if (piece.Position == 0 && piece.Position + roll == 1)
                    {
                        // If a player tries to move from 0 -> 1 and there is a piece there.
                        return true;
                    }

                    // If any of the sqaures in path are occupied.
                    if ((item.Position > piece.Position && item.Position <= (piece.Position + roll) && item.HasFinished == false))
                    {
                        // If it's a piece owned by current player on the same square.
                        if (currentPlayer == p)
                        {
                            return false;
                        }
                        else if ((item.Position + relativePosition == piece.Position + roll) && piece.Position < 41)
                        {
                            // Push opponent piece back to start.
                            item.IsActive = false;
                            item.Position = 0;
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
            while (true)
            {
                var activePiece = PickActivePieceToMove(activePieces, Menu.PickPieceFromList(activePieces));

                if (IsPieceClearForMoving(p, activePiece, game, roll))
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
        
        public void MoveToStart(List<Piece> inactivePieces)
        {
            var piece = inactivePieces.Where(x => x.IsActive == false).ToList();
            piece.FirstOrDefault().IsActive = true;
            piece.FirstOrDefault().Position = 1;
            Console.WriteLine($"Moved one piece to square one.");
        }
        
        public Piece PickActivePieceToMove(List<Piece> activePieces, int playerpick)
        {
            return activePieces[playerpick];
        }
    }
}
