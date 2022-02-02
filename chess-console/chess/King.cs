using board;

namespace chess
{
    class King : Piece
    {

        public King (Board board, Color color) : base(board, color)
        {
        }

        public override bool[,] possibleMovements()
        {
            bool [,] possibles = new bool[board.rows, board.columns];

            Position pos = new Position(0, 0);

            // north
            pos.assignPosition(position.row - 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;
            // ne
            pos.assignPosition(position.row - 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;
            // east
            pos.assignPosition(position.row, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;
            // se
            pos.assignPosition(position.row + 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;
            // south
            pos.assignPosition(position.row + 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;
            // sw
            pos.assignPosition(position.row + 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;
            // west
            pos.assignPosition(position.row, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;
            // nw
            pos.assignPosition(position.row - 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;

            return possibles;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
