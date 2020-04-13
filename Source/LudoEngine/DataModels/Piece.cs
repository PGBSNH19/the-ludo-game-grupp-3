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
        public int PlayerID { get; set; }
        public Player player { get; set; }

        public Piece(Colors color)
        {
            IsActive = false;
            HasFinished = false;
            Steps = 0;
            Position = 0;
            Color = color;
        }

        public static Colors PickColor(string input)
        {
            Console.WriteLine();
            var colors = Enum.GetValues(typeof(Colors));

            // Här ska det göras ett call till databasen som kollar så att färgen som försöker väljas är ledig.

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
                    // This can never happen because of the menu selection process.
                    return Colors.blue;
            }
        }
        
        public override string ToString()
        {
            return $"Piece: {ID} of {Color} has the position {Position}.";
        }
    }
}
