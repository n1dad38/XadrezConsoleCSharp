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
            return "H";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(Position.Line, Position.Column);

            // ne
            pos.DefineValues(pos.Line - 2, pos.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.DefineValues(Position.Line, Position.Column);

            // en
            pos.DefineValues(pos.Line - 1, pos.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.DefineValues(Position.Line, Position.Column);

            // es
            pos.DefineValues(pos.Line + 1, pos.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.DefineValues(Position.Line, Position.Column);

            // se
            pos.DefineValues(pos.Line + 2, pos.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.DefineValues(Position.Line, Position.Column);

            // sw
            pos.DefineValues(pos.Line + 2, pos.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.DefineValues(Position.Line, Position.Column);

            // ws
            pos.DefineValues(pos.Line + 1, pos.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.DefineValues(Position.Line, Position.Column);

            // wn
            pos.DefineValues(pos.Line - 1, pos.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.DefineValues(Position.Line, Position.Column);

            //nw
            pos.DefineValues(pos.Line - 2, pos.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.DefineValues(Position.Line, Position.Column);

            return mat;
        }
    }
}
