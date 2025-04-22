using System.Diagnostics;
using static System.Console;

namespace Snake
{
    class Program
    {
        static void Main()
        {
            const int windowWidth = 16;
            const int windowHeight = 32;
            const int score = 5;
            const int speed = 20;
            
            Game game = new (windowWidth,windowHeight,score,speed);
            
            game.Start();
        }

    }
}