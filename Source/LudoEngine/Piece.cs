using System;
using System.Collections.Generic;
using System.Text;

namespace LudoEngine
{
    public class Piece
    {
        public int ID { get; set; }
        public bool IsActive { get; set; }
        public bool HasFinished { get; set; }
        public int Steps { get; set; }
        public int Position { get; set; }
        public string Color { get; set; }
        public enum colors
        {
            red ,
            blue,
            green,
            yellow
           
        }
        public Piece(string color)
        {
            IsActive = false;
            HasFinished = false;
            Steps = 0;
            Position = 0;
            Color = color;
        }

        public override string ToString()
        {
            return $"Piece: {ID} of {Color} has the position {Position}.";
        }
    }
}
