using System;
using System.Collections.Generic;
using System.Text;

namespace explorer
{
    class Queen:Piece
    {
        public Queen(int x, int y, char letter, Chessboard board)
        {
            this.x = x;
            this.y = y;
            this.chessboard = board;
            this.Letter = letter;
        }

        override public List<int[]> Moves(char[][]board)
        {
            List<int[]> moves = new List<int[]>();
            int[] deltaX = { 1, 1, -1, -1, 0, 0, 1, -1 };
            int[] deltaY = { 1, -1, 1, -1, 1, -1, 0, 0 };
            for (int i = 0; i < 8; i++)
            {
                int x = this.x;
                int y = this.y;

                x += deltaX[i];
                y += deltaY[i];
                while (x < 8 && x >= 0 && y < 8 && y >= 0 && board[x][y] == '0')
                {
                    int[] move = { x, y };
                    moves.Add(move);
                    x += deltaX[i];
                    y += deltaY[i];
                }
                if (x < 8 && x >= 0 && y < 8 && y >= 0 && Chessboard.isOppositeColor(board[x][y], Letter))
                {
                    int[] move = { x, y };
                    moves.Add(move);
                }

            }
            return moves;
        }
    }
}
