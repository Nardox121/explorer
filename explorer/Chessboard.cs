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

        public string Castling() {
            if(WhiteOnMove) {
                return WhiteCastling();
            }
            else {
                return BlackCastling();
            }
        }

        public string WhiteCastling() {
            Point[] longCastling = {
                new Point(2, 7),
                new Point(3, 7),
                new Point(4, 7),
            };
            bool longCastle = WhiteCastlingLong;

            Point[] shortCastling = {
                new Point(4, 7),
                new Point(5, 7),
                new Point(6, 7),
            };
            bool shortCastle = WhiteCastlingShort;

            if(IsPieceOn(new Point(1, 7)) || IsPieceOn(new Point(2, 7)) || IsPieceOn(new Point(3, 7))) {
                longCastle = false;
            }

            if(IsPieceOn(new Point(5, 7)) || IsPieceOn(new Point(6, 7))) {
                shortCastle = false;
            }

            for(int i = 0; i < 8; i++) {
                for(int j = 0; j < 8; j++) {
                    Piece piece = PieceBoard[i, j];

                    if(piece.color == PieceColor.black) {
                        List<Point> moves = piece.Moves();

                        foreach(Point move in moves) {
                            foreach(Point castlingSquare in longCastling) {
                                if(castlingSquare == move) {
                                    longCastle = false;
                                }
                            }
                            foreach(Point castlingSquare in shortCastling) {
                                if(castlingSquare == move) {
                                    shortCastle = false;
                                }
                            }
                        }
                    }
                }
            }

            StringBuilder s = new StringBuilder();
            if(longCastle) {
                s.Append("O-O-O\n");
            }
            if(shortCastle) {
                s.Append("O-O");
            }

            return s.ToString();
        }

        public string BlackCastling() {
            Point[] longCastling = {
                new Point(2, 0),
                new Point(3, 0),
                new Point(4, 0),
            };
            bool longCastle = WhiteCastlingLong;

            Point[] shortCastling = {
                new Point(4, 0),
                new Point(5, 0),
                new Point(6, 0),
            };
            bool shortCastle = WhiteCastlingShort;

            if(IsPieceOn(new Point(1, 0)) || IsPieceOn(new Point(2, 0)) || IsPieceOn(new Point(3, 0))) {
                longCastle = false;
            }

            if(IsPieceOn(new Point(5, 0)) || IsPieceOn(new Point(6, 0))) {
                shortCastle = false;
            }

            for(int i = 0; i < 8; i++) {
                for(int j = 0; j < 8; j++) {
                    Piece piece = PieceBoard[i, j];

                    if(piece.color == PieceColor.white) {
                        List<Point> moves = piece.Moves();

                        foreach(Point move in moves) {
                            foreach(Point castlingSquare in longCastling) {
                                if(castlingSquare == move) {
                                    longCastle = false;
                                }
                            }
                            foreach(Point castlingSquare in shortCastling) {
                                if(castlingSquare == move) {
                                    shortCastle = false;
                                }
                            }
                        }
                    }
                }
            }

            StringBuilder s = new StringBuilder();
            if(longCastle) {
                s.Append("O-O-O\n");
            }
            if(shortCastle) {
                s.Append("O-O");
            }

            return s.ToString();
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
