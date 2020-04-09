using LudoEngine;
using System;
using System.Collections.Generic;

namespace LudoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu.MainMenu(Menu.MenuOptions(new List<string> { "Start new game", "Load game", "Save game" }, "Options"));
            Console.ReadLine();
        }
    }
}
