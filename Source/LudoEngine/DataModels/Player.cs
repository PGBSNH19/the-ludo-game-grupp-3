using System;
using System.Collections.Generic;

namespace LudoEngine
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool HasFinished { get; set; }
        public List<Piece> Pieces { get; set; }

        List<GameState> PlayedGames;

        public Player(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}: {ID}";
        }
    }
}
