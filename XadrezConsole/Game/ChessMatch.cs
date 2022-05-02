﻿using Board;

namespace Game
{
    internal class ChessMatch
    {
        public Board.Board board { get; private set; }
        public int round { get; private set; }
        public Color curPlayer { get; private set; }
        public bool finished { get; set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }

        public ChessMatch()
        {
            board = new Board.Board(8, 8);
            round = 1;
            curPlayer = Color.White;
            finished = false;
            check = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            PlacePieces();
        }

        public Piece makeMove(Position from, Position to)
        {
            Piece p = board.RemovePiece(from);
            p.incMovementQt();
            Piece capturedPiece = board.RemovePiece(to);
            board.PlacePiece(p, to);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }

            // Special Move Castling
            if (p is King)
            {
                // Special Move Castling Short
                if (to.Column == from.Column + 2)
                {
                    Position fromRook = new Position(from.Line, from.Column + 3);
                    Position toRook = new Position(to.Line, to.Column - 1);
                    Piece rook = board.RemovePiece(fromRook);
                    rook.incMovementQt();
                    board.PlacePiece(rook, toRook);
                }
                // Special Move Castling Long
                else if (to.Column == from.Column - 2)
                {
                    Position fromRook = new Position(from.Line, from.Column - 4);
                    Position toRook = new Position(to.Line, to.Column + 1);
                    Piece rook = board.RemovePiece(fromRook);
                    rook.incMovementQt();
                    board.PlacePiece(rook, toRook);
                }
            }
            return capturedPiece;
        }

        public void handleMove(Position from, Position to)
        {
            Piece capturedPiece = makeMove(from, to);

            if (isInCheck(curPlayer))
            {
                undoMovement(from, to, capturedPiece);
                throw new BoardException("You cannot place yourself in check.");
            }

            if (isInCheck(enemyColor(curPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (testCheckMate(enemyColor(curPlayer)))
            {
                finished = true;
            }
            else
            {
                round++;
                changePlayer();
            }
        }

        public void undoMovement(Position from, Position to, Piece capturedPiece)
        {
            Piece piece = board.RemovePiece(to);
            piece.decMovementQt();
            if (capturedPiece != null)
            {
                board.PlacePiece(capturedPiece, to);
                captured.Remove(capturedPiece);
            }
                board.PlacePiece(piece, from);

            // Undo Special Move Castling
            if (piece is King)
            {
                // Undo Special Move Castling Short
                if (to.Column == from.Column + 2)
                {
                    Position fromRook = new Position(from.Line, from.Column + 3);
                    Position toRook = new Position(to.Line, to.Column - 1);
                    Piece rook = board.RemovePiece(toRook);
                    rook.decMovementQt();
                    board.PlacePiece(rook, fromRook);
                }
                // Undo Special Move Castling Long
                else if (to.Column == from.Column - 2)
                {
                    Position fromRook = new Position(from.Line, from.Column - 4);
                    Position toRook = new Position(to.Line, to.Column + 1);
                    Piece rook = board.RemovePiece(toRook);
                    rook.decMovementQt();
                    board.PlacePiece(rook, fromRook);
                }
            }
        }

        public void validateFromPosition(Position from)
        {
            if (board.Piece(from) == null)
            {
                throw new BoardException("There is no piece at this position.");
            }
            if (curPlayer != board.Piece(from).Color)
            {
                throw new BoardException("You cannot move a piece from another player.");
            }
            if (!board.Piece(from).anyPossibleMove())
            {
                throw new BoardException("There is no move avaiable for this piece.");
            }
        }

        public void validateToPosition(Position from, Position to)
        {
            if (!board.Piece(from).possibleMovement(to))
            {
                throw new BoardException("Cannot make this move.");
            }
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> assistant = new HashSet<Piece>();
            foreach (Piece piece in captured)
            {
                if (piece.Color == color)
                {
                    assistant.Add(piece);
                }
            }
            return assistant;
        }

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> assistant = new HashSet<Piece>();
            foreach (Piece piece in pieces)
            {
                if (piece.Color == color)
                {
                    assistant.Add(piece);
                }
            }
            assistant.ExceptWith(capturedPieces(color));
            return assistant;
        }

        public void PlaceNewPiece(char column, int line, Piece piece)
        {
            board.PlacePiece(piece, new ChessPositioning(column, line).toPosition());
            pieces.Add(piece);
        }

        public bool isInCheck(Color color)
        {
            Piece k = king(color);
            foreach (Piece piece in piecesInGame(enemyColor(color)))
            {
                bool[,] mat = piece.PossibleMoves();
                if (mat[k.Position.Line, k.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testCheckMate(Color color)
        {
            if (!isInCheck(color)) { return false; }
            foreach (Piece piece in piecesInGame(color))
            {
                bool[,] mat = piece.PossibleMoves();
                for (int i = 0; i < board.Lines; i++)
                {
                    for (int j = 0; j < board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position from = piece.Position;
                            Position to = new Position(i, j);
                            Piece capturedPiece = makeMove(from, to);
                            bool testCheck = isInCheck(color);
                            undoMovement(from, to, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void changePlayer()
        {
            if (curPlayer == Color.White)
            {
                curPlayer = Color.Black;
            }
            else
            {
                curPlayer = Color.White;
            }
        }


        private void PlacePieces()
        {
            //White
            PlaceNewPiece('a', 1, new Rook(Color.White, board));
            PlaceNewPiece('b', 1, new Knight(Color.White, board));
            PlaceNewPiece('c', 1, new Bishop(Color.White, board));
            PlaceNewPiece('d', 1, new Queen(Color.White, board));
            PlaceNewPiece('e', 1, new King(Color.White, board, this));
            PlaceNewPiece('f', 1, new Bishop(Color.White, board));
            PlaceNewPiece('g', 1, new Knight(Color.White, board));
            PlaceNewPiece('h', 1, new Rook(Color.White, board));
            for (char i = 'a'; i <= 'h'; i++)
            {
                PlaceNewPiece(i, 2, new Pawn(Color.White, board));
            }


            //Black
            PlaceNewPiece('a', 8, new Rook(Color.Black, board));
            PlaceNewPiece('b', 8, new Knight(Color.Black, board));
            PlaceNewPiece('c', 8, new Bishop(Color.Black, board));
            PlaceNewPiece('d', 8, new Queen(Color.Black, board));
            PlaceNewPiece('e', 8, new King(Color.Black, board, this));
            PlaceNewPiece('f', 8, new Bishop(Color.Black, board));
            PlaceNewPiece('g', 8, new Knight(Color.Black, board));
            PlaceNewPiece('h', 8, new Rook(Color.Black, board));
            for (char i = 'a'; i <= 'h'; i++)
            {
                PlaceNewPiece(i, 7, new Pawn(Color.Black, board));
            }
        }

        private Color enemyColor(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece piece in piecesInGame(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }
    }
}
