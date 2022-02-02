namespace board
{
    class Board
    {
        public int rows { get; set; }
        public int columns { get; set; }
        private Piece[,] _pieces;

        public Board(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            _pieces = new Piece[rows, columns];
        }

        public Piece piece(int rowIndex, int columnIndex)
        {
            return _pieces[rowIndex, columnIndex];
        }

        public Piece piece(Position pos)
        {
            return _pieces[pos.row, pos.column];
        }

        public bool isPositionFilled(Position pos)
        {
            validatePosition(pos);
            return piece(pos) != null;
        }

        public void placePiece(Piece p, Position position)
        {
            if (isPositionFilled(position))
                throw new BoardException("Position already filled");
            _pieces[position.row, position.column] = p;
            p.position = position;
        }

        public Piece deletePiece(Position pos)
        {
            if (piece(pos) == null)
                return null;
            Piece aux = piece(pos);
            aux.position = null;
            _pieces[pos.row, pos.column] = null;
            return aux;
        }

        public bool validPosition(Position pos)
        {
            if (pos.row < 0 || pos.row >= rows || pos.column < 0 || pos.column >= columns)
                return false;
            return true;
        }

        public void validatePosition(Position pos)
        {
            if (!validPosition(pos))
                throw new BoardException("Invalid position!");
        }
    }
}
