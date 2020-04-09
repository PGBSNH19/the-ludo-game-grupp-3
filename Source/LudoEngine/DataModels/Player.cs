using System;
using System.Collections.Generic;

namespace LudoEngine
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool HasFinished { get; set; }
        public bool IsMyTurn { get; set; }
        public bool IsWinner { get; set; }
        public int GameStateID { get; set; }
        public GameState GameState { get; set; }
        public List<Piece> Pieces { get; set; }

        public Player(string name)
        {
            Name = name;
            HasFinished = false;
            IsMyTurn = false;
            IsWinner = false;
        }

        /// <summary>
        /// how relative position is calculated
        /// spelare 0 :
        ///     spelare1+10
        ///	    spelare2+20
        ///	    spelare3+30
        /// spelare 1 :
        /// 	spelare2+10
        ///	    spelare3+20
        ///	    spelare0+30
        /// spelare 2 :
        ///	    spelare3+10
        ///	    spelare0+20
        ///	    spelare1+30
        /// spelare 3 :
        ///	    spelare0+10
        ///	    spelare1+20
        ///	    spelare2+30

        /// </summary>
        /// <returns></returns>
        /// 
        public int GetRelativePositionOfOpponent(GameState game, Player p)
        {
            var currentPlayerIndex = game.Players.IndexOf(this);
            var oponentIndex = game.Players.IndexOf(p);

            if (currentPlayerIndex == 0)
            {
                if (oponentIndex == 1) { return 10; }

                if (oponentIndex == 2) { return 20; }

                return 30;

            }
            else if (currentPlayerIndex == 1)
            {
                if (oponentIndex == 2) { return 10; }

                if (oponentIndex == 3) { return 20; }

                return 30;

            }
            else if (currentPlayerIndex == 2)
            {
                if (oponentIndex == 3) { return 10; }

                if (oponentIndex == 0) { return 20; }

                return 30;
            }
            else
            {
                if (oponentIndex == 0) { return 10; }

                if (oponentIndex == 1) { return 20; }

                return 30;
            }
        }

        public override string ToString()
        {
            return $"{Name}: {ID}";
        }
    }
}
