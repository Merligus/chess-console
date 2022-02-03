using System.Collections.Generic;
using board;

namespace chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public int round;
        public Color playerColor;
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;

        public ChessMatch()
        {
            board = new Board(8, 8);
            round = 1;
            playerColor = Color.White;
            finished = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();

            placePieces();
        }

        public void execMove(Position origin, Position destiny)
        {
            Piece pAttack = board.deletePiece(origin);
            pAttack.move();
            Piece pCaptured = board.deletePiece(destiny);
            board.placePiece(pAttack, destiny);
            if (pCaptured != null)
                captured.Add(pCaptured);
        }

        public void play(Position origin, Position destiny)
        {
            execMove(origin, destiny);
            round++;
            changePlayer();
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
            if (!board.piece(origin).canMoveTo(destiny))
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

        public void placeNewPiece(char column, int row, Piece piece)
        {
            board.placePiece(piece, new ChessPosition(column, row).toPosition());
            pieces.Add(piece);
        }

        private void placePieces()
        {
            placeNewPiece('c', 1, new Rook(board, Color.White));
            placeNewPiece('c', 2, new Rook(board, Color.White));
            placeNewPiece('d', 2, new Rook(board, Color.White));
            placeNewPiece('e', 2, new Rook(board, Color.White));
            placeNewPiece('e', 1, new Rook(board, Color.White));
            placeNewPiece('d', 1, new King(board, Color.White));

            //board.placePiece(new Rook(board, Color.Black), new ChessPosition('c', 7).toPosition());
            //board.placePiece(new Rook(board, Color.Black), new ChessPosition('c', 8).toPosition());
            //board.placePiece(new Rook(board, Color.Black), new ChessPosition('d', 7).toPosition());
            //board.placePiece(new Rook(board, Color.Black), new ChessPosition('e', 7).toPosition());
            //board.placePiece(new Rook(board, Color.Black), new ChessPosition('e', 8).toPosition());
            placeNewPiece('d', 6, new King(board, Color.Black));
        }
    }
}
