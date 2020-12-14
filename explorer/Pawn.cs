using System;
using System.Collections.Generic;
using System.Text;

namespace explorer {
    class Pawn : Piece {

        public Pawn(Point position, Chessboard board, PieceColor color) : base(position, board, color) {
            Letter = '\0';
        }

        override public List<Point> Moves() {
            List<Point> moves = new List<Point>();

            if(color == PieceColor.white) {
                Point possibleMove = position + new Point(0, -1);
                if(IsOnBoard(possibleMove) && !chessboard.IsPieceOn(possibleMove)) {
                    moves.Add(possibleMove);
                }

                possibleMove = position + new Point(0, -2);
                if(position.Y == 6  && !chessboard.IsPieceOn(possibleMove) && !chessboard.IsPieceOn(position + new Point(0, -1))) {
                    moves.Add(possibleMove);
                }

                possibleMove = position + new Point(-1, -1);
                if(IsOnBoard(possibleMove) && chessboard.IsPieceOn(possibleMove) && chessboard.PieceOn(possibleMove).color != color) {
                    moves.Add(possibleMove);
                }

                possibleMove = position + new Point(1, -1);
                if(IsOnBoard(possibleMove) && chessboard.IsPieceOn(possibleMove) && chessboard.PieceOn(possibleMove).color != color) {
                    moves.Add(possibleMove);
                }

                return moves;
            }
            else {
                Point possibleMove = position + new Point(0, 1);
                if(IsOnBoard(possibleMove) && !chessboard.IsPieceOn(possibleMove)) {
                    moves.Add(possibleMove);
                }

                possibleMove = position + new Point(0, 2);
                if(position.Y == 6 && !chessboard.IsPieceOn(possibleMove) && !chessboard.IsPieceOn(position + new Point(0, 1))) {
                    moves.Add(possibleMove);
                }

                possibleMove = position + new Point(-1, 1);
                if(IsOnBoard(possibleMove) && chessboard.IsPieceOn(possibleMove) && chessboard.PieceOn(possibleMove).color != color) {
                    moves.Add(possibleMove);
                }

                possibleMove = position + new Point(1, 1);
                if(IsOnBoard(possibleMove) && chessboard.IsPieceOn(possibleMove) && chessboard.PieceOn(possibleMove).color != color) {
                    moves.Add(possibleMove);
                }

                return moves;
            }
        }
    }
}
