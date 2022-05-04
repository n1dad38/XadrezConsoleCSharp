using Board;

namespace Game
{
    internal class ChessPositioning
    {
        public char Column { get; set; }
        public int Line { get; set; }

        public ChessPositioning(char column, int line)
        {
            Column = column;
            Line = line;
        }

        public Position ToPosition()
        {
            return new Position(8 - Line, Column - 'a');
        }

        public override string ToString()
        {
            return "" + Column + Line;
        }
    }
}
