using Board;

namespace Game
{
    internal class Pawn : Piece
    {
        private ChessMatch match;
        public Pawn(Color color, Board.Board board, ChessMatch match) : base(color, board)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool isEnemy(Position pos)
        {
            Piece piece = Board.Piece(pos);
            return piece != null && piece.Color != Color;
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

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
                if (Board.ValidPosition(pos) && isEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && isEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                // Special Move En Passant for White
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(left) && isEnemy(left) && Board.Piece(left) == match.weakEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    if (Board.ValidPosition(right) && isEnemy(right) && Board.Piece(right) == match.weakEnPassant)
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
                if (Board.ValidPosition(pos) && isEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && isEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                // Special Move En Passant for Black
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(left) && isEnemy(left) && Board.Piece(left) == match.weakEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    if (Board.ValidPosition(right) && isEnemy(right) && Board.Piece(right) == match.weakEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
