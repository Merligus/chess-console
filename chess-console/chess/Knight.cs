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
            Position pos = new Position(0, 0);

            pos.assignPosition(position.row - 1, position.column - 2);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;

            pos.assignPosition(position.row - 2, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;

            pos.assignPosition(position.row - 2, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;

            pos.assignPosition(position.row - 1, position.column + 2);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;

            pos.assignPosition(position.row + 1, position.column + 2);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;

            pos.assignPosition(position.row + 2, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;

            pos.assignPosition(position.row + 1, position.column - 2);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;

            pos.assignPosition(position.row + 2, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                possibles[pos.row, pos.column] = true;

            return possibles;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
