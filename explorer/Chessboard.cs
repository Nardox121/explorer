using System;
using System.Collections.Generic;
using System.Text;

namespace explorer
{
    class Chessboard
    {
        public char[][] Board = new char[8][];

        public Piece[][] PieceBoard;
        public bool BlackCastlingLong { get; set; }
        public bool BlackCastlingShort { get; set; }
        public bool WhiteCastlingLong { get; set; }
        public bool WhiteCastlingShort { get; set; }
        public bool WhiteMove { get; set; }

        public char[][] nextMoveBoard;
        public Piece[][] nextMovePieceBoard;

        public Chessboard(string FEN)
        {
            for (int i = 0; i < 8; i++)
                Board[i] = new char[8];
            string[] data = FEN.Split(' ');
            string[] pieces = data[0].Split('/');

            int row = 0;
            int col = 0;

            for (int i = 0; i < pieces.Length; i++)
            {
                for (int j = 0; j < pieces[i].Length; j++)
                {
                    if (Char.IsDigit(pieces[i][j]))
                    {
                        for (int k = 0; k < int.Parse(pieces[i][j].ToString()); k++)
                        {
                            Board[row][col] = '0';
                            col++;
                        }
                    }
                    else
                    {
                        Board[row][col] = pieces[i][j];
                        col++;
                    }
                }
                col = 0;
                row++;
            }

            WhiteMove = (data[1][0] == 'w') ? true : false;

            foreach (char c in data[2])
            {
                if (c == 'K')
                {
                    WhiteCastlingShort = true;
                }
                else if (c == 'Q')
                {
                    WhiteCastlingLong = true;
                }
                else if (c == 'k')
                {
                    BlackCastlingShort = true;
                }
                else if (c == 'q')
                {
                    BlackCastlingLong = true;
                }
            }

            PieceBoard = initializePieceBoard(Board);
        }

        public Piece[][] initializePieceBoard(char[][] board) {
            Piece[][] PB = new Piece[8][];
            for (int i = 0; i < 8; i++)
            {
                PB[i] = new Piece[8];
                for (int j = 0; j < 8; j++)
                {
                    char letter = board[i][j];
                    if (letter == '0')
                    {
                        PB[i][j] = new Empty(i, j, letter, this);
                    }
                    else if (char.ToLower(letter) == 'r')
                    {
                        PB[i][j] = new Rook(i, j, letter, this);
                    }
                    else if (char.ToLower(letter) == 'b')
                    {
                        PB[i][j] = new Bishop(i, j, letter, this);
                    }
                    else if (char.ToLower(letter) == 'n')
                    {
                        PB[i][j] = new Knight(i, j, letter, this);
                    }
                    else if (char.ToLower(letter) == 'q')
                    {
                        PB[i][j] = new Queen(i, j, letter, this);
                    }
                    else if (char.ToLower(letter) == 'p')
                    {
                        PB[i][j] = new Pawn(i, j, letter, this);
                    }
                    else
                    {
                        PB[i][j] = new King(i, j, letter, this);
                    }
                }
            }
            return PB;
        }

        public static bool isOppositeColor(char first, char second)
        {
            if (char.IsLower(first) == char.IsLower(second)) return false;
            if (char.IsUpper(first) == char.IsUpper(second)) return false;
            if (first == '0' | second == '0') return false;
            return true;
        }

        public static bool isOnBoard(int x, int y)
        {
            if (x < 0) return false;
            if (x > 7) return false;
            if (y < 0) return false;
            if (y > 7) return false;
            return true;
        }

        public static bool isCheck(bool whiteMove, char[][] board, Piece[][] pieceBoard)
        {
            int kingx = 0;
            int kingy = 0;
            if (whiteMove)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (board[i][j] == 'K')
                        {
                            kingx = i;
                            kingy = j;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (board[i][j] == 'k')
                        {
                            kingx = i;
                            kingy = j;
                        }
                    }
                }
            }
            Piece king = pieceBoard[kingx][kingy];
            int[] kingPosition = { kingx, kingy };
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    char letter = pieceBoard[i][j].Letter;

                    if (Chessboard.isOppositeColor(letter, king.Letter))
                    {
                        List<int[]> ms = pieceBoard[i][j].Moves(board);
                        foreach (int[] m in ms)
                        {
                            if (m[0] == kingPosition[0] && m[1] == kingPosition[1]) return true;
                        }
                    }
                }
            }
            return false;
        }

        public List<Move> allMoves()
        {
            List<Move> moves = new List<Move>();
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    List<int[]> pieceMoves = new List<int[]>();
                    if (char.IsUpper(Board[i][j])==WhiteMove)
                    {
                        if (PieceBoard[i][j].Moves(this.Board) != null)
                        {
                            pieceMoves = PieceBoard[i][j].PossibleMoves();
                        }
                        foreach (int[] pm in pieceMoves)
                        {
                            moves.Add(new Move(i, j, pm[0], pm[1], PieceBoard[i][j].Letter, WhiteMove));
                        }
                    }
                }
            }
            return moves;
        }

    }
}
