using Board;

namespace Game
{
    internal class Tower : Piece
    {
        public Tower(Color color, Board.Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
