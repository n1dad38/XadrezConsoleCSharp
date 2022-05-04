using Board;

namespace Game
{
    internal class Queen : Piece
    {
        public Queen(Color color, Board.Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "Q";
        }

        private bool CanMove(Position pos)
        {
            Piece? p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            if (Position != null)
            {

                Position pos = new(Position.Line, Position.Column);

                // n
                pos.DefineValues(pos.Line - 1, pos.Column);
                while (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                    if (Board.Piece(pos) != null && Board.Piece(pos)?.Color != Color)
                    {
                        mat[pos.Line, pos.Column] = true;
                        break;
                    }
                    pos.DefineValues(pos.Line - 1, pos.Column);
                }
                pos.DefineValues(Position.Line, Position.Column);


                // w
                pos.DefineValues(pos.Line + 1, pos.Column);
                while (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                    if (Board.Piece(pos) != null && Board.Piece(pos)?.Color != Color)
                    {
                        mat[pos.Line, pos.Column] = true;
                        break;
                    }
                    pos.DefineValues(pos.Line + 1, pos.Column);
                }
                pos.DefineValues(Position.Line, Position.Column);


                // e
                pos.DefineValues(pos.Line, pos.Column + 1);
                while (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                    if (Board.Piece(pos) != null && Board.Piece(pos)?.Color != Color)
                    {
                        mat[pos.Line, pos.Column] = true;
                        break;
                    }
                    pos.DefineValues(pos.Line, pos.Column + 1);
                }
                pos.DefineValues(Position.Line, Position.Column);


                // w
                pos.DefineValues(pos.Line, pos.Column - 1);
                while (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                    if (Board.Piece(pos) != null && Board.Piece(pos)?.Color != Color)
                    {
                        mat[pos.Line, pos.Column] = true;
                        break;
                    }
                    pos.DefineValues(pos.Line, pos.Column - 1);
                }
                pos.DefineValues(Position.Line, Position.Column);


                // nw
                pos.DefineValues(pos.Line - 1, pos.Column - 1);
                while (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                    if (Board.Piece(pos) != null && Board.Piece(pos)?.Color != Color)
                    {
                        mat[pos.Line, pos.Column] = true;
                        break;
                    }
                    pos.DefineValues(pos.Line - 1, pos.Column - 1);
                }
                pos.DefineValues(Position.Line, Position.Column);


                // sw
                pos.DefineValues(pos.Line + 1, pos.Column - 1);
                while (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                    if (Board.Piece(pos) != null && Board.Piece(pos)?.Color != Color)
                    {
                        mat[pos.Line, pos.Column] = true;
                        break;
                    }
                    pos.DefineValues(pos.Line + 1, pos.Column - 1);
                }
                pos.DefineValues(Position.Line, Position.Column);


                // se
                pos.DefineValues(pos.Line + 1, pos.Column + 1);
                while (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                    if (Board.Piece(pos) != null && Board.Piece(pos)?.Color != Color)
                    {
                        mat[pos.Line, pos.Column] = true;
                        break;
                    }
                    pos.DefineValues(pos.Line + 1, pos.Column + 1);
                }
                pos.DefineValues(Position.Line, Position.Column);


                // ne
                pos.DefineValues(pos.Line - 1, pos.Column + 1);
                while (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                    if (Board.Piece(pos) != null && Board.Piece(pos)?.Color != Color)
                    {
                        mat[pos.Line, pos.Column] = true;
                        break;
                    }
                    pos.DefineValues(pos.Line - 1, pos.Column + 1);
                }
                pos.DefineValues(Position.Line, Position.Column);

                return mat;
            }
            return mat;
        }
    }
}
