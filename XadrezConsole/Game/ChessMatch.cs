using Board;

namespace Game
{
    internal class ChessMatch
    {
        public Board.Board Board { get; private set; }
        public int Round { get; private set; }
        public Color CurPlayer { get; private set; }
        public bool Finished { get; set; }
        public HashSet<Piece> Pieces { get; private set; }
        public HashSet<Piece> Captured { get; private set; }
        public bool Check { get; private set; }
        public Piece? WeakEnPassant { get; private set; }
        private char promotedTo;
        private Piece? promoted;

        public ChessMatch()
        {
            Board = new Board.Board(8, 8);
            Round = 1;
            CurPlayer = Color.White;
            Finished = false;
            Check = false;
            WeakEnPassant = null;
            promoted = null;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PlacePieces();
        }

        public Piece? MakeMove(Position from, Position to)
        {
            Piece? p = Board.RemovePiece(from);
            p?.IncMovementQt();
            Piece? capturedPiece = Board.RemovePiece(to);
            Board.PlacePiece(p, to);
            if (capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }

            // Special Move Pawn Promotion
            if (p is Pawn)
            {
                if ((p.Color == Color.White && to.Line == 0) || (p.Color == Color.Black && to.Line == 7))
                {
                    Console.Write("Choose your promotion (R/H/B/Q): ");

                    string? promotedToInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(promotedToInput))
                    {
                        UndoMovement(from, to, capturedPiece);
                        throw new BoardException("Invalid promotion.");
                    }
                    promotedTo = char.Parse(promotedToInput);
                    if (
                    promotedTo != 'R' &&
                    promotedTo != 'H' &&
                    promotedTo != 'B' &&
                    promotedTo != 'Q' &&
                    promotedTo != 'r' &&
                    promotedTo != 'h' &&
                    promotedTo != 'b' &&
                    promotedTo != 'q')
                    {
                        UndoMovement(from, to, capturedPiece);
                        throw new BoardException("Invalid promotion.");
                    }
                    p = Board.RemovePiece(to);
                    if (p != null)
                    {
                        Pieces.Remove(p);
                    }
                    if ((promotedTo is 'R' or 'r') && p != null)
                    {
                        promoted = new Rook(p.Color, Board);
                    }
                    else if ((promotedTo is 'H' or 'h') && p != null)
                    {
                        promoted = new Knight(p.Color, Board);
                    }
                    else if ((promotedTo is 'B' or 'b') && p != null)
                    {
                        promoted = new Bishop(p.Color, Board);
                    }
                    else if ((promotedTo is 'Q' or 'q') && p != null)
                    {
                        promoted = new Queen(p.Color, Board);
                    }

                    if (promoted != null)
                    {
                        Board.PlacePiece(promoted, to);
                        Pieces.Add(promoted);
                    }
                }
            }



            // Special Move Castling
            if (p is King)
            {
                // Special Move Castling Short
                if (to.Column == from.Column + 2)
                {
                    Position fromRook = new(from.Line, from.Column + 3);
                    Position toRook = new(to.Line, to.Column - 1);
                    Piece? rook = Board.RemovePiece(fromRook);
                    rook?.IncMovementQt();
                    if (rook != null)
                        Board.PlacePiece(rook, toRook);
                }
                // Special Move Castling Long
                else if (to.Column == from.Column - 2)
                {
                    Position fromRook = new(from.Line, from.Column - 4);
                    Position toRook = new(to.Line, to.Column + 1);
                    Piece? rook = Board.RemovePiece(fromRook);
                    rook?.IncMovementQt();
                    if (rook != null)
                        Board.PlacePiece(rook, toRook);
                }
            }

