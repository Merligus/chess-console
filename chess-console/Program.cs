using System;
using board;
using chess;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            board.placePiece(new Rook(board, Color.Black), new Position(0, 0));
            board.placePiece(new Rook(board, Color.Black), new Position(1, 3));
            board.placePiece(new King(board, Color.Red), new Position(2, 4));

            Display.printBoard(board);
        }
    }
}
