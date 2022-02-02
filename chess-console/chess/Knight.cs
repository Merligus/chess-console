using board;

namespace chess
{
    class Knight : Piece
    {

        public Knight(Board board, Color color) : base(board, color)
        {
        }

        public override bool[,] possibleMovements()
        {
            bool[,] possibles = new bool[board.rows, board.columns];

            return possibles;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
