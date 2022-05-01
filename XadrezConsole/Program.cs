using Board;
using Game;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            ChessMatch chessMatch = new ChessMatch();
            Position from = new Position(-1, -1);

            while (!chessMatch.finished)
            {
                
                try
                {
                    Console.Clear();


                    if (from.Line == -1 && from.Column == -1)
                    {
                        Screen.printMatch(chessMatch);
                        Console.WriteLine();

                        Console.Write("From: ");
                        from = Screen.readChessPositioning().toPosition();
                        chessMatch.validateFromPosition(from);
                        Console.Clear();
                    }

                    bool[,] possiblePositions = chessMatch.board.Piece(from).PossibleMoves();
                    Screen.Print(chessMatch.board, possiblePositions);

                    Console.WriteLine();

                    Console.Write("To: ");
                    Position to = Screen.readChessPositioning().toPosition();

                    chessMatch.validateToPosition(from, to);

                    chessMatch.makeMove(from, to);
                }
                catch (BoardException e) { Console.WriteLine(e.Message); Console.ReadLine(); }
                catch (IndexOutOfRangeException) { Console.WriteLine("This position does not exist."); Console.ReadLine(); }
                finally { from.DefineValues(-1, -1); }
            }

        }
    }
}