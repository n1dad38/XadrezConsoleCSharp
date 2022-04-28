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
                    Screen.Print(chessMatch.board);
                    Console.WriteLine();
                    Console.Write("From: ");
                    Position from = Screen.readChessPositioning().toPosition();

                    bool[,] possiblePositions = chessMatch.board.Piece(from).PossibleMoves();

                    

                    Console.Clear();
                    Screen.Print(chessMatch.board, possiblePositions);

                    Console.WriteLine();
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