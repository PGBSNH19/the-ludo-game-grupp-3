using LudoEngine;
using System;

namespace LudoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameEngine(2,1);
            var gamestate=game.StartNewGame();
            game.PlayGame(gamestate);
            Console.ReadLine();
        }
    }
}
