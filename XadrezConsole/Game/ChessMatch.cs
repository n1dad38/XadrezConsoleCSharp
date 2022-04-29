using Board;

namespace Game
{
    internal class ChessMatch
    {
        public Board.Board board { get; private set; }
        public int round { get; private set; }
        public Color curPlayer { get; private set; }
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

        public void execPlay(Position from, Position to)
        {
            move(from, to);
            round++;
            changePlayer();
        }

        private void changePlayer()
        {
            if (curPlayer == Color.White)
            {
                curPlayer = Color.Black;
            }
            else
            {
                curPlayer = Color.White;
            }
        }

        public void validateFromPosition(Position pos)
        {
            if (board.Piece(pos) == null)
            {
                throw new BoardException("There is no piece at this position.");
            } 
            if (curPlayer != board.Piece(pos).Color)
            {
                throw new BoardException("You cannot move a piece from another player.");
            }
            if (!board.Piece(pos).anyPossibleMove())
            {
                throw new BoardException("There is no move avaiable for this piece.");
            }
        }

        public void validateToPosition(Position from, Position to)
        {
            if (!board.Piece(from).canMoveTo(to))
            {
                throw new BoardException("Cannot make this move.");
            } 
        }

        private void PlacePiece()
        {
            //White
            //Special
            board.PlacePiece(new Tower(Color.White, board), new ChessPositioning('e', 2).toPosition());
            board.PlacePiece(new Tower(Color.White, board), new ChessPositioning('d', 1).toPosition());
            board.PlacePiece(new Tower(Color.White, board), new ChessPositioning('d', 2).toPosition());
            board.PlacePiece(new King(Color.White, board), new ChessPositioning('e', 1).toPosition());
            board.PlacePiece(new Tower(Color.White, board), new ChessPositioning('f', 1).toPosition());
            board.PlacePiece(new Tower(Color.White, board), new ChessPositioning('f', 2).toPosition());

            //Black
            //Special
            board.PlacePiece(new Tower(Color.Black, board), new ChessPositioning('a', 8).toPosition());
            board.PlacePiece(new King(Color.Black, board), new ChessPositioning('e', 8).toPosition());
            board.PlacePiece(new Tower(Color.Black, board), new ChessPositioning('h', 8).toPosition());

        }
    }
}
