using board;

namespace chess
{
    class Queen : Piece
    {

        public Queen(Board board, Color color) : base(board, color)
        {
        }

        public override bool[,] possibleMovements()
        {
            bool[,] possibles = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            // BiSHOP
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

            // ROOK
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
            return "Q";
        }
    }
}
