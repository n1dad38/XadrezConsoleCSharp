namespace Board
{
    abstract class Piece
    {
        public Position? Position { get; set; } = new Position();
        public Color Color { get; protected set; }
        public int QtMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            QtMovements = 0;
            Board = board;
        }

        public bool anyPossibleMove()
        {
            bool[,] mat = PossibleMoves();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i,j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool possibleMovement(Position pos)
        {
            return PossibleMoves()[pos.Line, pos.Column];
        }

        public void incMovementQt()
        {
            QtMovements++;
        }

        public void decMovementQt()
        {
            QtMovements--;
        }

        public abstract bool[,] PossibleMoves();
    }
}
