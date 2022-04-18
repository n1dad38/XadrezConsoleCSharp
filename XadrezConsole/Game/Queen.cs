using Board;

namespace Game
{
    internal class Queen : Piece
    {
        public Queen(Color color, Board.Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}
