using Board;
using Game;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            ChessMatch chessMatch = new ChessMatch();

            while (!chessMatch.finished)
            {
                try
                {
                    Console.Clear();

                    Screen.Print(chessMatch.board);

                    Console.WriteLine();
                    Console.WriteLine("Round: " + chessMatch.round);

                    Console.WriteLine("Waiting move from: " + chessMatch.curPlayer);
                    Console.WriteLine();

                    Console.Write("From: ");
                    Position from = Screen.readChessPositioning().toPosition();
                    chessMatch.validateFromPosition(from);

                    bool[,] possiblePositions = chessMatch.board.Piece(from).PossibleMoves();



                    Console.Clear();
                    Screen.Print(chessMatch.board, possiblePositions);

                    Console.WriteLine();
                    while (true)
                    {
                        Console.Write("To: ");
                        Position to = Screen.readChessPositioning().toPosition();

                        chessMatch.validateToPosition(from, to);

                        chessMatch.execPlay(from, to);
                        if (chessMatch.board.Piece(from) == null)
                        {
                            break;
                        }
                    }
                }
                catch (BoardException e) { Console.WriteLine(e.Message); Console.ReadLine(); }
            }

        }
    }
}