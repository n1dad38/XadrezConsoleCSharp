using Board;
using Game;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Board.Board board = new Board.Board(8,8);
            board.PlacePiece(new King(Color.Black, board), new Position(2,3));
            board.PlacePiece(new Tower(Color.Black, board), new Position(2,4));
            Screen.Print(board);
        }
    }
}