using System;
using System.Collections.Generic;
using board;
using chess;

namespace chess_console
{
    class Display
    {
        public static void printMatch(ChessMatch match, bool[,] possiblePositions)
        {
            printBoard(match.board, possiblePositions);
            printCapturedPieces(match);
            Console.WriteLine($"\nRound: {match.round}");
            Console.WriteLine($"Current player: {match.playerColor}");
        }

        public static void printCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("\nCaptured pieces:");
            Console.Write("White: ");
            printSet(match.capturedPieces(Color.White));

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Black: ");
            printSet(match.capturedPieces(Color.Black));
            Console.ForegroundColor = aux;
        }

        public static void printSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach (Piece x in set)
                Console.Write(x + " ");
            Console.WriteLine("]");
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
