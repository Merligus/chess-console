using board;

namespace chess
{
    class Pawn : Piece
    {
        private ChessMatch match;

        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
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

                // en passant
                if (position.row == 3)
                {
                    Position left = new Position(position.row, position.column - 1);
                    if (board.validPosition(left) && enemyIn(left) && board.piece(left) == match.enPassantVulnerable)
                        possibles[left.row - 1, left.column] = true;
                    Position right= new Position(position.row, position.column + 1);
                    if (board.validPosition(right) && enemyIn(right) && board.piece(right) == match.enPassantVulnerable)
                        possibles[right.row - 1, right.column] = true;
                }
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

                // en passant
                if (position.row == 4)
                {
                    Position left = new Position(position.row, position.column - 1);
                    if (board.validPosition(left) && enemyIn(left) && board.piece(left) == match.enPassantVulnerable)
                        possibles[left.row + 1, left.column] = true;
                    Position right = new Position(position.row, position.column + 1);
                    if (board.validPosition(right) && enemyIn(right) && board.piece(right) == match.enPassantVulnerable)
                        possibles[right.row + 1, right.column] = true;
                }
            }

            return possibles;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
