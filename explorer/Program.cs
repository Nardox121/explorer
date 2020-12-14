using System;
using System.Collections.Generic;

namespace explorer
{
    class Program
    {
        static void Main(string[] args)
        {
            string str;
            while((str = Console.ReadLine()) != "") {
                Chessboard c = new Chessboard(str);
                List<Move> moves = c.AllMoves();
                foreach(Move m in moves) {
                    Console.WriteLine(m);
                }
            }
        }
    }
}
