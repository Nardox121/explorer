using System;
using System.Collections.Generic;

namespace explorer
{
    class Program
    {
        static void Main(string[] args)
        {
            Chessboard c = new Chessboard("8/8/2q5/6b1/8/1pBnP3/4P3/2K5 w - - 0 1");
            List<Move> moves = c.allMoves();
            foreach(Move m in moves)
            {
                m.printMove();
            }
        }
    }
}
