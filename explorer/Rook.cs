using System;
using System.Collections.Generic;
using System.Text;

namespace explorer {
    class Rook : Piece {
        public Rook(Point position, Chessboard board, PieceColor color) : base(position, board, color) {
            Letter = 'R';
        }

        override public List<Point> Moves() {
            List<Point> moves = new List<Point>();

            Point[] offset = {
                new Point(0, 1),
                new Point(0, -1),
                new Point(1, 0),
                new Point(-1, 0)
            };

            for(int i = 0; i < 4; i++) {
                Point possibleMove = position;

                possibleMove += offset[i];
                while(IsOnBoard(possibleMove) && chessboard.PieceOn(possibleMove).color == PieceColor.none) {
                    moves.Add(possibleMove);
                    possibleMove += offset[i];
                }
                if(IsOnBoard(possibleMove) && chessboard.PieceOn(possibleMove).color != color) {
                    moves.Add(possibleMove);
                }
            }

            return moves;
        }
    }
}
