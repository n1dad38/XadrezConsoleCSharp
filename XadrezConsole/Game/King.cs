using Board;

namespace Game
{
    internal class King : Piece
    {
        public King(Color color, Board.Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
