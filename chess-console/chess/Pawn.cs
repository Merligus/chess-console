using board;

namespace chess
{
    class Pawn : Piece
    {

        public Pawn(Board board, Color color) : base(board, color)
        {
        }

        public override bool[,] possibleMovements()
        {
            bool[,] possibles = new bool[board.rows, board.columns];

            return possibles;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
