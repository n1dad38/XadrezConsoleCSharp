using Board;

namespace Game
{
    internal class Pawn : Piece
    {
        public ChessMatch Match { get; private set; }
        public Pawn(Color color, Board.Board board, ChessMatch match) : base(color, board)
        {
            Match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool IsEnemy(Position pos)
        {
            Piece? piece = Board.Piece(pos);
            return piece != null && piece.Color != Color;
        }

        private bool CanMove(Position pos)
        {
            Piece? p = Board.Piece(pos);
            return p == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new(0, 0);

            if (Position != null)
            {

                if (Color == Color.White)
                {
                    pos.DefineValues(Position.Line - 1, Position.Column);
                    if (Board.ValidPosition(pos) && CanMove(pos))
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                    pos.DefineValues(Position.Line - 2, Position.Column);
                    if (Board.ValidPosition(pos) && CanMove(pos) && QtMovements == 0)
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                    pos.DefineValues(Position.Line - 1, Position.Column - 1);
                    if (Board.ValidPosition(pos) && IsEnemy(pos))
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                    pos.DefineValues(Position.Line - 1, Position.Column + 1);
                    if (Board.ValidPosition(pos) && IsEnemy(pos))
                    {
                        mat[pos.Line, pos.Column] = true;
                    }

                    // Special Move En Passant for White
                    if (Position.Line == 3)
                    {
                        Position left = new(Position.Line, Position.Column - 1);
                        Position right = new(Position.Line, Position.Column + 1);
                        if (Board.ValidPosition(left) && IsEnemy(left) && Board.Piece(left) == Match.WeakEnPassant)
                        {
                            mat[left.Line - 1, left.Column] = true;
                        }
                        if (Board.ValidPosition(right) && IsEnemy(right) && Board.Piece(right) == Match.WeakEnPassant)
                        {
                            mat[right.Line - 1, right.Column] = true;
                        }
                    }
                }
                if (Color == Color.Black)
                {
                    pos.DefineValues(Position.Line + 1, Position.Column);
                    if (Board.ValidPosition(pos) && CanMove(pos))
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                    pos.DefineValues(Position.Line + 2, Position.Column);
                    if (Board.ValidPosition(pos) && CanMove(pos) && QtMovements == 0)
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                    pos.DefineValues(Position.Line + 1, Position.Column - 1);
                    if (Board.ValidPosition(pos) && IsEnemy(pos))
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                    pos.DefineValues(Position.Line + 1, Position.Column + 1);
                    if (Board.ValidPosition(pos) && IsEnemy(pos))
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                    // Special Move En Passant for Black
                    if (Position.Line == 4)
                    {
                        Position left = new(Position.Line, Position.Column - 1);
                        Position right = new(Position.Line, Position.Column + 1);
                        if (Board.ValidPosition(left) && IsEnemy(left) && Board.Piece(left) == Match.WeakEnPassant)
                        {
                            mat[left.Line + 1, left.Column] = true;
                        }
                        if (Board.ValidPosition(right) && IsEnemy(right) && Board.Piece(right) == Match.WeakEnPassant)
                        {
                            mat[right.Line + 1, right.Column] = true;
                        }
                    }
                }

                return mat;
            }
            return mat;
        }
    }
}
