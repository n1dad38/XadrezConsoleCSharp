namespace Board
{
    internal class Piece
    {
        public Position Position { get; set; } = new Position();
        public Color Color { get; protected set; }
        public int QtMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Position position, Color color, int qtMovements, Board board)
        {
            Position = position;
            Color = color;
            QtMovements = 0;
            Board = board;
        }
    }
}
