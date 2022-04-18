using Board;

namespace Game
{
    internal class Knight : Piece
    {
        public Knight(Color color, Board.Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "k";
        }
    }
}
