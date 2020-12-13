using System;
using System.Collections.Generic;
using System.Text;

namespace explorer
{
    class Pawn : Piece
    {

        public Pawn(int x, int y, char letter, Chessboard board)
        {
            this.x = x;
            this.y = y;
            this.chessboard = board;
            this.Letter = letter;
        }

        override public List<int[]> Moves(char[][]board)
        {
            List<int[]> moves = new List<int[]>();
            if (char.IsUpper(Letter))
            {
                if (y - 1 >= 0 && Chessboard.isOppositeColor(board[x - 1][y - 1], Letter))
                {
                    int[] move = { x - 1, y - 1 };
                    moves.Add(move);
                }
                if (y + 1 < 8 && Chessboard.isOppositeColor(board[x - 1][y + 1], Letter))
                {
                    int[] move = { x - 1, y + 1 };
                    moves.Add(move);
                }
                if (board[x - 1][y] == '0')
                {
                    int[] move = { x - 1, y };
                    moves.Add(move);
                }
                return moves;
            }
            else
            {
                if (y - 1 >= 0 && Chessboard.isOppositeColor(board[x + 1][y - 1], Letter))
                {
                    int[] move = { x + 1, y - 1 };
                    moves.Add(move);
                }
                if (y + 1 < 8 && Chessboard.isOppositeColor(board[x + 1][y + 1], Letter))
                {
                    int[] move = { x + 1, y + 1 };
                    moves.Add(move);
                }
                if (board[x + 1][y] == '0')
                {
                    int[] move = { x + 1, y };
                    moves.Add(move);
                }
                return moves;
            }
        }
    }
}
