using System;
using System.Collections.Generic;
using System.Text;

namespace explorer {
    class Queen : Piece {
        public Queen(Point position, Chessboard board, PieceColor color) : base(position, board, color) {
            Letter = 'Q';
        }

        override public List<Point> Moves() {
            List<Point> moves = new List<Point>();

            Point[] offset = {
                new Point(1, 1),
                new Point(1, -1),
                new Point(-1, 1),
                new Point(-1, -1),
                new Point(0, 1),
                new Point(0, -1),
                new Point(1, 0),
                new Point(-1, 0)
            };

            for(int i = 0; i < 8; i++) {
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
