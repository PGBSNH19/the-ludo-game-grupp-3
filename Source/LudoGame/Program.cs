using LudoEngine;
using System;
using System.Collections.Generic;

namespace LudoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu.MenuHeader();
            Menu.MainMenu(Menu.MenuOptions(new List<string> { "Start New Game", "Load Unfinished Games", "Show Finished Games" }, "Options"));
            Console.ReadLine();
        }
    }
}
