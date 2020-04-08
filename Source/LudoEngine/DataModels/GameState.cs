using System;
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
        public List<Player> Players;

        public GameState()
        {
            Players = new List<Player>();
        }

        public void MovePiece(Player player, Piece piece, int steps)
        {
            var correctPiece = player.Pieces.Single(s => s == piece);
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

        public void SaveGame()
        {
            
        }

        public GameState LoadGame()
        {
            List<GameState> savedGames = GetSavedGames();
            if (savedGames == null)
            {
                throw new NullReferenceException("No saved games.");
            }

            List<string> availableGames = new List<string>();
            savedGames.ForEach(x => availableGames.Add(x.ToString()));

            GameState selectedGame = PickSavedGame(Menu.MenuOptions(availableGames, "Choose a saved game"), savedGames);

            return selectedGame;
        }
        public List<GameState> GetSavedGames()
        {
            throw new NotImplementedException();
        }

        public GameState PickSavedGame(string selectedGame, List<GameState> savedGames)
        {
            return savedGames.Where(x => x.ToString() == selectedGame).FirstOrDefault();
        }

        public override string ToString()
        {
            if (Players.Count == 2)
            {

                return $"Players in this game {ID}: {Players[0].Name}, {Players[1].Name}. ";
            }
            else if (Players.Count == 3)
            {
                return $"Players in this game {ID}: {Players[0].Name}, {Players[1].Name}, {Players[2].Name}. ";

            }
            else
            {
                return $"Players in this game {ID}: {Players[0].Name}, {Players[1].Name}, {Players[2].Name}, {Players[3].Name}. ";

            }
        }
    }
}
