using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LudoEngine
{
    public class Menu
    {

        public static void InvalidPieceMove()
        {
            Console.WriteLine("Sorry You can't move that piece.");
            Thread.Sleep(2000);
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
            return true;
        }

        public static bool WantToMoveActivePiece()
        {
            //vill du gå ut med gubbar eller vill du gå med en Active gubbe
            return true;
        }
    }
}
