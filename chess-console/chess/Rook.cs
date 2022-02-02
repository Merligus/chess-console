using board;

namespace chess
{
    class Rook : Piece
    {

        public Rook(Board board, Color color) : base(board, color)
        {
        }

        public override bool[,] possibleMovements()
        {
            bool[,] possibles = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            // north
            pos.assignPosition(position.row - 1, position.column);
            while (board.validPosition(pos) && canMove(pos))
            {
                possibles[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.row = pos.row - 1;
            }
            // east
            pos.assignPosition(position.row, position.column + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                possibles[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.column = pos.column + 1;
            }
            // south
            pos.assignPosition(position.row + 1, position.column);
            while (board.validPosition(pos) && canMove(pos))
            {
                possibles[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.row = pos.row + 1;
            }
            // west
            pos.assignPosition(position.row, position.column - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                possibles[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.column = pos.column - 1;
            }

            return possibles;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
