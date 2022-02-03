using System.Collections.Generic;
using board;

namespace chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public int round;
        public Color playerColor;
        public bool bFinished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool bInCheck { get; private set; }
        public Piece enPassantVulnerable { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            round = 1;
            playerColor = Color.White;
            bFinished = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            bInCheck = false;
            enPassantVulnerable = null;

            placePieces();
        }

        public Piece execMove(Position origin, Position destiny)
        {
            Piece pAttack = board.deletePiece(origin);
            pAttack.move();
            Piece pCaptured = board.deletePiece(destiny);
            board.placePiece(pAttack, destiny);
            if (pCaptured != null)
                captured.Add(pCaptured);

            // castling
            if (pAttack is King && destiny.column == origin.column + 2)
            {
                Position originRook = new Position(origin.row, origin.column + 3);
                Position destinyRook = new Position(origin.row, origin.column + 1);
                Piece R = board.deletePiece(originRook);
                R.move();
                board.placePiece(R, destinyRook);
            }
            else if (pAttack is King && destiny.column == origin.column - 2)
            {
                Position originRook = new Position(origin.row, origin.column - 4);
                Position destinyRook = new Position(origin.row, origin.column - 1);
                Piece R = board.deletePiece(originRook);
                R.move();
                board.placePiece(R, destinyRook);
            }

            // en passant
            if (pAttack is Pawn)
            {
                if (origin.column != destiny.column && pCaptured == null)
                {
                    Position posP;
                    if (pAttack.color == Color.White)
                        posP = new Position(destiny.row + 1, destiny.column);
                    else
                        posP = new Position(destiny.row - 1, destiny.column);
                    pCaptured = board.deletePiece(posP);
                    captured.Add(pCaptured);
                }
            }

            return pCaptured;
        }

        public void undoMove(Position origin, Position destiny, Piece pCaptured)
        {
            Piece p = board.deletePiece(destiny);
            p.unmove();
            if (pCaptured != null)
            {
                board.placePiece(pCaptured, destiny);
                captured.Remove(pCaptured);
            }

            // castling
            if (p is King && destiny.column == origin.column + 2)
            {
                Position originRook = new Position(origin.row, origin.column + 3);
                Position destinyRook = new Position(origin.row, origin.column + 1);
                Piece R = board.deletePiece(destinyRook);
                R.unmove();
                board.placePiece(R, originRook);
            }
            else if (p is King && destiny.column == origin.column - 2)
            {
                Position originRook = new Position(origin.row, origin.column - 4);
                Position destinyRook = new Position(origin.row, origin.column - 1);
                Piece R = board.deletePiece(destinyRook);
                R.unmove();
                board.placePiece(R, originRook);
            }

            // en passant
            if (p is Pawn)
            {
                if (origin.column != destiny.column && pCaptured == enPassantVulnerable)
                {
                    Piece pawn = board.deletePiece(destiny);
                    Position posP;
                    if (p.color == Color.White)
                        posP = new Position(3, destiny.column);
                    else
                        posP = new Position(4, destiny.column);
                    board.placePiece(pawn, posP);
                }
            }

            board.placePiece(p, origin);
        }

        public void play(Position origin, Position destiny)
        {
            Piece pCaptured = execMove(origin, destiny);
            if (inCheck(playerColor))
            {
                undoMove(origin, destiny, pCaptured);
                throw new BoardException("Can not put own king in check");
            }

            if (inCheck(enemy(playerColor)))
                bInCheck = true;
            else
                bInCheck = false;

            if (verifyCheckmate(enemy(playerColor)))
                bFinished = true;
            else
            {
                round++;
                changePlayer();
            }

            Piece p = board.piece(destiny);
            // en passant
            if (p is Pawn && (destiny.row == origin.row - 2 || destiny.row == origin.row + 2))
                enPassantVulnerable = p;
            else
                enPassantVulnerable = null;
        }

        public void validateOrigin(Position pos)
        {
            board.validatePosition(pos);
            if (board.piece(pos) == null)
                throw new BoardException("No piece chosen");
            if (playerColor != board.piece(pos).color)
                throw new BoardException($"Not a {playerColor} piece");
            if (!board.piece(pos).anyPossibleMovement())
                throw new BoardException("Chosen piece has no possible movements");
        }

        public void validateDestiny(Position origin, Position destiny)
        {
            board.validatePosition(destiny);
            if (!board.piece(origin).possibleMovement(destiny))
                throw new BoardException("Invalid destiny position");
        }

        private void changePlayer()
        {
            if (playerColor == Color.White)
                playerColor = Color.Black;
            else
                playerColor = Color.White;
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
                if (x.color == color)
                    aux.Add(x);

            return aux;
        }

        public HashSet<Piece> inGamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
                if (x.color == color)
                    aux.Add(x);
            aux.ExceptWith(capturedPieces(color));

            return aux;
        }

        private Color enemy(Color color)
        {
            if (color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }

        private Piece king(Color color)
        {
            foreach(Piece x in inGamePieces(color))
                if (x is King)
                    return x;
            return null;
        }

        public bool inCheck(Color color)
        {
            Piece K = king(color);
            if (K == null)
                throw new BoardException($"No {color} king in the board");

            foreach (Piece x in inGamePieces(enemy(color)))
            {
                bool[,] possibleMovements = x.possibleMovements();
                if (possibleMovements[K.position.row, K.position.column])
                    return true;
            }
            return false;
        }

        public bool verifyCheckmate(Color color)
        {
            if (!inCheck(color))
                return false;

            foreach (Piece x in inGamePieces(color))
            {
                bool[,] possibleMovements = x.possibleMovements();
                for (int i = 0; i < board.rows; i++)
                    for (int j = 0; j < board.columns; j++)
                        if (possibleMovements[i, j])
                        {
                            Position origin = x.position;
                            Position destiny = new Position(i, j);
                            Piece pCaptured = execMove(origin, destiny);
                            bool verifyCheck = inCheck(color);
                            undoMove(origin, destiny, pCaptured);
                            if (!verifyCheck)
                                return false;
                        }
            }
            return true;
        }

        public void placeNewPiece(char column, int row, Piece piece)
        {
            board.placePiece(piece, new ChessPosition(column, row).toPosition());
            pieces.Add(piece);
        }

        private void placePieces()
        {
            placeNewPiece('a', 8, new Rook(board, Color.Black));
            placeNewPiece('b', 8, new Knight(board, Color.Black));
            placeNewPiece('c', 8, new Bishop(board, Color.Black));
            placeNewPiece('d', 8, new Queen(board, Color.Black));
            placeNewPiece('e', 8, new King(board, Color.Black, this));
            placeNewPiece('f', 8, new Bishop(board, Color.Black));
            placeNewPiece('g', 8, new Knight(board, Color.Black));
            placeNewPiece('h', 8, new Rook(board, Color.Black));
            placeNewPiece('a', 4, new Pawn(board, Color.Black, this));
            placeNewPiece('b', 7, new Pawn(board, Color.Black, this));
            placeNewPiece('c', 7, new Pawn(board, Color.Black, this));
            placeNewPiece('d', 7, new Pawn(board, Color.Black, this));
            placeNewPiece('e', 4, new Pawn(board, Color.Black, this));
            placeNewPiece('f', 7, new Pawn(board, Color.Black, this));
            placeNewPiece('g', 4, new Pawn(board, Color.Black, this));
            placeNewPiece('h', 7, new Pawn(board, Color.Black, this));

            placeNewPiece('a', 1, new Rook(board, Color.White));
            placeNewPiece('b', 1, new Knight(board, Color.White));
            placeNewPiece('c', 1, new Bishop(board, Color.White));
            placeNewPiece('d', 1, new Queen(board, Color.White));
            placeNewPiece('e', 1, new King(board, Color.White, this));            
            placeNewPiece('f', 1, new Bishop(board, Color.White));
            placeNewPiece('g', 1, new Knight(board, Color.White));
            placeNewPiece('h', 1, new Rook(board, Color.White));
            placeNewPiece('a', 2, new Pawn(board, Color.White, this));
            placeNewPiece('b', 2, new Pawn(board, Color.White, this));
            placeNewPiece('c', 2, new Pawn(board, Color.White, this));
            placeNewPiece('d', 2, new Pawn(board, Color.White, this));
            placeNewPiece('e', 2, new Pawn(board, Color.White, this));
            placeNewPiece('f', 2, new Pawn(board, Color.White, this));
            placeNewPiece('g', 2, new Pawn(board, Color.White, this));
            placeNewPiece('h', 2, new Pawn(board, Color.White, this));
        }
    }
}

// possible movements do king deveria analisar se esta em check
