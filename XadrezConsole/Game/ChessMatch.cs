using Board;

namespace Game
{
    internal class ChessMatch
    {
        public Board.Board board { get; private set; }
        public int round { get; private set; }
        public Color curPlayer { get; private set; }
        public bool finished { get; set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }

        public ChessMatch()
        {
            board = new Board.Board(8, 8);
            round = 1;
            curPlayer = Color.White;
            finished = false;
            check = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            PlacePieces();
        }

        public Piece move(Position from, Position to)
        {
            Piece p = board.RemovePiece(from);
            p.incMovementQt();
            Piece capturedPiece = board.RemovePiece(to);
            board.PlacePiece(p, to);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void makeMove(Position from, Position to)
        {
            Piece capturedPiece = move(from, to);

            if (isInCheck(curPlayer))
            {
                undoMovement(from, to, capturedPiece);
                throw new BoardException("You cannot place yourself in check.");
            }

            if (isInCheck(enemyColor(curPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            round++;
            changePlayer();
        }

        public void undoMovement(Position from, Position to, Piece capturedPiece)
        {
            Piece piece = board.RemovePiece(to);
            piece.decMovementQt();
            if (capturedPiece != null)
            {
                board.PlacePiece(capturedPiece, to);
                captured.Remove(capturedPiece);
            }
            board.PlacePiece(piece, from);
        }

        public void validateFromPosition(Position from)
        {
            if (board.Piece(from) == null)
            {
                throw new BoardException("There is no piece at this position.");
            }
            if (curPlayer != board.Piece(from).Color)
            {
                throw new BoardException("You cannot move a piece from another player.");
            }
            if (!board.Piece(from).anyPossibleMove())
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

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> assistant = new HashSet<Piece>();
            foreach (Piece piece in captured)
            {
                if (piece.Color == color)
                {
                    assistant.Add(piece);
                }
            }
            return assistant;
        }

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> assistant = new HashSet<Piece>();
            foreach (Piece piece in pieces)
            {
                if (piece.Color == color)
                {
                    assistant.Add(piece);
                }
            }
            assistant.ExceptWith(capturedPieces(color));
            return assistant;
        }

        public void PlaceNewPiece(char column, int line, Piece piece)
        {
            board.PlacePiece(piece, new ChessPositioning(column, line).toPosition());
            pieces.Add(piece);
        }

        public bool isInCheck(Color color)
        {
            Piece k = king(color);
            foreach (Piece piece in piecesInGame(enemyColor(color)))
            {
                bool[,] mat = piece.PossibleMoves();
                if (mat[k.Position.Line, k.Position.Column])
                {
                    return true;
                }
            }
            return false;
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


        private void PlacePieces()
        {
            //White
            //Special
            PlaceNewPiece('e', 2, new Tower(Color.White, board));
            PlaceNewPiece('d', 1, new Tower(Color.White, board));
            PlaceNewPiece('d', 2, new Tower(Color.White, board));
            PlaceNewPiece('e', 1, new King(Color.White, board));
            PlaceNewPiece('f', 1, new Tower(Color.White, board));
            PlaceNewPiece('f', 2, new Tower(Color.White, board));

            //Black
            //Special
            PlaceNewPiece('d', 8, new Tower(Color.Black, board));
            PlaceNewPiece('d', 7, new Tower(Color.Black, board));
            PlaceNewPiece('e', 7, new Tower(Color.Black, board));
            PlaceNewPiece('e', 8, new King(Color.Black, board));
            PlaceNewPiece('f', 8, new Tower(Color.Black, board));
            PlaceNewPiece('f', 7, new Tower(Color.Black, board));

        }

        private Color enemyColor(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece piece in piecesInGame(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }
    }
}
