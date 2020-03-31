using System;
using System.Collections.Generic;
using System.Text;

namespace LudoEngine
{
    public class GameState
    {
        public int ID { get; set; }
        public int NextPlayerID { get; set; }
        public Player NextPlayer { get; set; }
        public bool HasFinished { get; set; }
        List<Player> players;

        Dictionary<Player, Piece> PlayerPieces;

        public GameState()
        {
            players = new List<Player>();
        }


        /// <summary>
        ///Every GameState takes each player and it's pieces in list form. 
        ///Saves the players to a temp list.
        ///Both the players and each of it's pieces will then be saved in dictionary PlayerPieces.
        /// </summary>
        public GameState(Player playerOne, List<Piece> playerOnePieces
            , Player playerTwo, List<Piece> playerTwoPieces
            , Player playerThree = null , List<Piece> playerThreePieces = null
            , Player playerFour = null, List<Piece> playerFourPieces = null)
            :base()
        {
            
            PlayerPieces = new Dictionary<Player, Piece>();

            players.Add(playerOne);
            players.Add(playerTwo);
            players.Add(playerThree);
            players.Add(playerFour);


            foreach (var player in players)
            {
                if (player != null)
                {
                    PlayerPieces.Add(player, playerOnePieces[0]);
                    PlayerPieces.Add(player, playerOnePieces[1]);
                    PlayerPieces.Add(player, playerOnePieces[2]);
                    PlayerPieces.Add(player, playerOnePieces[3]);
                }
            }
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public override string ToString()
        {
            return $"Is the game {ID} finished: {HasFinished}.";
        }
    }
}
