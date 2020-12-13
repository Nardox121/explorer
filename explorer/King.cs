using System;
using System.Collections.Generic;
using System.Text;

namespace explorer
{
    class King : Piece
    {
        public King(int x, int y, char letter, Chessboard board)
        {
            this.x = x;
            this.y = y;
            this.chessboard = board;
            this.Letter = letter;
        }

        override public List<int[]> Moves(char[][]board)
        {
            List<int[]> moves = new List<int[]>();
            int[,] multipliers = { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };
            for (int set = 0; set < 8; set++)
            {
                if(x + multipliers[set, 0] < 8 & y + multipliers[set, 1] < 8 & x + multipliers[set, 0] >= 0 & y + multipliers[set, 1] >= 0)
                {
                    if (board[x + multipliers[set, 0]][y + multipliers[set, 1]] == '0')
                    {
                        int[] move = { x + multipliers[set, 0], y + multipliers[set, 1] };
                        moves.Add(move);
                    }
                    else if (Chessboard.isOppositeColor(board[x + multipliers[set, 0]][y + multipliers[set, 1]], Letter))
                    {
                        int[] move = { x + multipliers[set, 0], y + multipliers[set, 1] };
                        moves.Add(move);
                    }
                }
            }

            return moves;
        }

    }
}
