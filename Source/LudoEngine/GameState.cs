using System;
using System.Collections.Generic;
using System.Linq;
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

        Dictionary<Player, List<Piece>> playerPieces;


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
        public void AddPieces(Player player,List<Piece> pieces)
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

            // A method should determine if the piece can move to the square it is intending.
            if (true)
            {
                Console.Write($"You moved {correctPiece.Steps} from square {correctPiece.Position} ");

                correctPiece.Position+= correctPiece.Steps;
                Console.Write($"and landed on square {correctPiece.Position}.\n");

            }
            correctPiece.Steps = 0;
        }

        public bool IsPieceClearForMoving(Player currentPlayer, Piece piece)
        {
            foreach (var p in playerPieces)
            {
                foreach (var item in p.Value)
                {
                    // Checks position of each piece in the game.
                    if (item.Position >= piece.Position + piece.Steps)
                    {
                        // If any of the sqaures in path are occupied.

                        if (currentPlayer == p.Key)
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
            return true;
        }



        public override string ToString()
        {
            return $"Is the game {ID} finished: {HasFinished}.";
        }
    }
}
