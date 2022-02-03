using board;

namespace chess
{
    class King : Piece
    {

        private ChessMatch match;

        public King (Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }

        private bool verifyRookCastling(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Rook && p.color == color && p.movQtty == 0;
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

            // castling
            if (movQtty == 0 && !match.bInCheck)
            {
                Position rookPos1 = new Position(position.row, position.column - 4);
                if (verifyRookCastling(rookPos1))
                {
                    Position p1 = new Position(position.row, position.column - 1);
                    Position p2 = new Position(position.row, position.column - 2);
                    Position p3 = new Position(position.row, position.column - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                        possibles[position.row, position.column - 2] = true;
                }

                Position rookPos2 = new Position(position.row, position.column + 3);
                if (verifyRookCastling(rookPos2))
                {
                    Position p1 = new Position(position.row, position.column + 1);
                    Position p2 = new Position(position.row, position.column + 2);
                    if (board.piece(p1) == null && board.piece(p2) == null)
                        possibles[position.row, position.column + 2] = true;
                }
            }

            return possibles;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
