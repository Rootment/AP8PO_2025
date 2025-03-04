using System.Diagnostics;
using static System.Console;

namespace Snake
{
    class Program
    {
        static void Main()
        {
            Game game = new Game(32,16,5);
            
            game.Start();
        }

    }
}