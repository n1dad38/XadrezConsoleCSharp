using Board;
using Game;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessMatch chessMatch = new ChessMatch();
            Screen.Print(chessMatch.board);
        }
    }
}