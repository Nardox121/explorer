using System;
using System.Collections.Generic;
using System.Text;

namespace explorer
{
    class Knight : Piece
    {
        public Knight(Point position, Chessboard board, PieceColor color) : base(position, board, color) {
            Letter = 'n';
        }

        public override List<Point> Moves() {
            List<Point> moves = new List<Point>();

            Point[] offset = { 
                new Point(-1, 2), 
                new Point(1, 2), 
                new Point(-1, -2), 
                new Point(1, -2), 
                new Point(-2, -1), 
                new Point(-2, 1), 
                new Point(2, -1), 
                new Point(2, 1) 
            };

            for (int i = 0; i < offset.Length; i++) {
                Point possibleMove = position + offset[i];
                if (IsOnBoard(possibleMove) && chessboard.PieceOn(possibleMove).color != color) {
                    moves.Add(possibleMove);
                }
            }

            return moves;
        }
    }
}
