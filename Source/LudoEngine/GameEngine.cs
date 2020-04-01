using System;
using System.Collections.Generic;
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
            while (true)
            {
                //game.MovePieces(game.NextPlayer
                //    , game.GetPieces(game.NextPlayer)
                //    , Dice.Roll());


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
