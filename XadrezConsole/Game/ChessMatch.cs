using Board;

namespace Game
{
    internal class ChessMatch
    {
        public Board.Board board { get; private set; }
        private int round;
        private Color curPlayer;
        public bool finished { get; set; }

        public ChessMatch()
        {
            board = new Board.Board(8, 8);
            round = 1;
            curPlayer = Color.White;
            finished = false;
            PlacePiece();
        }

        public void move(Position from, Position to)
        {
            Piece p = board.RemovePiece(from);
            p.incMovementQt();
            board.RemovePiece(from);
            board.PlacePiece(p, to);
        }

        private void PlacePiece()
        {
            //White
            //Special
            board.PlacePiece(new Tower(Color.White, board), new ChessPositioning('a', 1).toPosition());
            board.PlacePiece(new King(Color.White, board), new ChessPositioning('e', 1).toPosition());
            board.PlacePiece(new Tower(Color.White, board), new ChessPositioning('h', 1).toPosition());

            //Black
            //Special
            board.PlacePiece(new Tower(Color.Black, board), new ChessPositioning('a', 8).toPosition());
            board.PlacePiece(new King(Color.Black, board), new ChessPositioning('e', 8).toPosition());
            board.PlacePiece(new Tower(Color.Black, board), new ChessPositioning('h', 8).toPosition());

        }
    }
}
