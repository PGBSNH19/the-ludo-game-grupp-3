﻿using System;
using System.Collections.Generic;

namespace LudoEngine
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool HasFinished { get; set; }
        public bool IsMyTurn { get; set; }
        public int GameStateID { get; set; }
        public GameState GameState { get; set; }
        public List<Piece> Pieces { get; set; }

        public Player(string name)
        {
            Name = name;
            HasFinished = false;
            IsMyTurn = false;
        }

        public override string ToString()
        {
            return $"{Name}: {ID}";
        }
    }
}
