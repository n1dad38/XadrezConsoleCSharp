namespace Board
{
    internal class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        public Piece?[,] Pieces { get; private set; }

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines, columns];
        }

        public Piece? Piece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece? Piece(Position pos)
        {
            return Pieces[pos.Line, pos.Column];
        }

        public bool ContainPiece(Position pos)
        {
            ValidatePosition(pos);
            return Piece(pos) != null;
        }

        public void PlacePiece(Piece? p, Position pos)
        {
            if (ContainPiece(pos))
            {
                throw new BoardException("Contains a piece in this position.");
            }
            Pieces[pos.Line, pos.Column] = p;
            if (p is not null)
            p.Position = pos;
        }


        public Piece? RemovePiece(Position pos)
        {
            if (Piece(pos) == null)
            {
                return null;
            }
            Piece? aux = Piece(pos);
            if (aux != null)
                aux.Position = null;
            Pieces[pos.Line, pos.Column] = null;
            return aux;
        }

        public bool ValidPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Column < 0 || pos.Line >= Lines || pos.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardException("Invalid Position.");
            }
        }
    }
}
