using Board;
using Game;

namespace XadrezConsole
{
    class Program
    {
        static void Main()
        {

            ChessMatch chessMatch = new();
            Position from;

            while (!chessMatch.Finished)
            {
                try
                {
                    Console.Clear();


                    Screen.PrintMatch(chessMatch);
                    Console.WriteLine();

                    Console.Write("From: ");
                    from = Screen.ReadChessPositioning().ToPosition();
                    chessMatch.ValidateFromPosition(from);
                    Console.Clear();

                    bool[,]? possiblePositions = chessMatch.Board.Piece(from)?.PossibleMoves();
                    if (possiblePositions != null)
                        Screen.Print(chessMatch.Board, possiblePositions);

                    Console.WriteLine();

                    Console.Write("To: ");
                    Position to = Screen.ReadChessPositioning().ToPosition();

                    chessMatch.ValidateToPosition(from, to);

                    chessMatch.HandleMove(from, to);
                }
                catch (BoardException e) { Console.WriteLine(e.Message); Console.ReadLine(); }
                catch (Exception) { Console.WriteLine("Invalid input."); Console.ReadLine(); }
            }
            Console.Clear();
            Screen.PrintMatch(chessMatch);
        }
    }
}