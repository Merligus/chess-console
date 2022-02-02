using System;
using board;

namespace chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        private int round;
        private Color playerColor;
        public bool finished { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            round = 1;
            playerColor = Color.White;
            finished = false;

            placePieces();
        }

        public void execMove(Position origin, Position destiny)
        {
            Piece pAttack = board.deletePiece(origin);
            pAttack.move();
            Piece pCaptured = board.deletePiece(destiny);
            board.placePiece(pAttack, destiny);
        }

        private void placePieces()
        {
            board.placePiece(new Rook(board, Color.White), new ChessPosition('c', 1).toPosition());
            board.placePiece(new Rook(board, Color.White), new ChessPosition('c', 2).toPosition());
            board.placePiece(new Rook(board, Color.White), new ChessPosition('d', 2).toPosition());
            board.placePiece(new Rook(board, Color.White), new ChessPosition('e', 2).toPosition());
            board.placePiece(new Rook(board, Color.White), new ChessPosition('e', 1).toPosition());
            board.placePiece(new King(board, Color.White), new ChessPosition('d', 1).toPosition());

            //board.placePiece(new Rook(board, Color.Black), new ChessPosition('c', 7).toPosition());
            //board.placePiece(new Rook(board, Color.Black), new ChessPosition('c', 8).toPosition());
            //board.placePiece(new Rook(board, Color.Black), new ChessPosition('d', 7).toPosition());
            //board.placePiece(new Rook(board, Color.Black), new ChessPosition('e', 7).toPosition());
            //board.placePiece(new Rook(board, Color.Black), new ChessPosition('e', 8).toPosition());
            board.placePiece(new King(board, Color.Black), new ChessPosition('d', 6).toPosition());
        }
    }
}
