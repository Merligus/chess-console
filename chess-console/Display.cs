using System;
using board;
using chess;

namespace chess_console
{
    class Display
    {
        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.rows; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.rows; j++)
                    printPiece(board.piece(i, j), false);
                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H");
        }

        public static void printBoard(Board board, bool [,] possiblePositions)
        {
            for (int i = 0; i < board.rows; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.rows; j++)
                    printPiece(board.piece(i, j), possiblePositions[i, j]);
                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H");
        }

        public static ChessPosition readChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }

        public static void printPiece(Piece piece, bool possible)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;

            if (possible)
                Console.BackgroundColor = ConsoleColor.DarkGray;

            if (piece == null)
                Console.Write("-");
            else
            {
                if (piece.color == Color.White)
                    Console.Write(piece);
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
            }

            Console.BackgroundColor = originalBackground;
            Console.Write(" ");
        }
    }
}
