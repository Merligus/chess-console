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
            bool[,] possiblePositions;

            while (!match.bFinished)
            {
                try
                {
                    // origin
                    Console.Clear();
                    possiblePositions = new bool[match.board.rows, match.board.columns];
                    Display.printMatch(match, possiblePositions);
                    
                    Console.Write("\nOrigin: ");
                    Position origin = Display.readChessPosition().toPosition();
                    match.validateOrigin(origin);

                    // destiny
                    Console.Clear();
                    possiblePositions = match.board.piece(origin).possibleMovements();
                    Display.printMatch(match, possiblePositions);

                    Console.Write("\nDestiny: ");
                    Position destiny = Display.readChessPosition().toPosition();
                    match.validateDestiny(origin, destiny);

                    // play
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

            Console.Clear();
            possiblePositions = new bool[match.board.rows, match.board.columns];
            Display.printMatch(match, possiblePositions);
        }
    }
}
