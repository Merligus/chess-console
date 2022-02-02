using System;
using board;
using chess;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessMatch match = new ChessMatch();

            while (!match.finished)
            {
                try
                {
                    Console.Clear();
                    Display.printBoard(match.board);
                    Console.WriteLine($"Round: {match.round}");
                    Console.WriteLine($"Current player: {match.playerColor}");

                    Console.Write("\nOrigin: ");
                    Position origin = Display.readChessPosition().toPosition();
                    match.validateOrigin(origin);

                    bool[,] possiblePositions = match.board.piece(origin).possibleMovements();

                    Console.Clear();
                    Display.printBoard(match.board, possiblePositions);

                    Console.Write("\nDestiny: ");
                    Position destiny = Display.readChessPosition().toPosition();

                    match.play(origin, destiny);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Game finished");
                    break;
                }
            }
        }
    }
}
