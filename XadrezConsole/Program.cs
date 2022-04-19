using Board;
using Game;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch chessMatch = new ChessMatch();

                while (!chessMatch.finished)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Screen.Print(chessMatch.board);
                    Console.WriteLine();
                    Console.Write("From: ");
                    Position from = Screen.readChessPositioning().toPosition();
                    Console.Write("To: ");
                    Position to = Screen.readChessPositioning().toPosition();

                    chessMatch.move(from, to);
                }
            }
            catch (BoardException e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}