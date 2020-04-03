using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LudoEngine
{
    public class Menu
    {

        public static void PrintNoActivePieces()
        {
            Console.WriteLine();
            Console.WriteLine("You need to roll 1 or 6 to move pieces from yard.");
            Console.WriteLine();
            Thread.Sleep(2000);
        }

        public static void PromtUserToRollDice()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to roll the dice.");
            Console.WriteLine();
        }

        public static void PrintPlayerName(Player player)
        {
            Console.WriteLine();
            Console.WriteLine($"It's {player.Name}'s turn.");
            Console.WriteLine();
        }
        public static void PrintDiceRoll(Player player, int roll)
        {
            Console.WriteLine($"{player.Name} rolled {roll}.");
        }


        public static int PickFromList(List<Piece> list)
        {
            while (true)
            {

                Console.WriteLine("What piece do you want to move?");
                int count = 0;
                foreach (var item in list)
                {
                    count++;
                    Console.WriteLine($"{count}.{item}");
                }

                try
                {
                    var playerpick = int.Parse(Console.ReadLine());

                    if (playerpick > list.Count() || playerpick < 1)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    else
                    {
                        return playerpick;

                    }
                }
                catch (Exception)
                {
                    Console.WriteLine();
                    Console.WriteLine("Pick a valid piece.");
                    Console.WriteLine();
                    Thread.Sleep(2000);
                }
            }
        }



        public static void PickInactivePieceToMove()
        {
            //skriv menu för inaktiva pjäser
        }



        public static bool WantToMoveTwoPiecesFromYard()
        {
            //fråga vill du flytta 2 gubbar till 1  från bo ja/nej
            Console.WriteLine("Do you want to move 2 pieces to square 1?");

            return bool.Parse(Console.ReadLine());
        }

        public static bool WantToMoveActivePiece()
        {
            Console.WriteLine("Do you want to move an active piece?");

            return bool.Parse(Console.ReadLine());
        }
    }
}
