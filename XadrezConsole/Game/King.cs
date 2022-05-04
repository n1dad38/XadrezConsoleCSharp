using Board;

namespace Game
{
    internal class King : Piece
    {
        public ChessMatch Match { get; private set; }
        public King(Color color, Board.Board board, ChessMatch match) : base(color, board)
        {
            this.Match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position pos)
        {
            Piece? p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        private bool CanCastle(Position pos)
        {
            Piece? piece = Board.Piece(pos);
            return piece != null && piece is Rook && piece.Color == Color && piece.QtMovements == 0 && QtMovements == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            if (Position != null)
            {
                Position pos = new(Position.Line, Position.Column);
                // n
                pos.DefineValues(pos.Line - 1, pos.Column);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line, Position.Column);

                // ne
                pos.DefineValues(pos.Line - 1, pos.Column + 1);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line, Position.Column);

                // e
                pos.DefineValues(pos.Line, pos.Column + 1);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line, Position.Column);

                // se
                pos.DefineValues(pos.Line + 1, pos.Column + 1);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line, Position.Column);

                // s
                pos.DefineValues(pos.Line + 1, pos.Column);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line, Position.Column);

                // sw
                pos.DefineValues(pos.Line + 1, pos.Column - 1);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line, Position.Column);

                // w
                pos.DefineValues(pos.Line, pos.Column - 1);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line, Position.Column);

                //nw
                pos.DefineValues(pos.Line - 1, pos.Column - 1);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line, Position.Column);

                // SpecialMove Castling
                if (QtMovements == 0 && !Match.Check)
                {
                    // SpecialMove Castling Short
                    Position posRookShort = new(Position.Line, Position.Column + 3);
                    if (CanCastle(posRookShort))
                    {
                        Position kingPlus1 = new(Position.Line, Position.Column + 1);
                        Position kingPlus2 = new(Position.Line, Position.Column + 2);
                        if (Board.Piece(kingPlus1) == null && Board.Piece(kingPlus2) == null)
                        {
                            mat[Position.Line, Position.Column + 2] = true;
                        }
                    }
                    // SpecialMove Castling Long
                    Position posRookLong = new(Position.Line, Position.Column - 4);
                    if (CanCastle(posRookLong))
                    {
                        Position kingSub1 = new(Position.Line, Position.Column - 1);
                        Position kingSub2 = new(Position.Line, Position.Column - 2);
                        Position kingSub3 = new(Position.Line, Position.Column - 3);
                        if (Board.Piece(kingSub1) == null && Board.Piece(kingSub2) == null && Board.Piece(kingSub3) == null)
                        {
                            mat[Position.Line, Position.Column - 2] = true;
                        }
                    }
                }


                return mat;
            }
            return mat;
        }
    }
}
