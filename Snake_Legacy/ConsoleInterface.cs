﻿namespace Snake;
using static System.Console;

/// <summary>
/// Console interface for snake
/// </summary>
public class ConsoleInterface
{
    
    /// <summary>
    /// Read movement based on input
    /// </summary>
    /// <param name="movement">Key input</param>
    /// <returns>Returns Direction</returns>
    public Direction ReadMovement(Direction movement)
    {
        if (!KeyAvailable) return movement;
            
        var key = ReadKey(true).Key;

        movement = key switch
        {
            ConsoleKey.UpArrow when movement != Direction.Down => Direction.Up,
            ConsoleKey.DownArrow when movement != Direction.Up => Direction.Down,
            ConsoleKey.LeftArrow when movement != Direction.Right => Direction.Left,
            ConsoleKey.RightArrow when movement != Direction.Left => Direction.Right,
            _ => movement
        };

        return movement;
    }

    /// <summary>
    /// Draw pixel in console interface
    /// </summary>
    /// <param name="pixel">One pixel to be drawn</param>
    public void DrawPixel(Pixel pixel)
    {
        SetCursorPosition(pixel.XPos, pixel.YPos);
        ForegroundColor = pixel.ScreenColor;
        Write("■");
        SetCursorPosition(0, 0);
    }

    /// <summary>
    /// Draw borders for console interface
    /// </summary>
    public void DrawBorder()
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

    /// <summary>
    /// Show game score after gameover
    /// </summary>
    /// <param name="score">Reached score</param>
    public void ShowGameOver(int score)
    {
        SetCursorPosition(WindowWidth / 5, WindowHeight / 2);
        WriteLine($"Game over, Score: {score - 5}");
        SetCursorPosition(WindowWidth / 5, WindowHeight / 2 + 1);
        ReadKey();
    }
}