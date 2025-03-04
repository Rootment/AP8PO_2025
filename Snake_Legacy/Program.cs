using System.Diagnostics;
using static System.Console;

namespace Snake
{
    class Program
    {
        static void Main()
        {
            ConsoleInterface consoleInterface = new ConsoleInterface();
            
            WindowHeight = 16;
            WindowWidth = 32;

            var rand = new Random();

            var score = 5;

            var head = new Pixel(WindowWidth / 2, WindowHeight / 2, ConsoleColor.Red);
            var berry = new Pixel(rand.Next(1, WindowWidth - 2), rand.Next(1, WindowHeight - 2), ConsoleColor.Cyan);

            var body = new List<Pixel>();

            var currentMovement = Direction.Right;

            var gameover = false;

            while (!gameover)
            {
                Clear();
                
                gameover |= (head.XPos == WindowWidth - 1 || head.XPos == 0 || head.YPos == WindowHeight - 1 ||
                             head.YPos == 0);

                consoleInterface.DrawBorder();

                if (berry.XPos == head.XPos && berry.YPos == head.YPos)
                {
                    score++;
                    berry = new Pixel(rand.Next(1, WindowWidth - 2), rand.Next(1, WindowHeight - 2), ConsoleColor.Cyan);
                }

                foreach (var pixel in body)
                {
                    consoleInterface.DrawPixel(pixel);
                    gameover |= (pixel.XPos == head.XPos && pixel.YPos == head.YPos);
                }

                consoleInterface.DrawPixel(head);
                consoleInterface.DrawPixel(berry);

                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds <= 500)
                {
                    currentMovement = consoleInterface.ReadMovement(currentMovement);
                }

                body.Add(new Pixel(head.XPos, head.YPos, ConsoleColor.Green));

                switch (currentMovement)
                {
                    case Direction.Up:
                        head.YPos--;
                        break;
                    case Direction.Down:
                        head.YPos++;
                        break;
                    case Direction.Left:
                        head.XPos--;
                        break;
                    case Direction.Right:
                        head.XPos++;
                        break;
                    default:
                        WriteLine("Use only up and down arrows or left arrows or right arrows.");
                        break;
                }

                if (body.Count > score)
                {
                    body.RemoveAt(0);
                }
            }

            consoleInterface.EndGame(score);
        }

    }
}