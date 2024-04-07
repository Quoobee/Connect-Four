using System;

namespace ConnectFour;

public class GameBoard
{
  //
  // Private constants
  //
  private const char _player1 = 'X';
  private const char _player2 = 'O';


  //
  // Private instance data
  //
  private readonly int _numRows;
  private readonly int _numCols;
  private readonly int _winningLineCount;
  private readonly char[,] _board;


  //
  // Public properties
  //
  public char NextPiece { get; private set; }


  //
  // Constructor
  //
  public GameBoard(int numRows, int numCols, int winningLineCount)
  {
    _numRows = numRows;
    _numCols = numCols;
    _winningLineCount = winningLineCount;
    _board = new char[_numRows, _numCols];
    NextPiece = _player1;
  }


  //
  // Public methods
  //

  public void StartGame()
  {
    for (var r = 0; r < _numRows; ++r)
      for (var c = 0; c < _numCols; ++c)
        _board[r, c] = '-';
  }

  public void PrintBoard()
  {
    for (var r = _numRows - 1; r >= 0; --r)
    {
      for (var c = 0; c < _numCols; ++c)
        Console.Write(_board[r, c] + " ");
      Console.Write("\n");
    }
  }

  public int Move(int c)
  {
    int r = NextRow(c);
    if (r == -1)
    {
      Console.WriteLine("That column is full");
      return -1;
    }

    _board[r, c] = NextPiece;

    if (NextPiece == _player1)
      NextPiece = _player2;
    else
      NextPiece = _player1;

    return r;
  }

  public bool IsWinner(int r, int c)
  {
    var piece = _board[r, c];
    foreach (Direction direction in Enum.GetValues(typeof(Direction)))
    {
      if (IsWinnerInDirection(r, c, piece, direction))
      {
        Console.WriteLine(piece + " won!");
        return true;
      }
    }
    return false;
  }


  //
  // Private types
  //
  private enum Direction { Vertical, Horizontal, LeftDiagonal, RightDiagonal }


  //
  // Private methods
  //

  private (int, int) StartRowCol(int r, int c, Direction direction)
  {
    switch (direction)
    {
      case Direction.Vertical:
        return (0, c);
      case Direction.Horizontal:
        return (r, 0);
      case Direction.LeftDiagonal:
        while (r > 0 && c > 0)
        {
          --r;
          --c;
        }
        return (r, c);
      case Direction.RightDiagonal:
        while (r < _numRows - 1 && c > 0)
        {
          ++r;
          --c;
        }
        return (r, c);
    }
    throw new Exception("This should not happen");
  }

  private static (int, int) NextRowCol(int r, int c, Direction direction)
  {
    switch (direction)
    {
      case Direction.Vertical:
        return (++r, c);
      case Direction.Horizontal:
        return (r, ++c);
      case Direction.LeftDiagonal:
        return (++r, ++c);
      case Direction.RightDiagonal:
        return (--r, ++c);
    }
    throw new Exception("This should not happen");
  }

  private bool IsWinnerInDirection(int r, int c, char piece, Direction direction)
  {
    int lineCount = 0;
    (r, c) = StartRowCol(r, c, direction);
    while (r >= 0 && r < _numRows && c >= 0 && c < _numCols)
    {
      if (_board[r, c] == piece)
        ++lineCount;
      else
        lineCount = 0;
      if (lineCount == _winningLineCount)
        return true;
      (r, c) = NextRowCol(r, c, direction);
    }
    return false;
  }

  private int NextRow(int c)
  {
    for (var r = 0; r < _numRows; ++r)
    {
      if (_board[r, c] == '-')
      {
        return r;
      }
    }
    return -1;
  }
}