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

        public void placePiece(Piece p, Position position)
        {
            _pieces[position.row, position.column] = p;
            p.position = position;
        }
    }
}
