using System;
using System.Collections.Generic;
using System.Text;

namespace explorer
{
    class Knight : Piece
    {
        public Knight(int x, int y, char letter, Chessboard board)
        {
            this.x = x;
            this.y = y;
            this.chessboard = board;
            this.Letter = letter;
        }

        override public List<int[]> Moves(char[][]board)
        {
            List<int[]> moves = new List<int[]>();
            int[,] values = { { -1, 2 }, { 1, 2 }, { -1, -2 }, { 1, -2 }, { -2, -1 }, { -2, 1 }, { 2, -1 }, { 2, 1 } };
            for (int set = 0; set < 8; set++)
            {
                if (Chessboard.isOnBoard(x + values[set, 0], y + values[set, 1]) && (board[x + values[set, 0]][y + values[set, 1]] == '0' || Chessboard.isOppositeColor(board[x + values[set, 0]][y + values[set, 1]], Letter)))
                {
                    int[] move = { x + values[set, 0], y + values[set, 1] };
                    moves.Add(move);
                }
            }
            return moves;
        }

    }
}
