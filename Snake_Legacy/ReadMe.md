Hi

gpt navrhl:
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Console;

namespace Snake
{
    class Program
    {
        static void Main()
        {
            Game game = new Game();
            game.Start();
        }
    }

    class Game
    {
        private const int WindowHeight = 16;
        private const int WindowWidth = 32;

        private readonly Random _random = new Random();
        private int _score = 5;

        private Pixel _head;
        private Pixel _berry;
        private List<Pixel> _body;

        private Direction _currentMovement;
        private bool _gameOver;

        public void Start()
        {
            Initialize();

            while (!_gameOver)
            {
                RenderFrame();
                ProcessInput();
                UpdateState();
            }

            ShowGameOver();
        }

        private void Initialize()
        {
            WindowHeight = WindowHeight;
            WindowWidth = WindowWidth;

            _head = new Pixel(WindowWidth / 2, WindowHeight / 2, ConsoleColor.Red);
            _berry = GenerateBerry();
            _body = new List<Pixel>();
            _currentMovement = Direction.Right;
            _gameOver = false;
        }

        private Pixel GenerateBerry()
        {
            return new Pixel(
                _random.Next(1, WindowWidth - 2),
                _random.Next(1, WindowHeight - 2),
                ConsoleColor.Cyan
            );
        }

        private void RenderFrame()
        {
            Clear();
            DrawBorder();
            DrawPixel(_head);
            DrawPixel(_berry);

            foreach (var pixel in _body)
            {
                DrawPixel(pixel);
            }
        }

        private void ProcessInput()
        {
            var stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedMilliseconds <= 500)
            {
                _currentMovement = ReadMovement(_currentMovement);
            }
        }

        private void UpdateState()
        {
            _gameOver |= IsCollision();

            if (_head.XPos == _berry.XPos && _head.YPos == _berry.YPos)
            {
                _score++;
                _berry = GenerateBerry();
            }

            _body.Add(new Pixel(_head.XPos, _head.YPos, ConsoleColor.Green));

            MoveHead();

            if (_body.Count > _score)
            {
                _body.RemoveAt(0);
            }
        }

        private bool IsCollision()
        {
            if (_head.XPos == WindowWidth - 1 || _head.XPos == 0 || _head.YPos == WindowHeight - 1 || _head.YPos == 0)
            {
                return true;
            }

            foreach (var pixel in _body)
            {
                if (pixel.XPos == _head.XPos && pixel.YPos == _head.YPos)
                {
                    return true;
                }
            }

            return false;
        }

        private void MoveHead()
        {
            switch (_currentMovement)
            {
                case Direction.Up:
                    _head.YPos--;
                    break;
                case Direction.Down:
                    _head.YPos++;
                    break;
                case Direction.Left:
                    _head.XPos--;
                    break;
                case Direction.Right:
                    _head.XPos++;
                    break;
            }
        }

        private void ShowGameOver()
        {
            SetCursorPosition(WindowWidth / 5, WindowHeight / 2);
            WriteLine($"Game over, Score: {_score - 5}");
            SetCursorPosition(WindowWidth / 5, WindowHeight / 2 + 1);
            ReadKey();
        }

        private static Direction ReadMovement(Direction currentMovement)
        {
            if (!KeyAvailable) return currentMovement;

            var key = ReadKey(true).Key;
            return key switch
            {
                ConsoleKey.UpArrow when currentMovement != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentMovement != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when currentMovement != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentMovement != Direction.Left => Direction.Right,
                _ => currentMovement
            };
        }

        private static void DrawPixel(Pixel pixel)
        {
            SetCursorPosition(pixel.XPos, pixel.YPos);
            ForegroundColor = pixel.ScreenColor;
            Write("■");
            SetCursorPosition(0, 0);
        }

        private static void DrawBorder()
        {
            for (var i = 0; i < WindowWidth; i++)
            {
                SetCursorPosition(i, 0);
                Write("■");
                SetCursorPosition(i, WindowHeight - 1);
                Write("■");
            }

            for (var i = 0; i < WindowHeight; i++)
            {
                SetCursorPosition(0, i);
                Write("■");
                SetCursorPosition(WindowWidth - 1, i);
                Write("■");
            }
        }
    }

    struct Pixel
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public ConsoleColor ScreenColor { get; set; }

        public Pixel(int xPos, int yPos, ConsoleColor color)
        {
            XPos = xPos;
            YPos = yPos;
            ScreenColor = color;
        }
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
