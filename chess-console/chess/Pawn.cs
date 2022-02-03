using board;

namespace chess
{
    class Pawn : Piece
    {

        public Pawn(Board board, Color color) : base(board, color)
        {
        }

        private bool enemyIn(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.color != color;
        }

        private bool free(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override bool[,] possibleMovements()
        {
            bool[,] possibles = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.assignPosition(position.row - 1, position.column);
                if (board.validPosition(pos) && free(pos))
                    possibles[pos.row, pos.column] = true;
                pos.assignPosition(position.row - 2, position.column);
                if (board.validPosition(pos) && free(pos) && movQtty == 0)
                    possibles[pos.row, pos.column] = true;
                pos.assignPosition(position.row - 1, position.column - 1);
                if (board.validPosition(pos) && enemyIn(pos))
                    possibles[pos.row, pos.column] = true;
                pos.assignPosition(position.row - 1, position.column + 1);
                if (board.validPosition(pos) && enemyIn(pos))
                    possibles[pos.row, pos.column] = true;
            }
            else
            {
                pos.assignPosition(position.row + 1, position.column);
                if (board.validPosition(pos) && free(pos))
                    possibles[pos.row, pos.column] = true;
                pos.assignPosition(position.row + 2, position.column);
                if (board.validPosition(pos) && free(pos) && movQtty == 0)
                    possibles[pos.row, pos.column] = true;
                pos.assignPosition(position.row + 1, position.column - 1);
                if (board.validPosition(pos) && enemyIn(pos))
                    possibles[pos.row, pos.column] = true;
                pos.assignPosition(position.row + 1, position.column + 1);
                if (board.validPosition(pos) && enemyIn(pos))
                    possibles[pos.row, pos.column] = true;
            }

            return possibles;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
