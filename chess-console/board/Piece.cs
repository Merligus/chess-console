namespace board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int movQtty { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)
        {
            this.position = null;
            this.board = board;
            this.color = color;
            this.movQtty = 0;
        }

        protected bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public void move()
        {
            movQtty++;
        }

        public void unmove()
        {
            movQtty--;
        }

        public bool anyPossibleMovement()
        {
            bool[,] possibles = possibleMovements();
            for (int i = 0; i < board.rows; i++)
                for (int j = 0; j < board.columns; j++)
                    if (possibles[i, j])
                        return true;
            return false;
        }

        public bool canMoveTo(Position pos)
        {
            bool[,] possibles = possibleMovements();
            return possibles[pos.row, pos.column];
        }

        public abstract bool[,] possibleMovements();
    }
}
