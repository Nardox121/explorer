using System;
using System.Collections.Generic;
using System.Text;

namespace explorer {
    class Empty : Piece {
        public Empty(Point position, Chessboard board, PieceColor color) : base(position, board, color) {
            Letter = '0';
        }

        public override List<Point> Moves() {
            return new List<Point>();
        }
    }
}
