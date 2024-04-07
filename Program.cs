using System;

namespace ConnectFour;

public class Program
{
  public static void Main(String[] args)
  {
    Console.Write("Number of rows on the board? (standard is 6): ");
    var numRows = Int32.Parse(Console.ReadLine());

    Console.Write("Number of columns on the board? (standard is 7): ");
    var numCols = Int32.Parse(Console.ReadLine());

    Console.Write("Number of pieces in a row to win? (standard is 4): ");
    var winningLineCount = Int32.Parse(Console.ReadLine());

    var gameBoard = new GameBoard(numRows, numCols, winningLineCount);
    gameBoard.StartGame();
    gameBoard.PrintBoard();

    int r;
    int c;
    while (true)
    {
      for (int i = 0; i < numCols; i++)
        Console.Write(i + " ");
      Console.Write("\n");

      Console.Write(gameBoard.NextPiece + "'s move, input column to drop piece: ");
      try
      {
        c = Int32.Parse(Console.ReadLine());
        if (c < 0 || c > numCols - 1)
        {
          Console.WriteLine("Input is not a valid column");
          continue;
        }
      }
      catch (FormatException)
      {
        Console.WriteLine("Input is not a number");
        continue;
      }
      r = gameBoard.Move(c);
      gameBoard.PrintBoard();
      if (r != -1 && gameBoard.IsWinner(r, c))
      {
        break;
      }
    }
  }
}