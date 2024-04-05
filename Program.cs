using System;

namespace ConnectFour;

public class Program
{
    public static void Main(String[] args)
    {
        Console.Write("Number of rows on the board? (standard is 6): ");
        int rows = Int32.Parse(Console.ReadLine());
        Console.Write("Number of columns on the board? (standard is 7): ");
        int cols = Int32.Parse(Console.ReadLine());
        Console.Write("Number of pieces in a row to win? (standard is 4): ");
        int ninarow = Int32.Parse(Console.ReadLine());
        GameBoard gameBoard = new GameBoard(rows, cols, ninarow);
        gameBoard.StartGame();

        gameBoard.PrintBoard();
        int r;
        int c;
        while (true)
        {
            for (int i = 0; i < cols; i++)
            {
                Console.Write(i + " ");
            }
            Console.Write("\n");
            Console.Write(gameBoard.NextPiece + "'s move, input column to drop piece: ");
            try
            {
                c = Int32.Parse(Console.ReadLine());
                if (c < 0 || c > cols-1)
                {
                    Console.WriteLine("Input is not a valid column");
                    continue;
                }
            }
            catch (System.FormatException)
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