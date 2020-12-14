using System;
using System.Collections.Generic;
using System.Text;

namespace explorer
{
    class King : Piece
    {
        public King(Point position, Chessboard board, PieceColor color) : base(position, board, color) {
            Letter = 'K';
        }

        override public List<Point> Moves() {
            List<Point> moves = new List<Point>();

            for(int i = -1; i < 2; i++) {
                for(int j = -1; j < 2; j++) {
                    Point possibleMove = position + new Point(i, j);
                    if(IsOnBoard(possibleMove) && chessboard.PieceOn(possibleMove).color != color) {
                        moves.Add(possibleMove);
                    }
                }
            }

            return moves;
        }

    }
}
