using Board;
namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Board.Board board = new Board.Board(8,8);
            Screen.Print(board);
        }
    }
}