            // Special Move En Passant
            if (p is Pawn)
            {
                if (to.Column != from.Column && capturedPiece == null)
                {
                    Position posWeakPawn;
                    if (p.Color == Color.White)
                    {
                        posWeakPawn = new Position(to.Line + 1, to.Column);
                    }
                    else
                    {
                        posWeakPawn = new Position(to.Line - 1, to.Column);
                    }
                    capturedPiece = Board.RemovePiece(posWeakPawn);
                    if (capturedPiece != null)
                        Captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void HandleMove(Position from, Position to)
        {
            Piece? capturedPiece = MakeMove(from, to);

            if (IsInCheck(CurPlayer))
            {
                UndoMovement(from, to, capturedPiece);
                throw new BoardException("You cannot place yourself in check.");
            }

            Piece? piece = Board.Piece(to);

            if (IsInCheck(EnemyColor(CurPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (TestCheckMate(EnemyColor(CurPlayer)))
            {
                Finished = true;
            }
            else
            {
                Round++;
                ChangePlayer();
            }

            // Special Move En Passant
            if (piece is Pawn && (to.Line == from.Line - 2) || (to.Line == from.Line + 2))
            {
                WeakEnPassant = piece;
            }
            else
            {
                WeakEnPassant = null;
            }
        }

        public void UndoMovement(Position from, Position to, Piece? capturedPiece)
        {
            Piece? piece = Board.RemovePiece(to);
            if (piece != null)
            {
                piece.DecMovementQt();
            }
            if (capturedPiece != null)
            {
                Board.PlacePiece(capturedPiece, to);
                Captured.Remove(capturedPiece);
            }
            Board.PlacePiece(piece, from);

            // Undo Special Move Castling
            if (piece is King)
            {
                // Undo Special Move Castling Short
                if (to.Column == from.Column + 2)
                {
                    Position fromRook = new(from.Line, from.Column + 3);
                    Position toRook = new(to.Line, to.Column - 1);
                    Piece? rook = Board.RemovePiece(toRook);
                    rook?.DecMovementQt();
                    Board.PlacePiece(rook, fromRook);
                }
                // Undo Special Move Castling Long
                else if (to.Column == from.Column - 2)
                {
                    Position fromRook = new(from.Line, from.Column - 4);
                    Position toRook = new(to.Line, to.Column + 1);
                    Piece? rook = Board.RemovePiece(toRook);
                    rook?.DecMovementQt();
                    Board.PlacePiece(rook, fromRook);
                }
            }

            if (piece is Pawn)
            {
                if (from.Column != to.Column && capturedPiece == WeakEnPassant)
                {
                    Piece? pawn = Board.RemovePiece(to);
                    Position posWeakPawn;
                    if (piece.Color == Color.White)
                    {
                        posWeakPawn = new Position(3, to.Column);
                    }
                    else
                    {
                        posWeakPawn = new Position(4, to.Column);
                    }
                    if (pawn != null)
                        Board.PlacePiece(pawn, posWeakPawn);
                }
            }
        }

        public void ValidateFromPosition(Position from)
        {
            if (Board.Piece(from) == null)
            {
                throw new BoardException("There is no piece at this position.");
            }
            if (CurPlayer != Board.Piece(from)?.Color)
            {
                throw new BoardException("You cannot move a piece from another player.");
            }
            Piece? pieceFrom = Board.Piece(from);
            if (pieceFrom != null)
            {
                if (!pieceFrom.AnyPossibleMove())
                {
                    throw new BoardException("There is no move avaiable for this piece.");
                }
            }
        }

        public void ValidateToPosition(Position from, Position to)
        {
            Piece? pieceFrom = Board.Piece(from);
            if (pieceFrom != null)
            {
                if (!pieceFrom.PossibleMovement(to))
                {
                    throw new BoardException("Cannot make this move.");
                }
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> assistant = new();
            foreach (Piece piece in Captured)
            {
                if (piece.Color == color)
                {
                    assistant.Add(piece);
                }
            }
            return assistant;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> assistant = new();
            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                {
                    assistant.Add(piece);
                }
            }
            assistant.ExceptWith(CapturedPieces(color));
            return assistant;
        }

        public void PlaceNewPiece(char column, int line, Piece piece)
        {
            Board.PlacePiece(piece, new ChessPositioning(column, line).ToPosition());
            Pieces.Add(piece);
        }

        public bool IsInCheck(Color color)
        {
            Piece? k = King(color);
            foreach (Piece piece in PiecesInGame(EnemyColor(color)))
            {
                bool[,] mat = piece.PossibleMoves();
                if (k?.Position != null)
                {
                    if (mat[k.Position.Line, k.Position.Column])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool TestCheckMate(Color color)
        {
            if (!IsInCheck(color)) { return false; }
            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] mat = piece.PossibleMoves();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position? from = piece.Position;
                            Position to = new(i, j);
                            if (from != null && to != null)
                            {
                                Piece? capturedPiece = MakeMove(from, to);
                                bool testCheck = IsInCheck(color);
                                UndoMovement(from, to, capturedPiece);
                                if (!testCheck)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void ChangePlayer()
        {
            if (CurPlayer == Color.White)
            {
                CurPlayer = Color.Black;
            }
            else
            {
                CurPlayer = Color.White;
            }
        }


        private void PlacePieces()
        {
            //White
            PlaceNewPiece('a', 1, new Rook(Color.White, Board));
            PlaceNewPiece('b', 1, new Knight(Color.White, Board));
            PlaceNewPiece('c', 1, new Bishop(Color.White, Board));
            PlaceNewPiece('d', 1, new Queen(Color.White, Board));
            PlaceNewPiece('e', 1, new King(Color.White, Board, this));
            PlaceNewPiece('f', 1, new Bishop(Color.White, Board));
            PlaceNewPiece('g', 1, new Knight(Color.White, Board));
            PlaceNewPiece('h', 1, new Rook(Color.White, Board));
            for (char i = 'a'; i <= 'h'; i++)
            {
                PlaceNewPiece(i, 2, new Pawn(Color.White, Board, this));
            }


            //Black
            PlaceNewPiece('a', 8, new Rook(Color.Black, Board));
            PlaceNewPiece('b', 8, new Knight(Color.Black, Board));
            PlaceNewPiece('c', 8, new Bishop(Color.Black, Board));
            PlaceNewPiece('d', 8, new Queen(Color.Black, Board));
            PlaceNewPiece('e', 8, new King(Color.Black, Board, this));
            PlaceNewPiece('f', 8, new Bishop(Color.Black, Board));
            PlaceNewPiece('g', 8, new Knight(Color.Black, Board));
            PlaceNewPiece('h', 8, new Rook(Color.Black, Board));
            for (char i = 'a'; i <= 'h'; i++)
            {
                PlaceNewPiece(i, 7, new Pawn(Color.Black, Board, this));
            }
        }

        private static Color EnemyColor(Color color)
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

        private Piece? King(Color color)
        {
            foreach (Piece piece in PiecesInGame(color))
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
