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
                    bool reroll = false;

                    do
                    {
                        var activePieces = CurrentPlayerPieces.Where(x => x.IsActive == true).ToList();
                        var inactivePieces = CurrentPlayerPieces.Where(x => x.IsActive == false).ToList();

                        int roll = Dice.Roll();




                        // If the player has any active pieces on the board 
                        if((activePieces != null) 
                            && (roll == 1 || roll == 6) 
                            && inactivePieces.Count >= 1)
                        {
                            Menu.PickPieceToMove(activePieces, roll);
                        }
                        else if (activePieces != null)
                        {
                            Menu.PickActivePieceToMove(activePieces);
                        }
                        else if (roll == 1 || roll == 6)
                        {
                            Menu.PickInactivePieceToMove();
                        }
                        else
                        {
                            Console.WriteLine("You have no active pieces.");
                        }
                        
                     

                        if (roll == 6) { reroll = true; }

                    } while (reroll);
                    // Första killen ska rolla
                    // Spara value på roll
                    // rollar man 6 får man ett extra slag

                    // om man har en pjäs isActive && HasFinished == false
                    // Försök gå med en pjäs (eventuellt från lista)
                    // Är det fritt, flytta.


                    // Om man inte har en aktiv pjäs kolla efter 1 || 6
                    // välj flytta en 6 steg eller 2 till "pos 1"
                    // Försök gå med en pjäs (utav de som är inactive && HasFinished == false)
                    // Är det min egen pjäs eller motståndarpjäs, flytta.


                }



            }
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



    }
}
