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

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Display.readChessPosition().toPosition();
                    Console.Write("Destiny: ");
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
