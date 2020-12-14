using System;
using System.Collections.Generic;
using System.Text;

namespace explorer {
    abstract class Piece {
        public char Letter { get; set; }
        public Chessboard chessboard;
        public PieceColor color;

        public Point position;

        public Piece(Point point, Chessboard board, PieceColor color) {
            this.position = point;
            this.chessboard = board;
            this.color = color;
        }

        public abstract List<Point> Moves();

        public List<Point> PossibleMoves() {
            List<Point> moves = new List<Point>();

            foreach(Point move in this.Moves()) {
                Piece tmp = chessboard.PieceBoard[move.X, move.Y];

                chessboard.PieceBoard[move.X, move.Y] = (Piece)this.MemberwiseClone();
                chessboard.PieceBoard[move.X, move.Y].position = new Point(move.X, move.Y);
                chessboard.PieceBoard[position.X, position.Y] = new Empty(new Point(position.X, position.Y), chessboard, PieceColor.none);

                if(!chessboard.IsCheck()) {
                    moves.Add(move);
                }

                chessboard.PieceBoard[move.X, move.Y] = tmp;
                chessboard.PieceBoard[position.X, position.Y] = this;
            }

            return moves;
        }

        public bool IsOnBoard(Point p)
            => ((p.X >= 0) && (p.X <= 7) && (p.Y >= 0) && (p.Y <= 7));
    }
}
