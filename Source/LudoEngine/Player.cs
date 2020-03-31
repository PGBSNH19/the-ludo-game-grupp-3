using System;
using System.Collections.Generic;

namespace LudoEngine
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool HasFinished { get; set; }

        List<GameState> PlayedGames;

        public override string ToString()
        {
            return $"{Name}: {ID}";
        }
    }
}
