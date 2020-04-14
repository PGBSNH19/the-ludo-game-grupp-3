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
        public bool HasFinished { get; set; }
        public List<Player> Players { get; set; }
        public LudoGameContext context = new LudoGameContext();

        public GameState()
        {
            HasFinished = false;
            Players = new List<Player>();
        }

        public void MovePiece(Player player, Piece piece, int steps)
        {
            var correctPiece = player.Pieces.SingleOrDefault(s => s.ID == piece.ID);
            correctPiece.Steps = steps;

            Console.Write($"You moved {correctPiece.Steps} steps from square {correctPiece.Position} ");

            correctPiece.Position += correctPiece.Steps;

            if (correctPiece.Position == 46)
            {
                correctPiece.HasFinished = true;
                correctPiece.IsActive = false;
                Console.WriteLine($"and {correctPiece.ID} of {correctPiece.Color} has finished.");
            }
            else
            {
                Console.Write($"and landed on square {correctPiece.Position}.\n");
                correctPiece.IsActive = true;
                correctPiece.Steps = 0;
            }

            // Updates The piece position in the database.
            SavePieceMove(correctPiece);
            Thread.Sleep(2500);
        }

        public void SavePieceMove(Piece p)
        {
            var currentPiece = context.Piece.SingleOrDefault(x => x.ID == p.ID);

            currentPiece.Position = p.Position;
            currentPiece.Steps = p.Steps;
            currentPiece.HasFinished = p.HasFinished;
            currentPiece.IsActive = p.IsActive;

            context.SaveChanges();
        }

        public void SaveGame(GameState game)
        {
            context.GameState.Add(game);

            foreach (var player in game.Players)
            {
                context.Player.Add(player);
                foreach (var piece in player.Pieces)
                {
                    context.Piece.Add(piece);
                }
            }
            context.SaveChanges();
        }

        public Player ChangePlayerTurn(Player p)
        {
            // Get the previous players index,
            int currentPlayerIndex = Players.IndexOf(p);

            if (currentPlayerIndex == 0)
            {
                currentPlayerIndex = Players.Count;
            }

            //Chooses the previous player.
            var previousPlayer = Players[currentPlayerIndex - 1];

            // Sets the IsMyTurn to false for the PREVIOUSPLAYER both locally and in the database.
            previousPlayer.IsMyTurn = false;
            context.Player.SingleOrDefault(x => x.ID == previousPlayer.ID).IsMyTurn = false;

            // Sets the IsMyTurn to true for the CURRENT PLAYER both locally and in the database.
            p.IsMyTurn = true;
            context.Player.SingleOrDefault(x => x.ID == p.ID).IsMyTurn = true;
            context.SaveChanges();
            return p;
        }

        public GameState LoadGame()
        {
            var availableGames = new List<string>();
            List<GameState> savedGames = GetUnfinishedGames();

            if (savedGames == null)
            {
                throw new NullReferenceException("No saved games.");
            }

            savedGames.ForEach(x => x.Players = getPlayersFromDatabase(x));
            savedGames.ForEach(x => x.Players.ForEach(z => z.Pieces = getPiecesFromDatabase(z)));
            savedGames.ForEach(x => availableGames.Add(x.ToString()));

            GameState selectedGame = PickSavedGame(Menu.MenuOptions(availableGames, "Choose a saved game: "), savedGames);

            return selectedGame;
        }

        public List<Player> getPlayersFromDatabase(GameState game)
        {
            return context.Player.Where(x => x.GameStateID == game.ID).ToList();
        }
       
        public List<Piece> getPiecesFromDatabase(Player player)
        {
            var temp = context.Piece.Where(x => x.PlayerID == player.ID).ToList();
            return temp;
        }

        public List<GameState> GetUnfinishedGames()
        {
            return context.GameState.Where(x => x.HasFinished == false).ToList();
        }

        public List<GameState> GetFinishedGames()
        {
            return context.GameState.Where(x => x.HasFinished).ToList();
        }

        public GameState PickSavedGame(string selectedGame, List<GameState> savedGames)
        {
            return savedGames.SingleOrDefault(x => x.ToString().ToLower() == selectedGame);
        }

        public override string ToString()
        {
            if (Players.Count == 2)
            {
                return $"Players in this game {ID}: {Players[0].Name}, {Players[1].Name}.";
            }
            else if (Players.Count == 3)
            {
                return $"Players in this game {ID}: {Players[0].Name}, {Players[1].Name}, {Players[2].Name}.";
            }
            else if (Players.Count == 4)
            {
                return $"Players in this game {ID}: {Players[0].Name}, {Players[1].Name}, {Players[2].Name}, {Players[3].Name}.";
            }
            else
            {
                return $"There are {Players.Count()} players in this game.";
            }
        }
    }
}
