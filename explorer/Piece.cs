using System;
using System.Collections.Generic;
using System.Text;

namespace explorer
{
    abstract class Piece
    {
        public char Letter;
        public Chessboard chessboard;

        public int x;
        public int y;

        public abstract List<int[]> Moves(char[][]board);

        public List<int[]> PossibleMoves()
        {
            List<int[]> moves = new List<int[]>();
            foreach (int[] move in this.Moves(chessboard.Board))
            {
                chessboard.nextMoveBoard = new char[8][];
                for (int i = 0; i < 8; i++)
                {
                    chessboard.nextMoveBoard[i] = new char[8];
                    for(int j = 0; j<8;j++)
                    {
                        chessboard.nextMoveBoard[i][j] = chessboard.Board[i][j];
                    }
                }

                chessboard.nextMoveBoard[move[0]][move[1]] = this.Letter;
                chessboard.nextMoveBoard[this.x][this.y] = '0';

                chessboard.nextMovePieceBoard = chessboard.initializePieceBoard(chessboard.nextMoveBoard);

                if (!Chessboard.isCheck(chessboard.WhiteMove, chessboard.nextMoveBoard, chessboard.nextMovePieceBoard))
                {
                    moves.Add(move);
                }
            }
            return moves;
        }

    }
}
