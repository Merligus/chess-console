using System;
using board;
using chess;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.finished)
                {
                    Console.Clear();
                    Display.printBoard(match.board);

                    Console.Write("\nOrigin: ");
                    Position origin = Display.readChessPosition().toPosition();

                    bool[,] possiblePositions = match.board.piece(origin).possibleMovements();

                    Console.Clear();
                    Display.printBoard(match.board, possiblePositions);

                    Console.Write("\nDestiny: ");
                    Position destiny = Display.readChessPosition().toPosition();

                    match.execMove(origin, destiny);
                }
                
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Game finished");
            }
        }
    }
}
