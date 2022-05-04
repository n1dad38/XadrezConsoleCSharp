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

        public bool AnyPossibleMove()
        {
            bool[,]? mat = PossibleMoves();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat is not null)
                    {
                        if (mat[i, j])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool PossibleMovement(Position pos)
        {
            return PossibleMoves()[pos.Line, pos.Column];
        }

        public void IncMovementQt()
        {
            QtMovements++;
        }

        public void DecMovementQt()
        {
            QtMovements--;
        }

        public abstract bool[,] PossibleMoves();
    }
}
