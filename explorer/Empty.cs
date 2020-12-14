using System;
using System.Collections.Generic;
using System.Text;

namespace explorer {
    class Empty : Piece {
        public Empty(int x, int y, char letter, Chessboard board) {
            this.x = x;
            this.y = y;
            this.chessboard = board;
            this.Letter = letter;
        }

        public override List<int[]> Moves(char[][] board) {
            return null;
        }
    }
}
