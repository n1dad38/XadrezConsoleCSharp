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
            board.PlacePiece(new Knight(Color.White, board), new ChessPositioning('b', 1).toPosition());
            board.PlacePiece(new Bishop(Color.White, board), new ChessPositioning('c', 1).toPosition());
            board.PlacePiece(new Queen(Color.White, board), new ChessPositioning('d', 1).toPosition());
            board.PlacePiece(new King(Color.White, board), new ChessPositioning('e', 1).toPosition());
            board.PlacePiece(new Bishop(Color.White, board), new ChessPositioning('f', 1).toPosition());
            board.PlacePiece(new Knight(Color.White, board), new ChessPositioning('g', 1).toPosition());
            board.PlacePiece(new Tower(Color.White, board), new ChessPositioning('h', 1).toPosition());
            //Pawns
            board.PlacePiece(new Pawn(Color.White, board), new ChessPositioning('a', 2).toPosition());
            board.PlacePiece(new Pawn(Color.White, board), new ChessPositioning('b', 2).toPosition());
            board.PlacePiece(new Pawn(Color.White, board), new ChessPositioning('c', 2).toPosition());
            board.PlacePiece(new Pawn(Color.White, board), new ChessPositioning('d', 2).toPosition());
            board.PlacePiece(new Pawn(Color.White, board), new ChessPositioning('e', 2).toPosition());
            board.PlacePiece(new Pawn(Color.White, board), new ChessPositioning('f', 2).toPosition());
            board.PlacePiece(new Pawn(Color.White, board), new ChessPositioning('g', 2).toPosition());
            board.PlacePiece(new Pawn(Color.White, board), new ChessPositioning('h', 2).toPosition());

            //Black
            //Special
            board.PlacePiece(new Tower(Color.Black, board), new ChessPositioning('a', 8).toPosition());
            board.PlacePiece(new Knight(Color.Black, board), new ChessPositioning('b', 8).toPosition());
            board.PlacePiece(new Bishop(Color.Black, board), new ChessPositioning('c', 8).toPosition());
            board.PlacePiece(new Queen(Color.Black, board), new ChessPositioning('d', 8).toPosition());
            board.PlacePiece(new King(Color.Black, board), new ChessPositioning('e', 8).toPosition());
            board.PlacePiece(new Bishop(Color.Black, board), new ChessPositioning('f', 8).toPosition());
            board.PlacePiece(new Knight(Color.Black, board), new ChessPositioning('g', 8).toPosition());
            board.PlacePiece(new Tower(Color.Black, board), new ChessPositioning('h', 8).toPosition());
            //Pawns
            board.PlacePiece(new Pawn(Color.Black, board), new ChessPositioning('a', 7).toPosition());
            board.PlacePiece(new Pawn(Color.Black, board), new ChessPositioning('b', 7).toPosition());
            board.PlacePiece(new Pawn(Color.Black, board), new ChessPositioning('c', 7).toPosition());
            board.PlacePiece(new Pawn(Color.Black, board), new ChessPositioning('d', 7).toPosition());
            board.PlacePiece(new Pawn(Color.Black, board), new ChessPositioning('e', 7).toPosition());
            board.PlacePiece(new Pawn(Color.Black, board), new ChessPositioning('f', 7).toPosition());
            board.PlacePiece(new Pawn(Color.Black, board), new ChessPositioning('g', 7).toPosition());
            board.PlacePiece(new Pawn(Color.Black, board), new ChessPositioning('h', 7).toPosition());

        }
    }
}
