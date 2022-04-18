namespace Board
{
    internal class Position
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Position()
        {

        }

        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }
    }
}
