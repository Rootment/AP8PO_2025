﻿using System.Diagnostics;
using static System.Console;

namespace Snake
{
    class Program
    {
        static void Main()
        {
            Game game = new Game(16,32,5,50);
            
            game.Start();
        }

    }
}