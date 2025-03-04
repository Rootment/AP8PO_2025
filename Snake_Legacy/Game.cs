using System.Diagnostics;
using static System.Console;

namespace Snake;

/// <summary>
/// Represents game logic
/// </summary>
public class Game
{
    private readonly int _windowHeight;

    private readonly int _windowWidth;

    private readonly Random _random = new();

    private readonly int _speed;
    private int Score { get; set; }

    private Pixel _head;

    private Pixel _berry;

    private List<Pixel> _body;

    private readonly ConsoleInterface _consoleInterface;

    private Direction _currentMovement;

    private bool _gameOver;


    /// <summary>
    /// Setup game settings on instance creation
    /// </summary>
    /// <param name="windowWidth">Width of console interface</param>
    /// <param name="windowHeight">Height of console interface</param>
    /// <param name="score">Score from start</param>
    /// <param name="speed">Speed of snake</param>
    public Game(int windowWidth, int windowHeight, int score, int speed)
    {
        _windowHeight = windowWidth;
        _windowWidth = windowHeight;
        Score = score;
        _speed = speed;

        _consoleInterface = new ConsoleInterface();
    }

    /// <summary>
    /// Start game
    /// </summary>
    public void Start()
    {
        Initialize();

        while (!_gameOver)
        {
            RenderFrame();
            ProcessInput();
            UpdateState();
        }

        _consoleInterface.ShowGameOver(Score);
    }

    /// <summary>
    /// Initialize some game settings
    /// </summary>
    private void Initialize()
    {
        WindowHeight = _windowHeight;
        WindowWidth = _windowWidth;

        _head = new Pixel(WindowWidth / 2, WindowHeight / 2, ConsoleColor.Red);
        _berry = GenerateBerry();
        _body = new List<Pixel>();
        _currentMovement = Direction.Right;
        _gameOver = false;
    }

    /// <summary>
    /// Generate berry at random position
    /// </summary>
    /// <returns></returns>
    private Pixel GenerateBerry()
    {
        return new Pixel(
            _random.Next(1, WindowWidth - 2),
            _random.Next(1, WindowHeight - 2),
            ConsoleColor.Cyan
        );
    }

    /// <summary>
    /// Render new frame, draw pixel and border
    /// </summary>
    private void RenderFrame()
    {
        Clear();
        _consoleInterface.DrawBorder();
        _consoleInterface.DrawPixel(_head);
        _consoleInterface.DrawPixel(_berry);

        foreach (var pixel in _body)
        {
            _consoleInterface.DrawPixel(pixel);
        }
    }

    /// <summary>
    /// Move head of snake based on direction
    /// </summary>
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
    
    
    /// <summary>
    /// Proccess how fast is snake moving //TODO ?
    /// </summary>
    private void ProcessInput()
    {
        var stopwatch = Stopwatch.StartNew();

        while (stopwatch.ElapsedMilliseconds <= _speed)
        {
            _currentMovement = _consoleInterface.ReadMovement(_currentMovement);
        }
    }

    /// <summary>
    /// Update state, score
    /// </summary>
    private void UpdateState()
    {
        _gameOver |= IsCollision();

        if (_head.XPos == _berry.XPos && _head.YPos == _berry.YPos)
        {
            Score++;
            _berry = GenerateBerry();
        }

        _body.Add(new Pixel(_head.XPos, _head.YPos, ConsoleColor.Green));

        MoveHead();

        if (_body.Count > Score)
        {
            _body.RemoveAt(0);
        }
    }

    /// <summary>
    /// Check for collision
    /// </summary>
    /// <returns>Returns if collision is present or not</returns>
    private bool IsCollision()
    {
        if (_head.XPos == WindowWidth - 1 || _head.XPos == 0 || _head.YPos == WindowHeight - 1 || _head.YPos == 0)
        {
            return true;
        }

        // foreach (var pixel in _body)
        // {
        //     if (pixel.XPos == _head.XPos && pixel.YPos == _head.YPos)
        //     {
        //         return true;
        //     }
        // }
        
        return _body.Any(pixel => pixel.XPos == _head.XPos && pixel.YPos == _head.YPos);
    }
}