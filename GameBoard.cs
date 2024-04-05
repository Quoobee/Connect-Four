using System;

namespace ConnectFour;

public class GameBoard
{
    private readonly string PLAYER1 = "X";
    private readonly string PLAYER2 = "O";
    private readonly int ROWS;
    private readonly int COLS;
    private readonly int NINAROW;
    private string[,] Board
    {
        get; set;
    }

    public string NextPiece
    {
        get; set;
    }

    public GameBoard(int rows, int cols, int ninarow)
    {
        ROWS = rows;
        COLS = cols;
        NINAROW = ninarow;
        Board = new string[rows, cols];
        NextPiece = PLAYER1;
    }

    public void StartGame()
    {
        for (int r = 0; r < ROWS; r++)
        {
            for (int c = 0; c < COLS; c++)
            {
                Board[r, c] = "-";
            }
        }
    }

    public void PrintBoard()
    {
        for (int r = ROWS - 1; r > -1; r--)
        {
            for (int c = 0; c < COLS; c++)
            {
                Console.Write(Board[r, c] + " ");
            }
            Console.Write("\n");
        }
    }

    public void SetPiece(int r, int c, string piece)
    {
        Board[r, c] = piece;
    }

    public int NextRow(int c)
    {
        for (int r = 0; r < ROWS; r++)
        {
            if (Board[r, c] == "-")
            {
                return r;
            }
        }
        return -1;
    }

    public int Move(int c)
    {
        int r = NextRow(c);
        if (r == -1) 
        {
            Console.WriteLine("That column is full");
            return -1;
        }

        SetPiece(r, c, NextPiece);

        if (NextPiece == PLAYER1)
        {
            NextPiece = PLAYER2;
        }
        else 
        {   
            NextPiece = PLAYER1;
        }

        return r;
    }
    
    public bool VerticalWinner(int r, int c, string piece) 
    {
        int ninarow = 0;
        while (r >= 0)
        {
            if (Board[r, c] != piece) 
            {
                ninarow = 0;
                r--;
                continue;
            }

            ninarow++;
            if (ninarow == NINAROW)
            {
                return true;
            }
            r--;
        }
        return false;
    }

    public bool HorizontalWinner(int r, int c, string piece)
    {
        int ninarow = 0;
        for (int i = 0; i < ROWS; i++)
        {
            if (Board[r, i] != piece)
            {
                ninarow = 0;
                continue;
            }
            
            ninarow++;
            if (ninarow == NINAROW)
            {
                return true;
            }
        }
        return false;
    }

    public bool RightDiagonalWinner(int r, int c, string piece)
    {
        int ninarow = 0;
        while (r > 0 && c > 0) 
        {
            r--;
            c--;
        }
        while (r < ROWS && c < COLS)
        {
            if (Board[r, c] != piece)
            {
                ninarow = 0;
                r++;
                c++;
                continue;
            }
            
            ninarow++;
            if (ninarow == NINAROW)
            {
                return true;
            }
            r++;
            c++;
        }
        return false;
    }

    public bool LeftDiagonalWinner(int r, int c, string piece)
    {
        int ninarow = 0;
        while(r > 0 && c < COLS - 1)
        {
            r--;
            c++;
        }
        while (r < ROWS && c >= 0)
        {
            if (Board[r, c] != piece)
            {
                ninarow = 0;
                r++;
                c--;
                continue;
            }
            
            ninarow++;
            if (ninarow == NINAROW)
            {
                return true;
            }
            r++;
            c--;
        }
        return false;
    }

    public bool IsWinner(int r, int c)
    {
        string piece = Board[r, c];
        if (VerticalWinner(r, c, piece) || 
        HorizontalWinner(r, c, piece) || 
        RightDiagonalWinner(r, c, piece) || 
        LeftDiagonalWinner(r, c, piece))
        {
            Console.WriteLine(piece + " won!");
            return true;
        }
        return false;
    }
}