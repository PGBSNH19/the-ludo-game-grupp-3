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
        public Colors Color { get; set; }
        
        public Piece(Colors color)
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

        public static Colors PickColor(string input)
        {
            Console.WriteLine();
            var colors = Enum.GetValues(typeof(Colors));

            // Här ska det göras ett call till databasen som kollar så att färgen som försöker väljas är ledig.

            while (true)
            {
                switch (input)
                {
                    case "red":
                        return Colors.red;
                    case "green":
                        return Colors.green;
                    case "yellow":
                        return Colors.yellow;
                    case "blue":
                        return Colors.blue;
                    default:
                        Console.WriteLine("Please choose a valid color");
                        break;
                }
            }
        }
    }
}
