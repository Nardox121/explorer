using System;
using System.Collections.Generic;
using System.Text;

namespace explorer {
    class Chessboard : ICloneable {
        public Piece[,] PieceBoard;
        public bool BlackCastlingLong { get; set; }
        public bool BlackCastlingShort { get; set; }
        public bool WhiteCastlingLong { get; set; }
        public bool WhiteCastlingShort { get; set; }
        public bool WhiteOnMove { get; set; }

        public Chessboard() { }
        public Chessboard(string FEN) {
            PieceBoard = new Piece[8, 8];

            string[] data = FEN.Split(' ');
            string[] pieces = data[0].Split('/');

            int row = 0;
            int col = 0;

            for(int i = 0; i < pieces.Length; i++) {
                for(int j = 0; j < pieces[i].Length; j++) {
                    if(Char.IsDigit(pieces[i][j])) {
                        for(int k = 0; k < int.Parse(pieces[i][j].ToString()); k++) {
                            PieceBoard[col, row] = new Empty(new Point(col,row), this, PieceColor.none);
                            col++;
                        }
                    }
                    else {
                        PieceColor color = char.IsUpper(pieces[i][j]) ? PieceColor.white : PieceColor.black;

                        if(char.ToLower(pieces[i][j]) == 'r') {
                            PieceBoard[col, row] = new Rook(new Point(col, row), this, color);
                        }
                        else if(char.ToLower(pieces[i][j]) == 'n') {
                            PieceBoard[col, row] = new Knight(new Point(col, row), this, color);
                        }
                        else if(char.ToLower(pieces[i][j]) == 'b') {
                            PieceBoard[col, row] = new Bishop(new Point(col, row), this, color);
                        }
                        else if(char.ToLower(pieces[i][j]) == 'q') {
                            PieceBoard[col, row] = new Queen(new Point(col, row), this, color);
                        }
                        else if(char.ToLower(pieces[i][j]) == 'k') {
                            PieceBoard[col, row] = new King(new Point(col, row), this, color);
                        }
                        else if(char.ToLower(pieces[i][j]) == 'p') {
                            PieceBoard[col, row] = new Pawn(new Point(col, row), this, color);
                        }

                        col++;
                    }
                }
                col = 0;
                row++;
            }

            WhiteOnMove = (data[1][0] == 'w') ? true : false;

            foreach(char c in data[2]) {
                if(c == 'K') {
                    WhiteCastlingShort = true;
                }
                else if(c == 'Q') {
                    WhiteCastlingLong = true;
                }
                else if(c == 'k') {
                    BlackCastlingShort = true;
                }
                else if(c == 'q') {
                    BlackCastlingLong = true;
                }
            }
        }

        public bool IsCheck() {
            Piece king = new Empty(new Point(0,0), this, PieceColor.none);

            if(WhiteOnMove) {
                for(int i = 0; i < 8; i++) {
                    for(int j = 0; j < 8; j++) {
                        if(PieceBoard[i,j] is King && PieceBoard[i, j].color == PieceColor.white) {
                            king = PieceBoard[i, j] as King;
                        }
                    }
                }
            }
            else {
                for(int i = 0; i < 8; i++) {
                    for(int j = 0; j < 8; j++) {
                        if(PieceBoard[i, j] is King && PieceBoard[i, j].color == PieceColor.black) {
                            king = PieceBoard[i, j] as King;
                        }
                    }
                }
            }

            for(int i = 0; i < 8; i++) {
                for(int j = 0; j < 8; j++) {
                    if(IsPieceOn(new Point(i, j))) {
                        Piece piece = PieceBoard[i, j];

                        if(piece.color != king.color) {
                            List<Point> moves = piece.Moves();
                            foreach(Point move in moves) {
                                if(move == king.position) return true;
                            }
                        }
                    }
                    
                }
            }

            return false;
        }

        public List<Move> AllMoves() {
            List<Move> moves = new List<Move>();
            for(int i = 0; i < 8; i++) {
                for(int j = 0; j < 8; j++) {
                    Piece piece = PieceBoard[i, j];

                    if((WhiteOnMove == true && piece.color == PieceColor.white) || (WhiteOnMove == false && piece.color == PieceColor.black)) {
                        List<Point> pieceMoves = piece.PossibleMoves();

                        foreach(Point p in pieceMoves) {
                            moves.Add(new Move(i, j, p.X, p.Y, piece.Letter));
                        }
                    }
                }
            }
            return moves;
        }

        public bool IsPieceOn(Point p)
            => !(PieceBoard[p.X, p.Y] is Empty);

        public Piece PieceOn(Point p)
            => PieceBoard[p.X, p.Y];

        public object Clone() {
            return this.MemberwiseClone();
        }
    }
}
