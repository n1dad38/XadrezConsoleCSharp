using Board;

namespace Game
{
    internal class Pawn : Piece
    {
        public Pawn(Color color, Board.Board board) : base(color, board)
        {
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
            }

            return mat;
        }
    }
}
