using board;

namespace chess
{
    class Bishop : Piece
    {

        public Bishop(Board board, Color color) : base(board, color)
        {
        }

        public override bool[,] possibleMovements()
        {
            bool[,] possibles = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            // nw
            pos.assignPosition(position.row - 1, position.column - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                possibles[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.assignPosition(pos.row - 1, pos.column - 1);
            }
            // ne
            pos.assignPosition(position.row - 1, position.column + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                possibles[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.assignPosition(pos.row - 1, pos.column + 1);
            }
            // se
            pos.assignPosition(position.row + 1, position.column + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                possibles[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.assignPosition(pos.row + 1, pos.column + 1);
            }
            // sw
            pos.assignPosition(position.row + 1, position.column - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                possibles[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;
                pos.assignPosition(pos.row + 1, pos.column - 1);
            }

            return possibles;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
