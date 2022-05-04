using Game;

namespace Board
{
    internal class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            Print(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Round: " + match.Round);
            if (!match.Finished)
            {
                Console.Write("Waiting move from: ");
                if (match.CurPlayer == Color.Black)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(match.CurPlayer);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(match.CurPlayer);
                }
                if (match.Check)
                {
                    Console.WriteLine("Check!");
                }
            }
            else
            {
                Console.WriteLine("Checkmate!");
                Console.WriteLine("Winner: " + match.CurPlayer);
            }
        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces:");
            Console.WriteLine();
            Console.Write("White: ");
            PrintSet(match.CapturedPieces(Color.White));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Black: ");
            PrintSet(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach (Piece piece in set)
            {
                Console.Write(piece + " ");
            }
            Console.Write("]");
            Console.WriteLine();
        }

        public static void Print(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPiece(board.Piece(i, j));
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void Print(Board board, bool[,] possiblePositions)
        {

            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor alteredBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = alteredBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(board.Piece(i, j));
                }
                Console.BackgroundColor = originalBackground;
                Console.WriteLine();
            }
            Console.BackgroundColor = originalBackground;
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintPiece(Piece? piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static ChessPositioning ReadChessPositioning()
        {
            string? s = Console.ReadLine();
            if (!string.IsNullOrEmpty(s))
            {
                char col = char.ToLower(s[0]);
                int row = int.Parse(s[1].ToString());
                return new ChessPositioning(col, row);
            }
            return new ChessPositioning('a',0);
        }
    }
}
