using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LudoEngine
{
    public class Menu
    {
        public static string MenuOptions(List<string> input, string type)
        {
            Console.WriteLine();
            Console.WriteLine(type);
            var options = input;

            int selected = 0;

            Console.CursorVisible = false;

            ConsoleKey? key = null;

            //Until the user presses enter the loop will continue to run
            while (key != ConsoleKey.Enter)
            {

                //Keeps track of what position the cursor has 
                if (key != null)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = Console.CursorTop - options.Count;
                }

                //Change the color at the cursor position
                for (int i = 0; i < options.Count; i++)
                {
                    var option = options[i];
                    if (i == selected)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.WriteLine("- " + option);
                    Console.ResetColor();
                }

                //Moves the cursor up or down
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Count - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }
            }

            Console.CursorVisible = true;

            //Return the selected option as lowercase
            return options[selected].ToLower();
        }

        public static void MainMenu(string input)
        {

            switch (input)
            {
                case "start new game":
                    int numberOfPlayers = ChoosePlayerAmount();

                    var game = new GameEngine(numberOfPlayers, 4);
                    var gamestate = game.StartNewGame();
                    //här ska vi spara spelet
                    game.PlayGame(gamestate);
                    break;

                case "load game":
                    var loadedGame = new GameEngine();
                    var loadedGameState = loadedGame.LoadGame();
                    loadedGame.PlayGame(loadedGameState);
                    break;
                default:
                    break;
            }
        }

        public static int ChoosePlayerAmount()
        {
            var choosenAmount = MenuOptions(new List<string> { "2", "3", "4" }, "Number of players 2-4");
            return int.Parse(choosenAmount);
        }

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
            Thread.Sleep(2000);
        }

        //This whole method needs refactoring.
        public static int PickFromList(List<Piece> list)
        {
            int choice = -1;
            List<string> availablePieces = new List<string>();

            foreach (var item in list)
            {
                availablePieces.Add(item.ToString());
            }

            // Gets the pieces ToString as an anwser from the menu choice.
            string input = MenuOptions(availablePieces, "What piece do you want to move?");

            // Gets the index of the chosen piece
            foreach (var piece in list)
            {
                if (piece.ToString().ToLower() == input)
                {
                    choice = list.FindIndex(x => x == piece);
                }
            }
            return choice;
        }

        public static void PickInactivePieceToMove()
        {
            //skriv menu för inaktiva pjäser
        }

        public static bool WantToMoveTwoPiecesFromYard()
        {
            string input = MenuOptions(new List<string> { "Yes", "No" }, "Do you want to move 2 pieces to square 1 ?");

            switch (input)
            {
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return false;
            }
        }

        public static bool WantToMoveActivePiece()
        {
            Console.WriteLine("Do you want to move an active piece?");

            while (true)
            {
                var input = Console.ReadLine();
                switch (input)
                {
                    case "yes":
                        return true;
                    case "no":
                        return false;
                    default:
                        Console.WriteLine("Please enter yes or no");
                        break;
                }
            }
        }
    }
}
